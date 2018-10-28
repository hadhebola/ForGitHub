using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Newtonsoft.Json;
using Microsoft.Ajax.Utilities;
using OloTest2.CustomClasses;

namespace OloTest2.Controllers
{
    public class OloTestController : ApiController
    {
        [HttpGet]
        [Route("test/olo")]
        public HttpResponseMessage LoadJson()
        {
           
            using (var w = new WebClient())
            {
                var json_data = string.Empty;
                // attempt to download JSON data as a string
                try
                {
                    json_data = w.DownloadString("http://files.olo.com/pizzas.json");
                }
                catch (Exception) { }
            
                List<Topping> items = JsonConvert.DeserializeObject<List<Topping>>(json_data);
          

                List<ToppingCount2> TopingsList = new List<ToppingCount2>();
           

                // the below is getting the list of toppings
                foreach (Topping h in items)
                {
                    
                    for( int i=0; i< h.toppings.Count-1; i++)
                    {
                        if (TopingsList.Count == 0)
                        {
                            var ToppingCountObj = new ToppingCount2();
                            ToppingCountObj.ToppingItem = h.toppings[i];
                            ToppingCountObj.Count++;
                            TopingsList.Add(ToppingCountObj);
                        }
                        else
                        {                         

                            var has = TopingsList.Any(x => x.ToppingItem == h.toppings[i]);
                               
                            if ( has == false )
                                {
                                    var ToppingCountObj = new ToppingCount2();
                                    ToppingCountObj.ToppingItem = h.toppings[i];
                                ToppingCountObj.Count++;
                                TopingsList.Add(ToppingCountObj);
                            }
                                else if(has)
                                    {
                                int index = TopingsList.FindIndex(ind => ind.ToppingItem.Equals(h.toppings[i]));
                                if (index > -1)
                                {
                                    TopingsList[index].Count = TopingsList[index].Count +1;
                                }                                  

                            }
                            }
                        }
                    }


                List<ToppingCount2> TopingsFinalValue = new List<ToppingCount2>();
                var sortedToppings = TopingsList.OrderByDescending(ord => ord.Count);
                
                int c = 1;
                foreach (var srt  in sortedToppings)
                {
                    
                    ToppingCount2 ToppingCount2Obj = new ToppingCount2();
                    ToppingCount2Obj.Count = srt.Count;
                    ToppingCount2Obj.ToppingItem = srt.ToppingItem;
                    ToppingCount2Obj.Rank = c;
                    TopingsFinalValue.Add(ToppingCount2Obj);
                    c++;

                }


                return Request.CreateResponse(HttpStatusCode.OK, TopingsFinalValue.Take(20));
            }



                            
            }
        }
    }



  


