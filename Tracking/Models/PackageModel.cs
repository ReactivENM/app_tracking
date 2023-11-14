using System;
using System.Threading.Tasks;
using System.Configuration;
using System.Net.Http;

namespace Models.PackageModel
{
    public class PackageModel
    {

        public PackageModel()
        {

        }

        public async Task<string> GetStatus(string id_externo)
        {
            string apiUrl = $"{ConfigurationSettings.AppSettings["API_URL"]}/transit/status/{id_externo}";

            using (HttpClient client = new HttpClient())
            {
                try
                {
                    HttpResponseMessage response = await client.GetAsync(apiUrl);

                    if (response.IsSuccessStatusCode)
                    {
                        string res = await response.Content.ReadAsStringAsync();
                        return res;
                    }
                    else
                    {
                        return null;
                    }
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
        }
    }
}
