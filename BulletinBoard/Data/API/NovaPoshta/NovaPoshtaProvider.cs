using BulletinBoard.Controllers;
using BulletinBoard.Data.Extensions;
using BulletinBoard.Models.NovaPoshtaModels;
using BulletinBoard.Models.NovaPoshtaModels.Request;
using BulletinBoard.Models.NovaPoshtaModels.Request.Interfaces;
using Newtonsoft.Json.Linq;
using System.Configuration;
using System.Net.Http;
using System.Security.Policy;

namespace BulletinBoard.Data.API.NovaPoshta
{
    public class NovaPoshtaProvider
    {
        private readonly HttpClient _httpClient = new HttpClient();
        private readonly string _apiKey;
        const string BaseUrl = "https://api.novaposhta.ua/v2.0/json//api/partner";

        //public NovaPoshtaProvider(IConfiguration configuration)
        //{
        //    Configuration = configuration;
        //    _apiKey = Configuration.GetValue<string>("NovaPoshtaKey");
        //}

        //public IConfiguration Configuration { get; }

        private T? CheckToSucces<T>(HttpResponseMessage result )
        {
            if (result.IsSuccessStatusCode)
            {
                var content = result.Content.ReadAsStringAsync().Result;
                JObject json = JObject.Parse(content);
                T item = Newtonsoft.Json.JsonConvert.DeserializeObject<T>(content);

                if (item != null)
                {
                    return item;
                }
                return default(T);
            };
            return default(T);
        }

        public async Task<List<City>> GetCitys()
        {
            var data = new
            {
                apiKey = _apiKey,
                modelName = "Address",
                calledMethod = "getCities",
                methodProperties = new Dictionary<string, string>() { }
            };

            var result = await _httpClient.PostAsync(BaseUrl + "/cities", data.AsJson());
            var checkToSucces = CheckToSucces<CitiesRequest>(result);

            if (checkToSucces != null)
            {
                return checkToSucces.data;
            }
            return new List<City>();
        }

        public async Task<List<Warehouse>> GetWarehouses(string city)
        {
            if(city == null)
            {
                return new List<Warehouse>();
            }
            var data = new
            {
                apiKey = _apiKey,
                modelName = "Address",
                calledMethod = "getWarehouses",
                methodProperties = new Dictionary<string, string>()
                {
                    { "CityName" , city}
                }
            };

            var result = await _httpClient.PostAsync(BaseUrl + "/districts", data.AsJson());
            var checkToSucces = CheckToSucces<WarehousesRequest>(result);

            if (checkToSucces != null)
            {
                return checkToSucces.data;
            }
            return new List<Warehouse>();
        }
    }
}
