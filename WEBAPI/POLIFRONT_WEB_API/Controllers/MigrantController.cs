using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;
using Newtonsoft.Json;
using POLIFRONT_WEB_API.App_Code;

namespace POLIFRONT_WEB_API.Controllers
{
    [RoutePrefix("api/migrant")]
    public class MigrantController : ApiController
    {
        [HttpPost]
        [Route("nouveauMigrant")]
        public HttpResponseMessage nouveauMigrant([FromBody] migrant migrant)
        {
            try
            {
                var response = new HttpResponseMessage(HttpStatusCode.OK);

                Dictionary<string, string> dict = migrant.addMigrant(migrant);

                if (dict == null)
                {
                    ApiObject apiObject = new ApiObject();
                    apiObject.status = "error";
                    apiObject.content = "Error connecting to database. See console.";
                    response.Content = new StringContent(JsonConvert.SerializeObject(apiObject));
                }
                else if (dict.Count == 0)
                {
                    ApiObject apiObject = new ApiObject();
                    apiObject.status = "error";
                    apiObject.content = "No migrant was created";
                    response.Content = new StringContent(JsonConvert.SerializeObject(apiObject));
                }
                else
                {
                    ApiObject apiObject = new ApiObject();
                    apiObject.status = "success";
                    apiObject.content = dict;
                    response.Content = new StringContent(JsonConvert.SerializeObject(apiObject));
                }

                response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                return response;

            }
            catch(Exception e)
            {
                var response = new HttpResponseMessage(HttpStatusCode.BadGateway);
                response.Content = new StringContent(JsonConvert.SerializeObject(e));
                response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                return response;
            }
        }
    }
}