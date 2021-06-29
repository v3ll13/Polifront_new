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
    [RoutePrefix("api/auth")]
    public class AuthController : ApiController
    {
        [HttpPost]
        [Route("login")]
        public HttpResponseMessage login([FromBody] BasicAuthObj auth)
        {
            try
            {
                var response = new HttpResponseMessage(HttpStatusCode.OK);

                utilisateur user = utilisateur.login(auth.email, auth.password);
                if (user == null)
                {
                    ApiObject apiObject = new ApiObject();
                    apiObject.status = "error";
                    apiObject.content = "cannot connect to database";
                    response.Content = new StringContent(JsonConvert.SerializeObject(apiObject));
                }
                else if (user.email_utils == null)
                {
                    ApiObject apiObject = new ApiObject();
                    apiObject.status = "error";
                    apiObject.content = "wrong email/password";
                    response.Content = new StringContent(JsonConvert.SerializeObject(apiObject));
                    //response.Content = new StringContent(JsonConvert.SerializeObject(user));
                }
                else
                {
                    ApiObject apiObject = new ApiObject();
                    apiObject.status = "success";
                    apiObject.content = user;

                    user_log log = new user_log();
                    log.id_utilisateurs = user.id_utilisateurs;
                    log.nom_utils = user.nom_utils + " " + user.prenom_utils;
                    user_log.insertUserLogin(log);

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
        [HttpPost]
        [Route("changepassword")]
        public HttpResponseMessage changePassword([FromBody] BasicAuthObj auth)
        {
            try
            {
                var response = new HttpResponseMessage(HttpStatusCode.OK);

                int result = utilisateur.updatePassword(auth.email, auth.password);
                if (result < 0)
                {
                    ApiObject apiObject = new ApiObject();
                    apiObject.status = "error";
                    apiObject.content = "cannot connect to database";
                    response.Content = new StringContent(JsonConvert.SerializeObject(apiObject));
                }
                else if (result == 0)
                {
                    ApiObject apiObject = new ApiObject();
                    apiObject.status = "error";
                    apiObject.content = "Password could not be changed";
                    response.Content = new StringContent(JsonConvert.SerializeObject(apiObject));
                    //response.Content = new StringContent(JsonConvert.SerializeObject(user));
                }
                else
                {
                    ApiObject apiObject = new ApiObject();
                    apiObject.status = "success";
                    apiObject.content = "Password has been changed successfully";
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