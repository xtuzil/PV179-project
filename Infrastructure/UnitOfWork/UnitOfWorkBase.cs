using System;
using System.Collections.Generic;

namespace Infrastructure.UnitOfWork
{
    public abstract class UnitOfWorkBase : IUnitOfWork
    {
        private List<Action> _afterCommitActions { get; set; } = new List<Action>();

        public void Commit()
        {
            CommitCore();
            RunAfterCommitActions();
        }

        public void RegisterAfterCommitAction(Action action)
        {
            _afterCommitActions.Add(action);
        }

        private void RunAfterCommitActions()
        {
            foreach (var action in _afterCommitActions)
            {
                action();
            }
            _afterCommitActions.Clear();
        }

        public abstract void Dispose();
        protected abstract void CommitCore();
    }
}
