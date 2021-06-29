using Newtonsoft.Json;
using POLIFRONT_WEB_API.App_Code;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;

namespace POLIFRONT_WEB_API.Controllers
{
    [RoutePrefix("api/acc_circ")]
    public class AccCircController : ApiController
    {
        // GET api/<controller>
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<controller>/5
        public string Get(int id)
        {
            return "value";
        }

        [HttpPost]
        [Route("ajouteracc_circ")]
        public HttpResponseMessage ajouteracc_circ([FromBody] acc_circ acc)
        {
            try
            {
                var response = new HttpResponseMessage(HttpStatusCode.OK);

                int result = acc_circ.addAccCirc(acc);

                if (result < 0)
                {
                    ApiObject apiObject = new ApiObject();
                    apiObject.status = "error";
                    apiObject.content = "Error connecting to database. See console.";
                    response.Content = new StringContent(JsonConvert.SerializeObject(apiObject));
                }
                else if (result == 0)
                {
                    ApiObject apiObject = new ApiObject();
                    apiObject.status = "error";
                    apiObject.content = "No acc_circ was added";
                    response.Content = new StringContent(JsonConvert.SerializeObject(apiObject));
                }
                else
                {
                    ApiObject apiObject = new ApiObject();
                    apiObject.status = "success";
                    apiObject.content = "AccCirc successfully added.";
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

        // PUT api/<controller>/5
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}