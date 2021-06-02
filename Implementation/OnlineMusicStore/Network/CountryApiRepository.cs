using Newtonsoft.Json;
using OnlineMusicStore.Models.NetworkModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace OnlineMusicStore.Network
{
    public class CountryApiRepository : INetworkRepository
    {

        public string BASE_URL { get; }

        public CountryApiRepository()
        {
            BASE_URL = "https://restcountries.eu/rest/v2/";
        }

        public async Task<List<Country>> GetAllCountries()
        {
            string endpoint = "all";

            List<Country> countries = null;

            using(var client = new HttpClient())
            {
                client.BaseAddress = new Uri(BASE_URL);

                HttpResponseMessage response = await client.GetAsync(endpoint);

                if(response.IsSuccessStatusCode)
                {
                    var res = await response.Content.ReadAsStringAsync();

                    countries = JsonConvert.DeserializeObject<List<Country>>(res);
                }
            }

            return countries;
        }
    }
}
