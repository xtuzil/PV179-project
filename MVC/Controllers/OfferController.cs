using BL.DTOs;
using BL.Facades;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MVC.Controllers
{
    [Authorize]
    public class OfferController : Controller
    {
        readonly IOfferFacade _offerFacade;
        readonly IUserCollectionFacade _userCollectionFacade;
        readonly IUserFacade _userFacade;

        public static readonly string SKEY_OFFER_ACCEPTED = "_offerAccepted";

        public OfferController(IOfferFacade offerFacade, IUserCollectionFacade userCollectionFacade, IUserFacade userFacade)
        {
            _offerFacade = offerFacade;
            _userCollectionFacade = userCollectionFacade;
            _userFacade = userFacade;
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
            if (id == null)
            {
                return NotFound();
            }

            var myId = int.Parse(User.Identity.Name);
            var yourId = id.Value;

            var model = new OfferCreateDto { AuthorId = myId, RecipientId = yourId, RequestedMoney = 0, OfferedMoney = 0, OfferedCactuses = new Dictionary<int, int>(), RequestedCactuses = new Dictionary<int, int>() };

            ViewBag.MyCollection = await _userCollectionFacade.GetUserCactusesForSale(await _userFacade.GetUserInfo(myId));
            ViewBag.YourCollection = await _userCollectionFacade.GetUserCactusesForSale(await _userFacade.GetUserInfo(yourId));

            return View(model);
        }

        [ActionName("Create")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateAsync(OfferCreateDto offer)
        {
            var myId = offer.AuthorId;
            var yourId = offer.RecipientId;

            var myCollection = await _userCollectionFacade.GetUserCactusesForSale(await _userFacade.GetUserInfo(myId));
            var yourCollection = await _userCollectionFacade.GetUserCactusesForSale(await _userFacade.GetUserInfo(yourId));

            var offeredCactuses = offer.OfferedCactuses.Where(o => o.Value != 0).ToList();
            var requestedCactuses = offer.RequestedCactuses.Where(o => o.Value != 0).ToList();

            if (offeredCactuses.Sum(c => c.Value) + requestedCactuses.Sum(c => c.Value) == 0)
            {
                ModelState.AddModelError("General", "At least one cactus should be requested or offered.");
            }

            if (ModelState.IsValid)
            {
                foreach (var offeredCactus in offeredCactuses)
                {
                    var found = myCollection.Find(dto => dto.Id == offeredCactus.Key);
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
                    var found = yourCollection.Find(dto => dto.Id == requestedCactus.Key);
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
                _offerFacade.CreateOffer(offer);
                return RedirectToAction("Outgoing");
            }

            ViewBag.MyCollection = myCollection;
            ViewBag.YourCollection = yourCollection;
            return View(offer);
        }

        public async Task<IActionResult> Accept(int? id)
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

            var success = await _offerFacade.AcceptOfferAsync(offer);
            TempData.Add(SKEY_OFFER_ACCEPTED, success);

            return RedirectToAction("Incoming");
        }
    }
}
