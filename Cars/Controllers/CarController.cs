using Cars.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

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

        

        //[HttpPost]
        //[Route("GetCarsFromYear")]
        //public async Task<List<Car>> GetCarsFromYear(ControllerParams selected)
        //{
        //    return await db.GetCarsFromYear(selected.year);
        //}



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

    }
}
