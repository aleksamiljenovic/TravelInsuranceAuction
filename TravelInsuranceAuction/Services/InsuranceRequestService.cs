using Azure.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.CodeAnalysis.FlowAnalysis.DataFlow;
using System.Security.Claims;
using TravelInsuranceAuction.Data;
using TravelInsuranceAuction.Hubs;
using TravelInsuranceAuction.Models;
using TravelInsuranceAuction.Repository.IRepository;
using TravelInsuranceAuction.Utility;
using TravelInsuranceAuction.ViewModels;

namespace TravelInsuranceAuction.Services
{
    public class InsuranceRequestService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHubContext<PriceHub> _hubContext;

        public InsuranceRequestService(IUnitOfWork unitOfWork, IHubContext<PriceHub> hubContext)
        {
            _hubContext = hubContext;
            _unitOfWork = unitOfWork;
        }

        public List<InsuranceRequest> GetActiveUser(string userId)
        {
            return _unitOfWork.InsuranceRequest
            .GetAll()
            .Where(u => u.UserId == userId &&
                   _unitOfWork.Auction.GetAll()
                   .Any(a => a.RequestId == u.Id && a.IsActive))
            .OrderByDescending(u => u.createdAt)
            .ToList();
        }

        public List<InsuranceRequest> GetClosedByUser(string userId)
        {
            return _unitOfWork.InsuranceRequest
            .GetAll()
            .Where(u => u.UserId == userId &&
                   _unitOfWork.Auction.GetAll()
                   .Any(a => a.RequestId == u.Id && a.IsActive == false))
            .OrderByDescending(u => u.createdAt)
            .ToList();
        }

        public async Task CreateAuction(InsuranceRequest obj, string userId)
        {
            obj.UserId = userId;
            _unitOfWork.InsuranceRequest.Add(obj);
            _unitOfWork.Save();

            Auction auction = new Auction
            {
                StartTime = DateTime.Now,
                EndTime = DateTime.Now.AddHours(6),
                IsActive = true,
                RequestId = obj.Id
            };

            _unitOfWork.Auction.Add(auction);
            _unitOfWork.Save();

            var agenciesList = _unitOfWork.Agency.GetAll().ToList();
            foreach (var agency in agenciesList)
            {
                var bid = _unitOfWork.AutoBiddingSetting.Get(u => u.AgencyId == agency.Id);
                if (bid != null)
                {
                    Offer offer = new Offer
                    {
                        InitialPrice = bid.DefaultStartPrice,
                        CurrentPrice = bid.DefaultStartPrice - (bid.DefaultStartPrice * bid.PriceDecrease / 100),
                        Conditions = bid.SpecialConditions,
                        AgencyId = agency.Id,
                        AuctionId = auction.Id
                    };
                    _unitOfWork.Offer.Add(offer);
                    _unitOfWork.Save();
                }
            }

            await _hubContext.Clients.Group("Agencies")
                .SendAsync("AuctionStarted", auction.Id, obj.Destination);
        }

        public AuctionOffersVM GetAuctionOffers(int requestId)
        {
            var request = _unitOfWork.InsuranceRequest.Get(r => r.Id == requestId);
            if (request == null) return null;

            var auction = _unitOfWork.Auction.Get(u => u.RequestId == request.Id);
            if (auction == null) return null;

            var offersVM = _unitOfWork.Offer.GetAll()
                .Where(u => u.AuctionId == auction.Id)
                .Select(o => new OfferVM
                {
                    Id = o.Id,
                    AgencyName = _unitOfWork.Agency.Get(a => a.Id == o.AgencyId)?.Name ?? "Nepoznata agencija",
                    InitialPrice = o.InitialPrice,
                    CurrentPrice = o.CurrentPrice,
                    Conditions = o.Conditions,
                    AuctionId = o.AuctionId
                }).ToList();

            return new AuctionOffersVM
            {
                AuctionId = auction.Id,
                AuctionStartTime = auction.StartTime,
                AuctionEndTime = auction.EndTime,
                Destination = request.Destination,
                Offers = offersVM
            };
        }

        public async Task SelectOffer(int offerId)
        {
            var offer = _unitOfWork.Offer.Get(o => o.Id == offerId);
            var auction = _unitOfWork.Auction.Get(a => a.Id == offer.AuctionId);

            var allOffers = _unitOfWork.Offer.GetAll().Where(o => o.AuctionId == auction.Id).ToList();
            foreach (var o in allOffers)
            {
                o.isWinning = false;
                _unitOfWork.Offer.Update(o);
            }

            offer.isWinning = true;
            auction.IsActive = false;

            _unitOfWork.Auction.Update(auction);
            _unitOfWork.Save();

            await _hubContext.Clients.Group($"auction-{auction.Id}")
                .SendAsync("AuctionFinished", offer.Id);
        }

        public async Task CancelAuction(int auctionId)
        {
            var auction = _unitOfWork.Auction.Get(a => a.Id == auctionId);
            if (auction == null) return;

            auction.IsActive = false;
            _unitOfWork.Auction.Update(auction);
            _unitOfWork.Save();

            await _hubContext.Clients.Group($"auction-{auction.Id}")
                .SendAsync("AuctionFinished", 0);
        }
    }
}
