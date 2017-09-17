using HeatChart.DataRepository.Sql.Infrastructure;
using HeatChart.DataRepository.Sql.Interfaces;
using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Transactions;

namespace HeatChart.DataRepository.Sql.UnitOfWork
{
    public class EFUnitOfWork : IEFUnitOfWork
    {
        private readonly TransactionOptions _transactionOptions;
        private bool _disposed;

        public DbContext Context
        {
            get;
        }

        public EFUnitOfWork(IDBFactory dbFactory)
        {
            Context = dbFactory.GetContext();
            _transactionOptions = new TransactionOptions
            {
                IsolationLevel = IsolationLevel.ReadCommitted
            };
        }

        public EFUnitOfWork()
        {
            //TODO: Enable dependency injection
            Context = new DBFactory().GetContext();
            _transactionOptions = new TransactionOptions
            {
                IsolationLevel = IsolationLevel.ReadCommitted
            };
        }
        public void Commit()
        {
            try
            {
                using (var scope = new TransactionScope(TransactionScopeOption.Required, _transactionOptions))
                {
                    Context.SaveChanges();
                    scope.Complete();
                }
            }            
            catch(Exception ex)
            {               
                throw ex;
            }
        }

        public void Rollback()
        {
            Dispose();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    Context.Dispose();
                }
            }
            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
