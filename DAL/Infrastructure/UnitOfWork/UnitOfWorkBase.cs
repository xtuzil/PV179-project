using CactusDAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CactusDAL.UnitOfWork
{
    public abstract class UnitOfWorkBase : IUnitOfWork, IDisposable
    {
        private List<Action> _afterCommitActions { get; set; } = new List<Action>();
        public abstract IRepository<PostalAddress> Addresses { get; }
        public abstract IRepository<Cactus> Cactuses { get; }
        public abstract IRepository<CactusOffer> CactusOffers { get; }
        public abstract IRepository<CactusRequest> CactusRequests { get; }
        public abstract IRepository<Comment> Comments { get; }
        public abstract IRepository<Genus> Genuses { get; }
        public abstract IRepository<Offer> Offers { get; }
        public abstract IRepository<Review> Reviews { get; }
        public abstract IRepository<Report> Reports { get; }
        public abstract IRepository<Species> Species { get; }
        public abstract IRepository<Transfer> Transfers { get; }
        public abstract IRepository<User> Users { get; }
        public abstract IRepository<Wishlist> WishlistItems { get; }

        public void Commit()
        {
            foreach (Action action in _afterCommitActions)
            {
                action();
            }
            CommitCore();
            _afterCommitActions.Clear();
        }

        public void RegisterAction(Action action)
        {
            _afterCommitActions.Add(action);
        }

        public abstract void Dispose();
        protected abstract void CommitCore();
    }
}
