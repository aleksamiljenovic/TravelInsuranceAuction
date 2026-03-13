using Microsoft.AspNetCore.SignalR;
namespace TravelInsuranceAuction.Hubs
{
    public class PriceHub : Hub
    {
        public async Task JoinAuctionGroup(int auctionId)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, $"auction-{auctionId}");
        }
    }
}
