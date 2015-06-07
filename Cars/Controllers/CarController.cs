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

        [Route("GetCarsFromYear")]
        public async Task<List<Car>> GetCarsFromYear(string year)
        {
            return await db.GetCarsFromYear(year);
        }


    }
}
