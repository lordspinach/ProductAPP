using ProductAPP.DBLayer.EF;
using ProductAPP.DBLayer.Entities;
using ProductAPP.DBLayer.Interfaces;
using System;

namespace ProductAPP.DBLayer.Repositories
{
    public class EFUnitOfWork : IUnitOfWork
    {
        private readonly ApplicationContext _context;
        private ProductRepository _productRepository;

        public EFUnitOfWork(ApplicationContext context)
        {
            _context = context;
        }

        public IRepository<ProductDb> Products
        {
            get
            {
                if( _productRepository == null )
                    _productRepository = new ProductRepository(_context);
                return _productRepository;
            }
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        private bool _disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
                _disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
