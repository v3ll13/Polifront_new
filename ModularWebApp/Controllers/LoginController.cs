using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using ModularWebApp.Models;
using Newtonsoft.Json.Linq;
using RestSharp;

namespace ModularWebApp.Controllers
{
    public class LoginController : Controller
    {

        //settings for the api
        Uri Baseurl = new Uri("https://localhost:44327/api");

        HttpClient client;

        public LoginController()
        {
            HttpClientHandler httpClientHandler = new HttpClientHandler()
            {
                Credentials = new NetworkCredential("username", "****"),
            };
            client = new HttpClient(httpClientHandler);
            client.BaseAddress = Baseurl;
        }
        // GET: Login
        
        public ActionResult ConfirmLogin()
        {
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

      
        [HttpPost]
        public ActionResult Login(lutLogin login)

        {

            var client = new RestClient("https://localhost:44327/");

            var request = new RestRequest("api/auth/login");
            request.AddHeader("Content-Type", "application/json");
            var email = login.email_utils;
            var password = login.mot2pass_utils_defaut;
             
            request.AddJsonBody(new
            {
                email = email,
                password = password
            });

            var response = client.Post(request);
            var content = response.Content;
            if (content != "{\"content\":\"wrong email/password\",\"status\":\"error\"}")
            {
                Response.Write("<script>alert('Success');</script>");
                return RedirectToAction("Index", "Home");
            }
            else
            {
                Response.Write("wrong Email/Password");
              

            }

            return View();
        }



        [HttpPost]
        public async Task<ActionResult> LoginAsync(lutLogin login)

        {
            
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            //setup login data
            var username = login.email_utils;
            var password = login.mot2pass_utils_defaut;
            var formContent = new FormUrlEncodedContent(new[]
             {
                 new KeyValuePair<string, string>("grant_type", "password"),
                 new KeyValuePair<string, string>("username", username),
                 new KeyValuePair<string, string>("password", password),
             });
           
            var items = new Tuple<string, string>(username,password);
            var test = formContent;


            //var PostJob = client.PostAsJsonAsync(client.BaseAddress + "/auth/login", formContent);
            //PostJob.Wait()
            HttpResponseMessage responseMessage = await client.PostAsJsonAsync(client.BaseAddress + "/auth/login", items);
            var responseJson = await responseMessage.Content.ReadAsStringAsync();
            var jObject = JObject.Parse(responseJson);
            //var token = jObject.GetValue("access_token").ToString();
            var usernam = jObject.ContainsKey(username);
            
            if (responseJson == "{\"content\":\"wrong email/password\",\"status\":\"error\"}") {
                 Response.Write("<script>alert('wrong Email/Password');</script>");
            }
            else
            {
                return RedirectToAction("ConfirmLogin" +
                    "");

            }
            //formContent.Dispose();
           

            return View();
        }

        public static string UserLogin(string email, string password)
        {
            var client = new RestClient("https://localhost:44327/");

            var request = new RestRequest("api/auth/login");
            request.AddHeader("Content-Type", "application/json");

            request.AddJsonBody(new
            {
                email = email,
                password = password
            });

            var response = client.Post(request);
            var content = response.Content;
            if (content!= "{\"content\":\"wrong email/password\",\"status\":\"error\"}")
            {
                Console.Write("Succesfully Loged In");
            }
            else
            {
                Console.Write("wrong Email/Password");

            }

            return content;



        }



    }
}