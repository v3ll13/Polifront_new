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
    [RoutePrefix("api/dash")]
    public class DashController : ApiController
    {
        [HttpGet]
        [Route("getstat")]
        public HttpResponseMessage getStat()
        {
            try
            {
                var response = new HttpResponseMessage(HttpStatusCode.OK);

                List<StatObj> stats = StatObj.getStat();
                if (stats.Count > 0)
                {
                    ApiObject apiObject = new ApiObject();
                    apiObject.status = "success";
                    apiObject.content = stats;
                    response.Content = new StringContent(JsonConvert.SerializeObject(apiObject));
                }
                else
                {
                    ApiObject apiObject = new ApiObject();
                    apiObject.status = "error";
                    apiObject.content = "Error connecting to database. See console.";
                    response.Content = new StringContent(JsonConvert.SerializeObject(apiObject));
                }

                response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                return response;
            }
            catch (Exception e)
            {
                var response = new HttpResponseMessage(HttpStatusCode.BadGateway);
                response.Content = new StringContent(JsonConvert.SerializeObject(e));
                response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                return response;
            }
        }

        // GET api/<controller>/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>
        public void Post([FromBody] string value)
        {
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