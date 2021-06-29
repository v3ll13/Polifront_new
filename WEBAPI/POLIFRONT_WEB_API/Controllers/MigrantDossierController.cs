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
//using static POLIFRONT_WEB_API.Controllers.DossierController;

namespace POLIFRONT_WEB_API.Controllers
{
    [RoutePrefix("api/migrantdossier")]
    public class MigrantDossierController : ApiController
    {
        [HttpPost]
        [Route("ajoutermigrant")]
        public HttpResponseMessage ajouter([FromBody] migrant_dossier migrant)
        {
            try
            {
                var response = new HttpResponseMessage(HttpStatusCode.OK);

                //Dictionary<string, string> dict = migrant_dossier.addMigrant(migrant);
                Dictionary<string, string> dict = migrant_dossier.fillMigrantInfo(migrant);
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
                    apiObject.content = "No migrant was added";
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

        [HttpPost]
        [Route("nouveauDossier")]
        public HttpResponseMessage nouveauDossier([FromBody] utilisateur user)
        {
            try
            {
                var response = new HttpResponseMessage(HttpStatusCode.OK);

                //Dictionary<string, string> dict = migrant_dossier.addMigrant(migrant);
                Dictionary<string, string> dict = migrant_dossier.nouveauDossier(user.id_utilisateurs.ToString(), user.affectation);
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
                    apiObject.content = "No dossier was created";
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

        [HttpPost]
        [Route("getmigrants")]
        public HttpResponseMessage getMigrants([FromBody] request request)
        {
            try
            {
                var response = new HttpResponseMessage(HttpStatusCode.OK);

                List<migrant_dossier> migrants = migrant_dossier.getMigrants(request.id, request.offset, request.max);
                if(migrants == null)
                {
                    ApiObject apiObject = new ApiObject();
                    apiObject.status = "error";
                    apiObject.content = "Error connecting to database. See console.";
                    response.Content = new StringContent(JsonConvert.SerializeObject(apiObject));
                }
                else if(migrants.Count == 0)
                {
                    ApiObject apiObject = new ApiObject();
                    apiObject.status = "error";
                    apiObject.content = "No migrant found";
                    response.Content = new StringContent(JsonConvert.SerializeObject(apiObject));
                }
                else
                {
                    ApiObject apiObject = new ApiObject();
                    apiObject.status = "success";
                    apiObject.content = migrants;
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

        [HttpGet]
        [Route("getmigrants/{id}")]
        public HttpResponseMessage getMigrants(string ID)
        {
            try
            {
                var response = new HttpResponseMessage(HttpStatusCode.OK);

                List<migrant_dossier> migrants = migrant_dossier.getMigrants(ID, 0, 1);
                if(migrants == null)
                {
                    ApiObject apiObject = new ApiObject();
                    apiObject.status = "error";
                    apiObject.content = "Error connecting to database. See console.";
                    response.Content = new StringContent(JsonConvert.SerializeObject(apiObject));
                }
                else if(migrants.Count == 0)
                {
                    ApiObject apiObject = new ApiObject();
                    apiObject.status = "error";
                    apiObject.content = "No migrant found";
                    response.Content = new StringContent(JsonConvert.SerializeObject(apiObject));
                }
                else
                {
                    ApiObject apiObject = new ApiObject();
                    apiObject.status = "success";
                    apiObject.content = migrants;
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

        [HttpPost]
        [Route("closedossier")]
        public HttpResponseMessage closeEditDossier([FromBody] migrant_dossier migrant)
        {
            try
            {
                var response = new HttpResponseMessage(HttpStatusCode.OK);

                int removed = migrant_dossier.closeEditDossier(migrant.id_mig_interp_doss);

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
                    apiObject.content = "Could not close folder.";
                    response.Content = new StringContent(JsonConvert.SerializeObject(apiObject));
                }
                else
                {
                    ApiObject apiObject = new ApiObject();
                    apiObject.status = "success";
                    apiObject.content = "Folder has been closed successfully.";
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

        [HttpDelete]
        [Route("removedraft/{id}")]
        public HttpResponseMessage Delete(int id)
        {
            try
            {
                var response = new HttpResponseMessage(HttpStatusCode.OK);

                int removed = migrant_dossier.removeDraftDossier(id);

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

        public class request
        {
            public string id { get; set; }
            public int offset { get; set; }
            public int max { get; set; }
        }
    }
}