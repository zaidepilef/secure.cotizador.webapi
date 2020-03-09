using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebAppJWT.Controllers
{
    public class EcuadorController : ApiController
    {
        [HttpGet]
        [CustomAuthenticationFilter]
        public HttpResponseMessage GetCities()
        {

            try
            {
                string fileData = System.IO.File.ReadAllText(@"C:\Users\Ougt\source\repos\CotizadorWebApi\WebAppJWT\Datos\Regiones.json");
                object jsonObject = JsonConvert.DeserializeObject(fileData);
                return Request.CreateResponse(HttpStatusCode.OK, jsonObject);
            }
            catch (Exception ex)
            {
                
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }

    }
}
