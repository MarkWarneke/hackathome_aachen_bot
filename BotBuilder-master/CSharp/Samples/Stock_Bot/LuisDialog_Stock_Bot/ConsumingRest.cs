using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;

namespace LuisDialog_Stock_Bot
{
    public class ConsumingRest
    {
        private string url;
        private HttpClient client;

        public ConsumingRest(string url)
        {
            this.url = url;

            client = new HttpClient();
            client.BaseAddress = new Uri(url);
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<DataObject> getObj(string parameter)
        {
            DataObject dataObj = null;
            HttpResponseMessage msg = client.GetAsync(parameter).Result;

            if (msg.IsSuccessStatusCode)
            {
                dataObj = await msg.Content.ReadAsAsync<DataObject>();
            }

            return dataObj;
        }

        public async Task<Uri> postObj(string parameter, DataObject dataObj)
        {
            HttpResponseMessage msg = await client.PostAsJsonAsync(parameter, dataObj);
            msg.EnsureSuccessStatusCode();

            return msg.Content.Headers.ContentLocation;
        }
    }
}