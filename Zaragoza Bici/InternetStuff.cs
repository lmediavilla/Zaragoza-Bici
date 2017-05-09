using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;
namespace Zaragoza_Bici
{
    class InternetStuff
    {

        public string url = "https://www.bizizaragoza.com/sites/default/files/stations/station_list.json";
        public string Response { get; set; }

        public InternetStuff()
        {


        }

        public async Task<List<Station>> getData()
        {
            try
            {
                System.Uri geturi = new System.Uri(url);
                HttpClient myClient = new HttpClient();
                System.Net.Http.HttpResponseMessage responseGet = await myClient.GetAsync(geturi);
                Response = await responseGet.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<Station>>(Response);

            }
            catch (Exception)
            {

                return null;

            }
        }
        public List<Station> getData2()
        {
            List<Station> L = this.getData().Result;
            return L;
        }






    }
}
