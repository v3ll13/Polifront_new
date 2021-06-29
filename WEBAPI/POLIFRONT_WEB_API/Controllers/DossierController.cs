using Newtonsoft.Json;
using POLIFRONT_WEB_API.App_Code;
using POLIFRONT_WEB_API.App_Code.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;

namespace POLIFRONT_WEB_API.Controllers
{
    [RoutePrefix("api/dossier")]
    public class DossierController : ApiController
    {
        [HttpPost]
        [Route("getdossiers")]
        public HttpResponseMessage getDossiers([FromBody] request request)
        {
            try
            {
                var response = new HttpResponseMessage(HttpStatusCode.OK);

                //List<dossier> dossiers = dossier.getDossier(request.id, request.offset, request.max);

                //if (dossiers == null)
                //{
                //    ApiObject apiObject = new ApiObject();
                //    apiObject.status = "error";
                //    apiObject.content = "Error connecting to database. See console.";
                //    response.Content = new StringContent(JsonConvert.SerializeObject(apiObject));
                //}
                //else if (dossiers.Count == 0)
                //{
                //    ApiObject apiObject = new ApiObject();
                //    apiObject.status = "error";
                //    apiObject.content = "No folder was found";
                //    response.Content = new StringContent(JsonConvert.SerializeObject(apiObject));
                //}
                //else
                //{
                //    ApiObject apiObject = new ApiObject();
                //    apiObject.status = "success";
                //    apiObject.content = dossiers;
                //    response.Content = new StringContent(JsonConvert.SerializeObject(apiObject));
                //}
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
        [HttpGet]
        [Route("getdossiers/{id}")]
        public HttpResponseMessage getDossiers(string ID)
        {
            try
            {
                var response = new HttpResponseMessage(HttpStatusCode.OK);

                //List<dossier> dossiers = dossier.getDossier(ID, 0, 1);

                //if (dossiers == null)
                //{
                //    ApiObject apiObject = new ApiObject();
                //    apiObject.status = "error";
                //    apiObject.content = "Error connecting to database. See console.";
                //    response.Content = new StringContent(JsonConvert.SerializeObject(apiObject));
                //}
                //else if (dossiers.Count == 0)
                //{
                //    ApiObject apiObject = new ApiObject();
                //    apiObject.status = "error";
                //    apiObject.content = "No folder was found";
                //    response.Content = new StringContent(JsonConvert.SerializeObject(apiObject));
                //}
                //else
                //{
                //    ApiObject apiObject = new ApiObject();
                //    apiObject.status = "success";
                //    apiObject.content = dossiers;
                //    response.Content = new StringContent(JsonConvert.SerializeObject(apiObject));
                //}
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

        [HttpPost]
        [Route("nouveaudossier")]
        public HttpResponseMessage createDossier([FromBody] utilisateur user)
        {
            try
            {
                var response = new HttpResponseMessage(HttpStatusCode.OK);

                Dictionary<string, string> dict = dossier.nouveauDossier(user.id_utilisateurs.ToString(), user.affectation);

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
                    apiObject.content = "No folder was created";
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
            catch (Exception e)
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

        [HttpDelete]
        [Route("removedraft/{id}")]
        public HttpResponseMessage Delete(int id)
        {
            try
            {
                var response = new HttpResponseMessage(HttpStatusCode.OK);

                int removed = dossier.removeDraftDossier(id);

                if (removed < 0)
                {
                    ApiObject apiObject = new ApiObject();
                    apiObject.status = "error";
                    apiObject.content = "Error connecting to database. See console.";
                    response.Content = new StringContent(JsonConvert.SerializeObject(apiObject));
                }
                else if (removed == 0)
                {
                    ApiObject apiObject = new ApiObject();
                    apiObject.status = "error";
                    apiObject.content = "Could not remove draft.";
                    response.Content = new StringContent(JsonConvert.SerializeObject(apiObject));
                }
                else
                {
                    ApiObject apiObject = new ApiObject();
                    apiObject.status = "success";
                    apiObject.content = "Draft has been removed.";
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

        #region Matching dossier with entities
        [HttpPost]
        [Route("dossierMigrant")]
        public HttpResponseMessage matchWithMigrant([FromBody] matching matching)
        {
            try
            {
                var response = new HttpResponseMessage(HttpStatusCode.OK);

                int result = dossier.matchDossierMigrant(matching.container, matching.content, matching.utilisateur);

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
                    apiObject.content = "Cound not match migrant with dossier";
                    response.Content = new StringContent(JsonConvert.SerializeObject(apiObject));
                }
                else
                {
                    //update dossier status
                    dossier.updateStatus(matching.container, result > 0 ? 1 : 0);
                    ApiObject apiObject = new ApiObject();
                    apiObject.status = "success";
                    apiObject.content = "Match success";
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
        #endregion

        public class request
        {
            public string id { get; set; }
            public int offset { get; set; }
            public int max { get; set; }
        }

        public class matching
        {
            public string container { get; set; }
            public string content { get; set; }
            public int utilisateur { get; set; }
        }
    }
}