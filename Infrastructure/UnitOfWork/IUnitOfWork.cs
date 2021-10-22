using CactusDAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CactusDAL.UnitOfWork
{
    public interface IUnitOfWork
    {
        IRepository<PostalAddress> Addresses { get; }
        IRepository<Cactus> Cactuses { get; }
        IRepository<CactusOffer> CactusOffers { get; }
        IRepository<CactusRequest> CactusRequests { get; }
        IRepository<Comment> Comments { get; }
        IRepository<Genus> Genuses { get; }
        IRepository<Offer> Offers { get; }
        IRepository<Review> Reviews { get; }
        IRepository<Report> Reports { get; }
        IRepository<Species> Species { get; }
        IRepository<Transfer> Transfers { get; }
        IRepository<User> Users { get; }
        IRepository<Wishlist> WishlistItems { get; }

        void Commit();
        void RegisterAction(Action action);
    }
}
