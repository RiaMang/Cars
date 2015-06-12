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
            public string year;
            public string make;
            public string model;
            public string trim;
        }

        public class Paging
        {
            public string page;
            public string perpage;
        }

        public class Fil
        {
            public string filter;
            public Paging paging;
        }

        [HttpPost]
        [Route("GetCarsFromYear")]
        public async Task<List<Car>> GetCarsFromYear(ControllerParams selected)
        {
            return await db.GetCarsFromYear(selected.year);
        }



        [HttpPost]
        [Route("GetCars")]
        public async Task<List<Car>> GetCars(ControllerParams selected)
        {
            return await db.GetCars(selected.year, selected.make, selected.model, selected.trim);
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
        [Route("SelectPagedCars")]
        public async Task<List<Car>> SelectPagedCars(Paging paging )
        {
           
             return await db.SelectPagedCars(paging.page, paging.perpage);
            
            
        }

        [HttpPost]
        [Route("GetCarCount")]
        public async Task<List<int>> GetCarCount(string filter)
        {

            return await db.GetCarCount(filter);

        }

        [HttpPost]
        [Route("GetAllFilCars")]
        public async Task<List<Car>> GetAllFilCars(Fil fil)
        {

            return await db.GetAllFilCars(fil.filter, fil.paging.page, fil.paging.perpage);


        }

    }
}
