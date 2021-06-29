using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using ModularWebApp.Models;
using Newtonsoft.Json;
using System.Text.Json;


namespace ModularWebApp.Controllers
{
    public class HomeController : Controller
    {


       //settings for the api
       Uri Baseurl = new Uri ("https://localhost:44327/api");
        
        HttpClient client;

        public HomeController()
        {
            client = new HttpClient();
            client.BaseAddress = Baseurl;
        }

        //Controller to select everithing in the api 
        public ActionResult LutUser()
        {
            ViewBag.Message = "Your LookUp Table Page.";
            List<LutUsers> lutUsers = new List<LutUsers>();
            HttpResponseMessage response = client.GetAsync(client.BaseAddress + "/user/findusers").Result;
            //Checking the response is successful or not which is sent using HttpClient  
            if (response.IsSuccessStatusCode)
            {
                    //Storing the response details recieved from web api   
                
                   // Response.Write("<script>alert('Connection Made successfully!');</script>");                               
                    string data = response.Content.ReadAsStringAsync().Result;
                    var rootObject = JsonConvert.DeserializeObject<Rootobject>(data);
                    lutUsers = rootObject.Content;
                    //Response.Write(lutUsers);


            } else
             {             

                    Response.Write("<script>alert('Connection not made!');</script>");
             }
                //returning the employee list to view  
                return View(lutUsers);

        }

        public ActionResult Create()
        {

            return View();
        }

        [HttpPost]
        public ActionResult Create( LutUserAdd users)
        {
            var PostJob = client.PostAsJsonAsync<LutUserAdd>(client.BaseAddress + "/user/create", users);
            PostJob.Wait();
            var response = PostJob.Result;
            if (response.IsSuccessStatusCode)
            {
                //Storing the response details recieved from web api   
               
                return RedirectToAction("LutUser");


            }
            Response.Write("<script>alert('Data Inserted successfully!');</script>");
            return View(users);
        }


        public ActionResult Delete(int id)
        {
            var DelJob = client.DeleteAsync(client.BaseAddress + "/user/delete/" + id.ToString());
            var response = DelJob.Result;      
            if (response.IsSuccessStatusCode)
            {
                //Storing the response details recieved from web api   
                Response.Write("<script>alert('Data Deleted successfully!');</script>");
                //return RedirectToAction("LutUser");


            }
            else
            {

                Response.Write("<script>alert('Connection not made!');</script>");
            }

            return RedirectToAction("LutUser");
        }

        public ActionResult Index()
        {
            return View();
        }


            public ActionResult Test()
            {
                return View();
            }


            public ActionResult About()
            {
                ViewBag.Message = "Your application description page.";



                return View();
            }

            public ActionResult Contact()
            {
                ViewBag.Message = "Your contact page.";

                return View();
            }







    }








}
