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
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<Agency> Agencies { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<InsuranceRequest>().HasData(
                 new InsuranceRequest
                 {
                     Id = 1,
                     Destination = "London",
                     NumberOfTravelers = 2,
                     StartDate = DateOnly.FromDateTime(DateTime.Today),
                     EndDate = DateOnly.FromDateTime(DateTime.Today)
                 },
                     new InsuranceRequest
                     {
                         Id = 2,
                         Destination = "Paris",
                         NumberOfTravelers = 2,
                         StartDate = DateOnly.FromDateTime(DateTime.Today),
                         EndDate = DateOnly.FromDateTime(DateTime.Today)
                     },
                      new InsuranceRequest
                      {
                          Id = 3,
                          Destination = "Lisabon",
                          NumberOfTravelers = 4,
                          StartDate = DateOnly.FromDateTime(DateTime.Today),
                          EndDate = DateOnly.FromDateTime(DateTime.Today)
                      }
                );

            modelBuilder.Entity<Agency>().HasData(
                new Agency
                {
                    Id = 1,
                    Name = "ArgusTours",
                    City = "Beograd",
                    PhoneNumber = "1234567890",
                    StreetAddress = "Kneza Milosa 18"
                },
                new Agency
                {
                    Id = 2,
                    Name = "VivaTravel",
                    City = "Beograd",
                    PhoneNumber = "1234567890",
                    StreetAddress = "Nehruova 44"
                },
                new Agency
                {
                    Id = 3,
                    Name = "Travellino",
                    City = "Beograd",
                    PhoneNumber = "1234567890",
                    StreetAddress = "Milutina Milankovica 23"
                }
                );

        }
    }
}
