using Cars.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Bing;

namespace Cars.Controllers
{   
    [RoutePrefix("api/Car")]
    public class CarController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public class ControllerParams
        {
            public string year { get; set; }
            public string make { get; set; }
            public string model { get; set; }
            public string trim { get; set; }
            public string filter { get; set; }
            public bool? paging { get; set; }
            public int? page { get; set; }
            public int? perPage { get; set; }
        }

        public class IdParam
        {
            public int id { get; set; }
        }


        [HttpPost]
        [Route("GetCars")]
        public async Task<List<Car>> GetCars(ControllerParams selected)
        {
            return await db.GetCars(selected.year, selected.make, selected.model, selected.trim, 
                                    selected.filter, selected.paging, selected.page, selected.perPage);
        }

        [HttpPost]
        [Route("GetYears")]
        public async Task<List<string>> GetYears()
        {
            return await db.GetYears();
        }
        
        [HttpPost]
        [Route("GetMakes")]
        public async Task<List<string>> GetMakes(ControllerParams selected)
        {
            return await db.GetMakes(selected.year);
        }
        

        [HttpPost]
        [Route("GetModels")]
        public async Task<List<string>> GetModels(ControllerParams selected)
        {
            return await db.GetModels(selected.year, selected.make);
        }

        [HttpPost]
        [Route("GetTrims")]
        public async Task<List<string>> GetTrims(ControllerParams selected)
        {
            return await db.GetTrims(selected.year, selected.make, selected.model);
        }

        [HttpPost]
        [Route("GetCarsCount")]
        public async Task<int> GetCarsCount(ControllerParams selected)
        {
            return await db.GetCarsCount(selected.year, selected.make, selected.model, selected.trim,
                                    selected.filter);

        }


        [HttpGet, HttpPost]
        [Route("GetCarDetails")]
        public async Task<IHttpActionResult> GetCarDetails(IdParam Id)
        {
            HttpResponseMessage response;
            string content = "";
            var car = new CarViewModel
            {
                Car = db.Cars.Find(Id.id),
                Recalls = "",
                Image = ""
            };

            using(var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://nhtsa.gov/");
                try
                {
                    response = await client.GetAsync("webapi/api/Recalls/vehicle/modelyear/"+car.Car.model_year
                        +"/make/"+car.Car.make+"/model/"+car.Car.model_name+ "?format=json");
                    content = await response.Content.ReadAsStringAsync();
                }
                catch(Exception e)
                {
                    return InternalServerError(e);
                }
            }
            car.Recalls = content;

            var image = new BingSearchContainer(new Uri("https://api.datamarket.azure.com/Bing/search/"));

            image.Credentials = new NetworkCredential("accountKey", "dwmFt1J2rM45AQXkGTk4ebfcVLNcytTxGMHK6dgMreg");
            var marketData = image.Composite("image", car.Car.model_year + " " + car.Car.make + " " + car.Car.model_name + " " + car.Car.model_trim,
                null, null, null, null, null, null, null, null, null, null, null, null, null).Execute();
            car.Image = marketData.First().Image.First().MediaUrl;

            return Ok(car);
        }
    }
}
