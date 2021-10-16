using CactusDAL.Models;
using CactusDAL.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using System;

namespace CactusDAL
{
    public class EntityFrameworkUnitOfWork : UnitOfWorkBase
    {
        public DbContext _context { get; set; }

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
            _context = dbContext;
            Addresses = new EntityFrameworkRepository<PostalAddress>(_context);
            Cactuses = new EntityFrameworkRepository<Cactus>(_context);
            CactusOffers = new EntityFrameworkRepository<CactusOffer>(_context);
            CactusRequests = new EntityFrameworkRepository<CactusRequest>(_context);
            Comments = new EntityFrameworkRepository<Comment>(_context);
            Genuses = new EntityFrameworkRepository<Genus>(_context);
            Offers = new EntityFrameworkRepository<Offer>(_context);
            Reviews = new EntityFrameworkRepository<Review>(_context);
            Reports = new EntityFrameworkRepository<Report>(_context);
            Species = new EntityFrameworkRepository<Species>(_context);
            Transfers = new EntityFrameworkRepository<Transfer>(_context);
            Users = new EntityFrameworkRepository<User>(_context);
            WishlistItems = new EntityFrameworkRepository<Wishlist>(_context);
        }

        protected override void CommitCore()
        {
            _context.SaveChanges();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this.disposed = true;
        }

        public override void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}