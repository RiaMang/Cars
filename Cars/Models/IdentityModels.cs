using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Cars.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("LocalConnection", throwIfV1Schema: false)
        {
        }

        public DbSet<Car> Cars { get; set; }

        public async Task<List<Car>> GetCarsFromYear(string year)
        {
            var yearParam = new SqlParameter("@year", year);
            return await this.Database
                .SqlQuery<Car>("GetCarsFromYear @year", yearParam).ToListAsync();
        }

        public async Task<List<string>> GetYears()
        {
            
            return await this.Database
                .SqlQuery<string>("GetYears").ToListAsync();
        }

        public async Task<List<string>> GetMakes(string year)
        {
            var yearParam = new SqlParameter("@year", year);
            return await this.Database
                .SqlQuery<string>("GetMakes @year", yearParam).ToListAsync();
        }

        public async Task<List<string>> GetModels(string year, string make)
        {
            var yearParam = new SqlParameter("@year", year);
            var makeParam = new SqlParameter("@make", make);
            return await this.Database
                .SqlQuery<string>("GetModels @year,@make", yearParam, makeParam).ToListAsync();
        }

        public async Task<List<string>> GetTrims(string year, string make, string model)
        {
            var yearParam = new SqlParameter("@year", year);
            var makeParam = new SqlParameter("@make", make);
            var modelParam = new SqlParameter("@model", model);
            return await this.Database
                .SqlQuery<string>("GetTrims @year,@make,@model", yearParam, makeParam, modelParam).ToListAsync();
        }

        public async Task<List<Car>> GetCars(string year, string make, string model, string trim)
        {
            var yearParam = new SqlParameter("@year", year);
            var makeParam = new SqlParameter("@make", make);
            var modelParam = new SqlParameter("@model", model);
            var trimParam = new SqlParameter("@trim", trim);
            return await this.Database
                .SqlQuery<Car>("GetCars @year,@make,@model,@trim", yearParam, makeParam, modelParam, trimParam).ToListAsync();
        }

        public async Task<List<Car>> SelectPagedCars(string page, string perpage)
        {
            var pageParam = new SqlParameter("@page", page);
            var perpageParam = new SqlParameter("@perpage", perpage);
            return await this.Database
                .SqlQuery<Car>("SelectPagedCars @page,@perpage", pageParam, perpageParam).ToListAsync();
        }

        public async Task<List<int>> GetCarCount(string filter)
        {
            var filterParam = new SqlParameter("@filter", filter);
           
            return await this.Database
                .SqlQuery<int>("GetCarCount @filter", filterParam).ToListAsync();
        }

        public async Task<List<Car>> GetAllFilCars(string filter, string page, string perpage)
        {
            var filterParam = new SqlParameter("@filter", filter);
            var pageParam = new SqlParameter("@page", page);
            var perpageParam = new SqlParameter("@perpage", perpage);
            return await this.Database
                .SqlQuery<Car>("GetAllFilCars @filter,@page,@perpage", filterParam, pageParam, perpageParam).ToListAsync();
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}