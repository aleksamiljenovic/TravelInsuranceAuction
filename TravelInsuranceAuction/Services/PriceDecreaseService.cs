using System.Diagnostics;
using System.Security.Cryptography;
using TravelInsuranceAuction.Data;

namespace TravelInsuranceAuction.Services
{
    public class PriceDecreaseService : BackgroundService
    {
        private readonly IServiceScopeFactory _scopeFactory;

        public PriceDecreaseService(IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                using (var scope = _scopeFactory.CreateScope())
                {
                    var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

                    var auctions = context.Auctions
                        .Where(a => a.IsActive && a.EndTime > DateTime.Now)
                        .ToList();

                    foreach (var auction in auctions)
                    {
                        var offers = context.Offers
                            .Where(o => o.AuctionId == auction.Id)
                            .ToList();

                        foreach (var offer in offers)
                        {
                            var autoBid = context.AutoBiddingSettings
                                .FirstOrDefault(a => a.AgencyId == offer.AgencyId);

                            if (autoBid == null)
                                continue;

                            //var timeSinceLast = DateTime.Now - offer.LastPriceDecrease;
                            //if (timeSinceLast.TotalSeconds < autoBid.IntervalSeconds)
                            //    continue; // još nije vreme za sledeće smanjenje


                            var newPrice = offer.CurrentPrice - autoBid.PriceDecrease;

                            //offer.LastPriceDecrease = DateTime.Now;
                            if (newPrice >= autoBid.DefaultMinPrice)
                                offer.CurrentPrice = newPrice;
                            
                        }
                    }

                    context.SaveChanges();
                }

                await Task.Delay(5000, stoppingToken);
            }
        }
    }
}
