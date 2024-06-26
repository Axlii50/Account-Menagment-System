using Microsoft.Win32;
using Newtonsoft.Json;
using System;
using System.Diagnostics;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.ServiceProcess;

class Program
{
    static void Main()
    {
        var query = new EventLogQuery("Security", PathType.LogName, "*[System/EventID=4624] and *[EventData[Data[@Name='LogonType']='10']]");
        var watcher = new EventLogWatcher(query);
        watcher.EventRecordWritten += new EventHandler<EventRecordWrittenEventArgs>(OnEventRecordWritten);
        watcher.Enabled = true;

        Console.WriteLine("Monitoring RDP connections. Press Enter to exit.");
        Console.ReadLine();
    }

    private static void OnEventRecordWritten(object sender, EventRecordWrittenEventArgs e)
    {
        if (e.EventRecord != null)
        {
            var properties = e.EventRecord.Properties;
            var username = properties[5].Value.ToString(); // Index 5 should correspond to the TargetUserName

            Console.WriteLine($"Detected RDP logon for user: {username}");

            var data = GetAccountRDPInfo(username).Result;

            Console.WriteLine($"{data.IsActive}    {data.RamAmountInMB}  {data.IsAdmin}   {data.ThreadCount}");


            if(!data.IsActive)
            {
                LogOff(username);
            }
            else if(!data.IsAdmin)
            {
                Console.WriteLine($"Ustawiam zasoby dla: {username}  ->     {data.RamAmountInMB}    {data.ThreadCount}");
                SetResourceLimits(username, data.ThreadCount, data.RamAmountInMB);
            }
        }
    }

    static void LogOff(string UserName)
    {
        int sessionId = GetSessionIdByUsername(UserName);

        Console.WriteLine(sessionId);

        if (sessionId != -1)
        {
            try
            {
                ProcessStartInfo psi = new ProcessStartInfo
                {
                    FileName = "cmd.exe",
                    Arguments = $"/C logoff {sessionId}",
                    UseShellExecute = false,
                    CreateNoWindow = true
                };

                Process.Start(psi);
                Console.WriteLine($"Odłączono użytkownika {UserName}, sesja numer: {sessionId}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Błąd podczas odłączania użytkownika: {ex.Message}");
            }
        }
        else
        {
            Console.WriteLine($"Nie można odnaleźć sesji dla użytkownika {UserName}");
        }
    }

    static int GetSessionIdByUsername(string username)
    {
        try
        {
            ProcessStartInfo psi = new ProcessStartInfo
            {
                FileName = "cmd.exe",
                Arguments = $"/C quser \"{username.ToLower()}\"",
                RedirectStandardOutput = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };

            using (Process process = Process.Start(psi))
            {
                using (var reader = process.StandardOutput)
                {
                    string result = reader.ReadToEnd();
                    string[] lines = result.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);

                    if (lines.Length > 1)
                    {
                        string line = lines[1]; // Przyjmujemy, że interesuje nas druga linia wynikowa
                        string[] parts = line.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                        if (parts.Length >= 3 && int.TryParse(parts[2], out int sessionId))
                        {
                            return sessionId;
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Błąd podczas pobierania numeru sesji dla użytkownika {username}: {ex.Message}");
        }

        return -1; // Zwracamy -1, jeśli nie udało się znaleźć sesji dla użytkownika
    }

    static async Task<AccountRDPDTO> GetAccountRDPInfo(string AccountName)
    {
        try
        {
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.GetAsync($"http://axlii50.somee.com/Accounts/GetRDPAccountData?accountLogin={AccountName}");

                if (response.IsSuccessStatusCode)
                {
                    string responseBody = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<AccountRDPDTO>(responseBody);
                }
                else
                {
                    Console.WriteLine($"Błąd podczas wywoływania API. Kod odpowiedzi: {response.StatusCode}");
                    return null;
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Błąd podczas pobierania danych z API: {ex.Message}");
            return null;
        }
    }

    static void SetResourceLimits(string username, int threadCount, int ramLimitInMB)
    {
        try
        {
            // Ustawienie limitu wątków dla procesów użytkownika
            Process[] processes = Process.GetProcessesByName(username);
            foreach (Process process in processes)
            {
                try
                {
                    process.ProcessorAffinity = (IntPtr)((1 << threadCount) - 1);
                    Console.WriteLine($"Ustawiono limit {threadCount} wątków dla procesu {process.ProcessName} (PID: {process.Id})");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Błąd podczas ustawiania limitu wątków dla procesu {process.ProcessName} (PID: {process.Id}): {ex.Message}");
                }
            }

            // Ustawienie limitu RAM w rejestrze systemowym
            Registry.SetValue(@"HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Control\Terminal Server\RCM", "MemoryAllocation", ramLimitInMB, RegistryValueKind.DWord);
            Console.WriteLine($"Ustawiono limit RAM na {ramLimitInMB} MB");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Błąd podczas ustawiania limitów zasobów dla użytkownika {username}: {ex.Message}");
        }
    }

    public class AccountRDPDTO
    {
        public bool IsAdmin { get; set; }

        public bool IsActive { get; set; }

        public int ThreadCount { get; set; }
        public int RamAmountInMB { get; set; }
    }
}


