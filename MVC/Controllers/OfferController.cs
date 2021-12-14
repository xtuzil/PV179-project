using BL.DTOs;
using BL.Exceptions;
using BL.Facades;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MVC.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MVC.Controllers
{
    [Authorize]
    public class OfferController : WithUserInfoController
    {
        readonly IOfferFacade _offerFacade;
        readonly IUserCollectionFacade _userCollectionFacade;
        readonly IUserFacade _userFacade;
        readonly ITransferFacade _transferFacade;

        public static readonly string SKEY_OFFER_ACCEPTED = "_offerAccepted";
        public static readonly string SKEY_OFFER_DECLINED = "_offerDeclined";
        public static readonly string SKEY_DELIVERY_APPROVED = "_deliveryApproved";
        public static readonly string SKEY_OFFER_MODEL = "_offerModel";

        public OfferController(
            IOfferFacade offerFacade,
            IUserCollectionFacade userCollectionFacade,
            IUserFacade userFacade,
            ITransferFacade transferFacade) : base(userFacade)
        {
            _offerFacade = offerFacade;
            _userCollectionFacade = userCollectionFacade;
            _userFacade = userFacade;
            _transferFacade = transferFacade;
        }

        public IActionResult Index()
        {
            return RedirectToAction("Outgoing");
        }

        public async Task<IActionResult> Outgoing()
        {
            ViewBag.Title = "Outgoing offers";
            return View(await _userFacade.GetUserOffers(int.Parse(User.Identity.Name)));
        }

        public async Task<IActionResult> Incoming()
        {
            ViewBag.Title = "Incoming offers";
            return View(await _userFacade.GetUserReceivedOffers(int.Parse(User.Identity.Name)));
        }

        [HttpGet]
        public async Task<IActionResult> Create(int? id)
        {
            var myId = int.Parse(User.Identity.Name);

            if (id == null || id == myId)
            {
                return NotFound();
            }

            var yourId = id.Value;
            var model = (TempData.ContainsKey(SKEY_OFFER_MODEL))
                ? JsonConvert.DeserializeObject<OfferCreateDto>((string)TempData[SKEY_OFFER_MODEL])
                : new OfferCreateDto { AuthorId = myId, RecipientId = yourId, RequestedMoney = 0, OfferedMoney = 0, OfferedCactuses = new Dictionary<int, int>(), RequestedCactuses = new Dictionary<int, int>() };

            ViewBag.MyCollection = await _userCollectionFacade.GetUserCactusesForSale(myId);
            ViewBag.YourCollection = await _userCollectionFacade.GetUserCactusesForSale(yourId);
            ViewBag.UserDetails = await _userFacade.GetUserInfo(yourId);

            return View(model);
        }

        [ActionName("Create")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateAsync(OfferCreateDto offer)
        {
            var myId = offer.AuthorId;
            var yourId = offer.RecipientId;

            var myCollection = await _userCollectionFacade.GetUserCactusesForSale(myId);
            var yourCollection = await _userCollectionFacade.GetUserCactusesForSale(yourId);

            var offeredCactuses = (offer.OfferedCactuses != null) ? offer.OfferedCactuses.Where(o => o.Value != 0).ToList() : new List<KeyValuePair<int, int>>();
            var requestedCactuses = (offer.RequestedCactuses != null) ? offer.RequestedCactuses.Where(o => o.Value != 0).ToList() : new List<KeyValuePair<int, int>>();

            if (offeredCactuses.Sum(c => c.Value) + requestedCactuses.Sum(c => c.Value) == 0)
            {
                ModelState.AddModelError("General", "At least one cactus should be requested or offered.");
            }

            if (ModelState.IsValid)
            {
                foreach (var offeredCactus in offeredCactuses)
                {
                    var found = myCollection.Where(dto => dto.Id == offeredCactus.Key).FirstOrDefault();
                    if (found == null)
                    {
                        ModelState.AddModelError("OfferedCactuses", "Invalid cactus offered.");
                    }
                    else if (offeredCactus.Value < 0)
                    {
                        ModelState.AddModelError("OfferedCactuses", "Offered amount can't be negative.");
                        //ModelState.AddModelError("OfferedCactuses[0].Value", "Offered amount can't be negative.");
                    }
                    else if (offeredCactus.Value > found.Amount)
                    {
                        ModelState.AddModelError("OfferedCactuses", "Offered amount must be less than max amount.");
                    }
                }

                foreach (var requestedCactus in requestedCactuses)
                {
                    var found = yourCollection.Where(dto => dto.Id == requestedCactus.Key).FirstOrDefault();
                    if (found == null)
                    {
                        ModelState.AddModelError("RequestedCactuses", "Invalid cactus requested.");
                    }
                    else if (requestedCactus.Value < 0)
                    {
                        ModelState.AddModelError("RequestedCactuses", "Requested amount can't be negative.");
                    }
                    else if (found.Amount < requestedCactus.Value)
                    {
                        ModelState.AddModelError("RequestedCactuses", "Requested amount must be less than max amount.");
                    }
                }
            }

            if (ModelState.IsValid)
            {
                offer.OfferedCactuses = new Dictionary<int, int>(offeredCactuses);
                offer.RequestedCactuses = new Dictionary<int, int>(requestedCactuses);
                await _offerFacade.CreateOffer(offer);
                return RedirectToAction("Outgoing");
            }

            ViewBag.MyCollection = myCollection;
            ViewBag.YourCollection = yourCollection;
            ViewBag.UserDetails = await _userFacade.GetUserInfo(yourId);
            return View(offer);
        }

        [HttpPost]
        public async Task<IActionResult> Accept(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            
            try
            {
                await _offerFacade.AcceptOfferAsync(id.Value);
                TempData.Add(SKEY_OFFER_ACCEPTED, true);
            }
            catch (InsufficientMoneyException)
            {
                TempData.Add(SKEY_OFFER_ACCEPTED, false);
            }

            return RedirectToAction("Incoming");
        }

        [HttpPost]
        public async Task<IActionResult> Counter(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var offer = await _offerFacade.GetOffer(id.Value);
            if (offer == null)
            {
                return NotFound();
            }

            var o = new OfferCreateDto
            {
                AuthorId = offer.RecipientId,
                RecipientId = offer.AuthorId,
                OfferedMoney = offer.RequestedMoney,
                RequestedMoney = offer.OfferedMoney,
                OfferedCactuses = offer.RequestedCactuses.ToDictionary(c => c.Cactus.Id, c => c.Amount),
                RequestedCactuses = offer.OfferedCactuses.ToDictionary(c => c.Cactus.Id, c => c.Amount),
                PreviousOfferId = offer.Id
            };

            TempData.Add(SKEY_OFFER_MODEL, JsonConvert.SerializeObject(o));
            return RedirectToAction("Create", new { id = offer.AuthorId });
        }

        [HttpPost]
        public async Task<IActionResult> Decline(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var offer = await _offerFacade.GetOffer(id.Value);
            if (offer == null)
            {
                return NotFound();
            }

            await _offerFacade.RejectOffer(id.Value);
            TempData.Add(SKEY_OFFER_DECLINED, true);

            return RedirectToAction("Incoming");
        }

        [HttpPost]
        public async Task<IActionResult> Confirm(int? id, string redirect)
        {
            if (id == null || redirect == null)
            {
                return NotFound();
            }

            var userId = int.Parse(User.Identity.Name);
            var offer = await _offerFacade.GetOffer(id.Value);
            if (offer == null || (offer.AuthorId != userId && offer.RecipientId != userId))
            {
                return NotFound();
            }

            var transfer = await _transferFacade.GetTransferByOfferId(id.Value);
            var success = await _transferFacade.ApproveDelivery(transfer.Id, userId);
            TempData.Add(SKEY_DELIVERY_APPROVED, success);

            return RedirectToAction(redirect);
        }
    }
}
