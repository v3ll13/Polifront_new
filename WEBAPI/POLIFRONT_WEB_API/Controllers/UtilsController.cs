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
    [RoutePrefix("api/utils")]
    public class UtilsController : ApiController
    {
        [HttpGet]
        [Route("gettypecas")]
        public HttpResponseMessage getTypeCas()
        {
            try
            {
                var response = new HttpResponseMessage(HttpStatusCode.OK);
                
                List<string> tables = Utils.tableList;

                if(tables == null)
                {
                    ApiObject apiObject = new ApiObject();
                    apiObject.status = "error";
                    apiObject.content = "cannot connect to database";
                    response.Content = new StringContent(JsonConvert.SerializeObject(apiObject));
                }
                else if (tables.Count == 0)
                {
                    ApiObject apiObject = new ApiObject();
                    apiObject.status = "error";
                    apiObject.content = "No data found";
                    response.Content = new StringContent(JsonConvert.SerializeObject(apiObject));
                }
                else
                {
                    ApiObject apiObject = new ApiObject();
                    apiObject.status = "success";
                    apiObject.content = tables;
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
                Console.WriteLine(response);
                return response;
            }
        }
    }
}