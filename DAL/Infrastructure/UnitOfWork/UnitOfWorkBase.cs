using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CactusDAL.UnitOfWork
{
    public abstract class UnitOfWorkBase : IUnitOfWork, IDisposable
    {
        private List<Action> _afterCommitActions { get; set; }
        public void Commit()
        {
            foreach (Action action in _afterCommitActions)
            {
                action();
            }
            CommitCore();
            _afterCommitActions = new List<Action>();
        }

        public void RegisterAction(Action action)
        {
            _afterCommitActions.Add(action);
        }

        public abstract void Dispose();
        protected abstract void CommitCore();
    }
}
