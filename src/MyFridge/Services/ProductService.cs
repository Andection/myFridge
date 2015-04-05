using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Xamarin.Forms;

namespace MyFridge.Services
{
    public class ProductService
    {
        private const string BaseUri = @"https://api.outpan.com/v1/products/";
        private const string ApiToken = "c06d02c98c067894d62d9c787eec4f5f";

        public async Task<string> FindByBarcode(string barcode)
        {
            using (var httpClient = new HttpClient())
            {
                var byteArray = Encoding.UTF8.GetBytes(string.Format("{0}:011235813_A", ApiToken));
                httpClient.DefaultRequestHeaders.Authorization =
                    new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));
                httpClient.Timeout = TimeSpan.FromSeconds(15);

                var uri = new Uri(BaseUri + barcode + "/name");
                var rawResult = await httpClient.GetStringAsync(uri);

                var result = JsonConvert.DeserializeObject<dynamic>(rawResult);

                return result.name;
            }
        }
    }
}