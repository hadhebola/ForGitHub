using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

using System.Net.Http;
using System.Web.Http;
using OloTest2.CustomClasses;

namespace OloTest2.Controllers
{
    public class OloController : Controller
    {

        public void InitiateCardGeneration()
        {

      //      var JsonReturn = JsonConvert.DeserializeObject<Topping>(response.Content);

            //  return JsonReturn;
        }


       // [HttpPost]
        [System.Web.Http.Route("test/olo")]
        public void LoadJson()
        {
            using (StreamReader r = new StreamReader("pizzas.json"))
            {
                string json = r.ReadToEnd();
                List<Topping> items = JsonConvert.DeserializeObject<List<Topping>>(json);
               // return Request.CreateResponse(HttpStatusCode.OK, items);
                
               // return ;
            }
        }
    }


   





}