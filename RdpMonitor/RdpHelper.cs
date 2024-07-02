using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RdpMonitor
{
    internal class RdpHelper
    {
        public static void DisableAccount(string UserName, bool State)
        {
            try
            {
                ProcessStartInfo psi = new ProcessStartInfo
                {
                    FileName = "net",
                    Arguments = $"user {UserName} /active:{(State ? "yes" : "no")}",
                    UseShellExecute = false,
                    CreateNoWindow = true,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true
                };

                using (Process process = Process.Start(psi))
                {
                    string output = process.StandardOutput.ReadToEnd();
                    string error = process.StandardError.ReadToEnd();
                    process.WaitForExit();

                    if (process.ExitCode == 0)
                    {
                        Logger.WriteLine($"Konto użytkownika {UserName} zostało {(State ? "włączone" : "wyłączone")}.");
                    }
                    else
                    {
                        Logger.WriteLine($"Nie udało się {(State ? "włączyć" : "wyłączyć")} konta użytkownika {UserName}.");
                        if (!string.IsNullOrEmpty(error))
                        {
                            Logger.WriteLine($"Błąd: {error}");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLine($"Błąd podczas wyłączania konta użytkownika: {ex.Message}");
            }
        }

        public static void LogOff(string UserName)
        {
            int sessionId = GetSessionIdByUsername(UserName);

            Logger.WriteLine(sessionId);

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
                    Logger.WriteLine($"Odłączono użytkownika {UserName}, sesja numer: {sessionId}");
                }
                catch (Exception ex)
                {
                    Logger.WriteLine($"Błąd podczas odłączania użytkownika: {ex.Message}");
                }
            }
            else
            {
                Logger.WriteLine($"Nie można odnaleźć sesji dla użytkownika {UserName}");
            }
        }

        public static int GetSessionIdByUsername(string username)
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
                Logger.WriteLine($"Błąd podczas pobierania numeru sesji dla użytkownika {username}: {ex.Message}");
            }

            return -1; // Zwracamy -1, jeśli nie udało się znaleźć sesji dla użytkownika
        }

        public static void MonitorRam(string userName, int ramLimitInMB)
        {
            
            //string userName = "NazwaUżytkownika";
            var sessionId = GetSessionIdByUsername(userName);


            long totalRAMUsage = 0;

            // Monitorowanie procesów użytkownika
            foreach (var process in Process.GetProcesses())
            {
                try
                {
                    if (process.SessionId == sessionId)
                    {
                        totalRAMUsage += process.PrivateMemorySize64;
                        //Logger.WriteLine(totalRAMUsage);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Błąd podczas pobierania informacji o procesie: {ex.Message}");
                }
            }

            var TotalMBUsage = totalRAMUsage / (1024 * 1024);

            Console.WriteLine($"Aktualne użycie RAM przez użytkownika {userName}: {TotalMBUsage} MB    -> max Ram:  {TotalMBUsage}/{ramLimitInMB}");

            while (TotalMBUsage > ramLimitInMB)
            {
                // Znalezienie procesu zużywającego najwięcej pamięci
                var userProcesses = Process.GetProcesses().Where(p => p.SessionId == sessionId);
                var processToKill = userProcesses.OrderByDescending(p => p.PrivateMemorySize64).FirstOrDefault();

                if (processToKill != null)
                {
                    Logger.WriteLine($"Przekroczono limit RAM. Zatrzymywanie procesu {processToKill.ProcessName} (PID: {processToKill.Id})");
                    try
                    {
                        processToKill.Kill();
                        // Czekamy na zakończenie procesu
                        processToKill.WaitForExit(5000); // Czekamy maksymalnie 5 sekund

                        // Aktualizacja całkowitego zużycia RAM po zamknięciu procesu
                        TotalMBUsage = (long)Process.GetProcesses()
                            .Where(p => p.SessionId == sessionId)
                            .Sum(p => p.PrivateMemorySize64) / (1024 * 1024);
                    }
                    catch (Exception ex)
                    {
                        Logger.WriteLine($"Błąd podczas zatrzymywania procesu: {ex.Message}");
                    }
                }
                else
                {
                    // Jeśli nie ma już procesów do zamknięcia, przerywamy pętlę
                    Logger.WriteLine("Brak procesów do zamknięcia. Limit RAM nadal przekroczony.");
                    break;
                }
            }

            Thread.Sleep(10000); // Poczekaj 10 sekund przed kolejnym sprawdzeniem
        }
    }
}
