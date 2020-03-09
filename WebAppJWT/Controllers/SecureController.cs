using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebAppJWT.Models;

namespace WebAppJWT.Controllers
{
    public class SecureController : ApiController
    {


        [HttpPost]
        public HttpResponseMessage GetToken(RequestApiKey ApiKey)
        {
            try
            {
                if (ApiKey == null)
                    throw new HttpResponseException(HttpStatusCode.BadRequest);


                string fileData = @"C:\Users\Ougt\source\repos\CotizadorWebApi\WebAppJWT\Datos\Users.json";
                string userKey = "";
                using (StreamReader r = new StreamReader(fileData))
                {
                    string json = r.ReadToEnd();
                    List<User> usersList = JsonConvert.DeserializeObject<List<User>>(json);
                    foreach (var user in usersList)
                    {
                        if (user.apiKey == ApiKey.apiKey)
                        {
                            userKey = user.apiKey;
                        }
                    }
                }

                if (userKey.Length > 0)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, value: TokenManager.GenerateToken(ApiKey.apiKey));
                }
                else {
                    return Request.CreateResponse(HttpStatusCode.NotFound);
                }
               
            }
            catch (Exception)
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }

        /// <summary>
        /// primera version
        /// </summary>
        /// <param name="ApiKey"></param>
        /// <returns></returns>
        [HttpPost]
        public HttpResponseMessage GetTokenTest(RequestApiKey ApiKey)
        {

            if (ApiKey.apiKey == "1234")
            {

                return Request.CreateResponse(HttpStatusCode.OK, value: TokenManager.GenerateToken(ApiKey.apiKey));
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, message: "Usuario no identficado");
            }
        }

    }
}
