using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RdpMonitor
{
    internal static class Api
    {
        public static async Task<AccountRDPDTO> GetAccountRDPInfo(string AccountName)
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
                        Debug.WriteLine(responseBody);
                        return JsonConvert.DeserializeObject<AccountRDPDTO>(responseBody);
                    }
                    else
                    {
                        Logger.WriteLine($"Błąd podczas wywoływania API. Kod odpowiedzi: {response.StatusCode}");
                        return null;
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLine($"Błąd podczas pobierania danych z API: {ex.Message}");
                return null;
            }
        }
    }
}
