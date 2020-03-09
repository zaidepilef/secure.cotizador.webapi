using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace WebMVC.Controllers
{
    public class HomeController : Controller
    {
        private static string WebAPIURL = "https://localhost:44359/";
        private static string SecureUrl = "https://localhost:44359/secure/gettoken";
        private static string getCiudadesURL = "https://localhost:44359/secure/gettoken";

        // GET: Home
        public ActionResult Index()
        {
            //urlApiAuthenticate = ConfigurationManager.AppSettings["URL_AUTHENTICATE"];
            //Apikey = ConfigurationManager.AppSettings["MAPFRE_ATLAS_API_KEY"];
            HttpClient httpClient = new HttpClient();
            ApiRequestModel apmo = new ApiRequestModel();
            string tokenBased = "";
            apmo.apiKey = "1001";
            //var content = new StringContent(apmo.ToString(), Encoding.UTF8, "application/json");
            var myContent = JsonConvert.SerializeObject(apmo);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var result = httpClient.PostAsync(SecureUrl, byteContent).Result.Content;
            var contents = result.ReadAsStringAsync().Result;
            tokenBased = JsonConvert.DeserializeObject<string>(contents);

            Session["TokenNumber"] = tokenBased;

            return Content(tokenBased);
           
        }


        public async Task<ActionResult> CiudadesEcuador() {
            string ReturnMessage = String.Empty;
            using (var client =  new HttpClient()) {
                client.DefaultRequestHeaders.Clear();
                client.BaseAddress = new Uri(WebAPIURL);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(mediaType:"application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(scheme:"Bearer",parameter:Session["TokenNumber"].ToString());
                var responseMessage = await client.GetAsync(requestUri: "ecuador/getcities");
                if (responseMessage.IsSuccessStatusCode) {
                    var resultMessage = responseMessage.Content.ReadAsStringAsync().Result;
                    ReturnMessage = JsonConvert.SerializeObject(resultMessage).ToString();

                }
            }
            return Content(ReturnMessage);
        }


    }

    internal class ApiRequestModel
    {
        public string apiKey { get; set; }
    }
}