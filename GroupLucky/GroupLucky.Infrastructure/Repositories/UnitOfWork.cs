﻿using GroupLucky.Application.Contracts.Persistences;
using System.Data;

namespace GroupLucky.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private IDbConnection _connection;
        private IDbTransaction _transaction;
        private ICategoryRepository categoryRepository;
        private bool _dispose;

        public void Commit()
        {
            try
            {
                _transaction.Commit(); 
            }
            catch
            {
                _transaction.Rollback();
                throw;
            }
            finally
            {
                _transaction.Dispose();
                _transaction = _connection.BeginTransaction();
                resetRepositories();
            }
        }
        public ICategoryRepository CategoryRepository => categoryRepository ??= new CategoryRepository(transaction: _transaction);
        public void Dispose()
        {
            dispose(true);
            GC.SuppressFinalize(this);
        }

        private void resetRepositories()
        {
            categoryRepository = null;
        }

        private void dispose(bool disposing)
        {
            if (!_dispose)
            {
                if (disposing)
                {
                    if (_transaction != null)
                    {
                        _transaction.Dispose();
                        _transaction = null;
                    }
                    if (_connection != null)
                    {
                        _connection.Dispose();
                        _connection = null;
                    }
                }
                _dispose = true;
            }
        }
        ~UnitOfWork()
        {
            dispose(false);
        }
    }
}
