using System.Diagnostics;
using System.Security.Cryptography;
using TravelInsuranceAuction.Data;
using Microsoft.AspNetCore.SignalR;
using TravelInsuranceAuction.Hubs;

namespace TravelInsuranceAuction.Services
{
    public class PriceDecreaseService : BackgroundService
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly IHubContext<PriceHub> _hubContext;

        public PriceDecreaseService(IServiceScopeFactory scopeFactory, IHubContext<PriceHub> hubContext)
        {
            _scopeFactory = scopeFactory;
            _hubContext = hubContext;
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

                            if (offer.LastPriceDecrease == null)
                            {
                                offer.LastPriceDecrease = DateTime.Now;
                                continue;
                            }

                            var timeSinceLast = DateTime.Now - offer.LastPriceDecrease.Value;

                            if (timeSinceLast.Minutes < autoBid.LoweringTime)
                                continue;


                            var newPrice = offer.CurrentPrice - (offer.CurrentPrice * autoBid.PriceDecrease / 100);

                            offer.LastPriceDecrease = DateTime.Now;
                            if (newPrice >= autoBid.DefaultMinPrice)
                                offer.CurrentPrice = newPrice;

                            await _hubContext.Clients.All.SendAsync(
                            "PriceUpdated",
                            offer.Id,
                            offer.CurrentPrice
                            );

                        }
                    }

                    context.SaveChanges();
                }

                await Task.Delay(5000, stoppingToken);
            }
        }
    }
}
