using Infrastructure.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using System;

namespace Infrastructure.EntityFramework
{
    public class EntityFrameworkUnitOfWork : UnitOfWorkBase
    {
        public DbContext Context { get; set; }

        public override EntityFrameworkRepository<PostalAddress> Addresses { get; }
        public override EntityFrameworkRepository<Cactus> Cactuses { get; }
        public override EntityFrameworkRepository<CactusOffer> CactusOffers { get; }
        public override EntityFrameworkRepository<CactusRequest> CactusRequests { get; }
        public override EntityFrameworkRepository<Comment> Comments { get; }
        public override EntityFrameworkRepository<Genus> Genuses { get; }
        public override EntityFrameworkRepository<Offer> Offers { get; }
        public override EntityFrameworkRepository<Review> Reviews { get; }
        public override EntityFrameworkRepository<Report> Reports { get; }
        public override EntityFrameworkRepository<Species> Species { get; }
        public override EntityFrameworkRepository<Transfer> Transfers { get; }
        public override EntityFrameworkRepository<User> Users { get; }
        public override EntityFrameworkRepository<Wishlist> WishlistItems { get; }

        public EntityFrameworkUnitOfWork(DbContext dbContext)
        {
            Context = dbContext;
            Addresses = new EntityFrameworkRepository<PostalAddress>(Context);
            Cactuses = new EntityFrameworkRepository<Cactus>(Context);
            CactusOffers = new EntityFrameworkRepository<CactusOffer>(Context);
            CactusRequests = new EntityFrameworkRepository<CactusRequest>(Context);
            Comments = new EntityFrameworkRepository<Comment>(Context);
            Genuses = new EntityFrameworkRepository<Genus>(Context);
            Offers = new EntityFrameworkRepository<Offer>(Context);
            Reviews = new EntityFrameworkRepository<Review>(Context);
            Reports = new EntityFrameworkRepository<Report>(Context);
            Species = new EntityFrameworkRepository<Species>(Context);
            Transfers = new EntityFrameworkRepository<Transfer>(Context);
            Users = new EntityFrameworkRepository<User>(Context);
            WishlistItems = new EntityFrameworkRepository<Wishlist>(Context);
        }

        protected override void CommitCore()
        {
            Context.SaveChanges();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    Context.Dispose();
                }
            }
            disposed = true;
        }

        public override void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}