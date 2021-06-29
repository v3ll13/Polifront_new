using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;
using System.Web.Http.Cors;
using Newtonsoft.Json;
using POLIFRONT_WEB_API.App_Code;
//using POLIFRONT_WEB_API.Models;

namespace POLIFRONT_WEB_API.Controllers
{
    [RoutePrefix("api/user")]
    [EnableCors(origins: "http://localhost:61734", headers: "*", methods: "*")]
    public class UserController : ApiController
    {
        [HttpGet]
        [Route("findusers")]
        public HttpResponseMessage findAll()
        {
            try
            {
                var response = new HttpResponseMessage(HttpStatusCode.OK);

                List<utilisateur> users = utilisateur.findUsers();

                if (users == null)
                {
                    ApiObject apiObject = new ApiObject();
                    apiObject.status = "error";
                    apiObject.content = "cannot connect to database";
                    response.Content = new StringContent(JsonConvert.SerializeObject(apiObject));
                }
                else if (users.Count < 1)
                {
                    ApiObject apiObject = new ApiObject();
                    apiObject.status = "error";
                    apiObject.content = "no user found";
                    response.Content = new StringContent(JsonConvert.SerializeObject(apiObject));
                    //response.Content = new StringContent(JsonConvert.SerializeObject(user));
                }
                else
                {
                    ApiObject apiObject = new ApiObject();
                    apiObject.status = "success";
                    apiObject.content = users;
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

        [HttpGet]
        [Route("find/{id}")]
        public HttpResponseMessage find(int id)
        {
            try
            {
                var response = new HttpResponseMessage(HttpStatusCode.OK);

                utilisateur user = utilisateur.find(id);

                if (user == null)
                {
                    ApiObject apiObject = new ApiObject();
                    apiObject.status = "error";
                    apiObject.content = "Cannot connect";
                    response.Content = new StringContent(JsonConvert.SerializeObject(apiObject));
                }
                else if (user.id_utilisateurs == 0)
                {
                    ApiObject apiObject = new ApiObject();
                    apiObject.status = "error";
                    apiObject.content = "No user found";
                    response.Content = new StringContent(JsonConvert.SerializeObject(apiObject));
                }
                else
                {
                    ApiObject apiObject = new ApiObject();
                    apiObject.status = "success";
                    apiObject.content = user;
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
      
        [Route("create")]

        public HttpResponseMessage create( utilisateur utilisateur)
        {
            try
            {

                var response = new HttpResponseMessage(HttpStatusCode.OK);

                int result = utilisateur.createUser(utilisateur);
                if (result < 0)
                {
                    ApiObject apiObject = new ApiObject();
                    apiObject.status = "error";
                    apiObject.content = "Cannot connect to database. See console log.";
                    response.Content = new StringContent(JsonConvert.SerializeObject(apiObject));
                }
                else if (result == 0)
                {
                    ApiObject apiObject = new ApiObject();
                    apiObject.status = "error";
                    apiObject.content = "No user has been inserted.";
                    response.Content = new StringContent(JsonConvert.SerializeObject(apiObject));
                }
                else
                {
                    ApiObject apiObject = new ApiObject();
                    apiObject.status = "success";
                    apiObject.content = "User inserted successfully.";
                    response.Content = new StringContent(JsonConvert.SerializeObject(apiObject));
                }

                //mdc.utilisateurs.Add(utilisateur);
                //mdc.SaveChanges();

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
        [Route("delete/{id}")]
        public HttpResponseMessage delete(int id)
        {
            try
            {
                var response = new HttpResponseMessage(HttpStatusCode.OK);

                //mdc.utilisateurs.Remove(mdc.utilisateurs.SingleOrDefault(u => u.id_utilisateurs == id));
                //mdc.SaveChanges();

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

        [HttpPut]

        [Route("update")]
        public HttpResponseMessage update(utilisateur utilisateur)
        {
            //a user should be able to modify:
            //email, password
            try
            {
                var response = new HttpResponseMessage(HttpStatusCode.OK);

                //var currUser = mdc.utilisateurs.SingleOrDefault(u => u.id_utilisateurs == utilisateur.id_utilisateurs);
                //if(currUser.dat2naiss_utils != utilisateur.dat2naiss_utils)
                //{
                //    currUser.dat2naiss_utils = utilisateur.dat2naiss_utils;
                //}
                //if(currUser.email_utils != utilisateur.email_utils)
                //{
                //    currUser.email_utils = utilisateur.email_utils;
                //}
                ////and so on...
                //mdc.SaveChanges();
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
    }
}
