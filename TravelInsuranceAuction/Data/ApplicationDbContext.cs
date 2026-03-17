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
        public DbSet<IncuranceAgency> Agencies { get; set; }
        public DbSet<AutoBiddingSetting> AutoBiddingSettings { get; set; }
        public DbSet<Auction> Auctions { get; set; }
        public DbSet<Offer> Offers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<IncuranceAgency>().HasData(
                new IncuranceAgency
                {
                    Id = 1,
                    Name = "Generali",
                    City = "Beograd",
                    PhoneNumber = "1234567890",
                    StreetAddress = "Kneza Milosa 18"
                },
                new IncuranceAgency
                {
                    Id = 2,
                    Name = "Dunav osiguranje",
                    City = "Beograd",
                    PhoneNumber = "1234567890",
                    StreetAddress = "Nehruova 44"
                },
                new IncuranceAgency
                {
                    Id = 3,
                    Name = "Wiener",
                    City = "Beograd",
                    PhoneNumber = "1234567890",
                    StreetAddress = "Milutina Milankovica 23"
                }
                );

            modelBuilder.Entity<AutoBiddingSetting>().HasData(
                new AutoBiddingSetting
                {
                    Id = 1,
                    DefaultStartPrice = 90,
                    DefaultMinPrice = 50,
                    LoweringTime = 20,
                    PriceDecrease = 10,
                    SpecialConditions = "Kašnjenje letova",
                    AgencyId = 1
                },
                new AutoBiddingSetting
                {
                    Id = 2,
                    DefaultStartPrice = 80,
                    DefaultMinPrice = 40,
                    LoweringTime = 30,
                    PriceDecrease = 15,
                    SpecialConditions = "",
                    AgencyId = 2
                },
                 new AutoBiddingSetting
                 {
                     Id = 3,
                     DefaultStartPrice = 75,
                     DefaultMinPrice = 35,
                     LoweringTime = 10,
                     PriceDecrease = 5,
                     SpecialConditions = "",
                     AgencyId = 3
                 }
                );

        }
    }
}