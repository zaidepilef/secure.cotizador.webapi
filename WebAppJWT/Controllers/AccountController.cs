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
    public class AccountController : ApiController
    {

        [HttpGet]
        [CustomAuthenticationFilter]
        public HttpResponseMessage MyUser()
        {
            try
            {
                System.Net.Http.Headers.HttpRequestHeaders headers = this.Request.Headers;
                string token = string.Empty;
                string data = string.Empty;
                if (headers.Contains("Authorization"))
                {
                    token = headers.GetValues("Authorization").First();
                    string res = token.Replace("Bearer ", "");
                    data = TokenManager.ValidateToken(res);

                    User who = new User();
                    who.apiKey = "";
                    who.email = "";
                    who.name = "";
                    who.id = "";

                    string fileData = @"C:\Users\Ougt\source\repos\CotizadorWebApi\WebAppJWT\Datos\Users.json";
                    using (StreamReader r = new StreamReader(fileData))
                    {
                        string json = r.ReadToEnd();
                        List<User> userList = JsonConvert.DeserializeObject<List<User>>(json);
                        foreach (var user in userList)
                        {
                            if (user.id == data)
                            {
                                who.apiKey = user.apiKey;
                                who.email = user.email;
                                who.name = user.name;
                                who.id = user.id;
                            }
                        }
                    }
                    return Request.CreateResponse(HttpStatusCode.OK, who);
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, message: "Usuario no identficado");
                }
            }
            catch (Exception)
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }

    }
}
