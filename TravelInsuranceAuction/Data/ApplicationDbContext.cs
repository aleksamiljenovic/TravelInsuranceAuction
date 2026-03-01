using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Reflection.Emit;
using System.Security.Cryptography.Xml;
using TravelInsuranceAuction.Models;

namespace TravelInsuranceAuction.Data
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        public DbSet<InsuranceRequest> InsuranceRequests { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<InsuranceRequest>().HasData(
                 new InsuranceRequest
                 {
                     RequestId = 1,
                     Destination = "London",
                     NumberOfTravelers = 2,
                     StartDate = DateOnly.FromDateTime(DateTime.Today),
                     EndDate = DateOnly.FromDateTime(DateTime.Today)
                 },
                     new InsuranceRequest
                     {
                         RequestId = 2,
                         Destination = "Paris",
                         NumberOfTravelers = 2,
                         StartDate = DateOnly.FromDateTime(DateTime.Today),
                         EndDate = DateOnly.FromDateTime(DateTime.Today)
                     },
                      new InsuranceRequest
                      {
                          RequestId = 3,
                          Destination = "Lisabon",
                          NumberOfTravelers = 4,
                          StartDate = DateOnly.FromDateTime(DateTime.Today),
                          EndDate = DateOnly.FromDateTime(DateTime.Today)
                      }
                );

        }
    }
}
