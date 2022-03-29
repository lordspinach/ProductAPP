using Microsoft.EntityFrameworkCore;
using ProductAPP.DBLayer.EF;
using ProductAPP.DBLayer.Entities;
using ProductAPP.DBLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ProductAPP.DBLayer.Repositories
{
    public class ProductRepository : IRepository<ProductDb>
    {
        private readonly ApplicationContext _context;

        public ProductRepository(ApplicationContext context)
        {
            _context = context;
        }

        public IEnumerable<ProductDb> GetAll()
        {
            return _context.Products.Where(s => s.IsDeleted == false).ToList();
        }

        public ProductDb Get(int id)
        {
            return _context.Products.Where(s => s.IsDeleted == false).Where(s => s.Id == id).FirstOrDefault();
        }

        public void Create(ProductDb product)
        {
            _context.Products.Add(product);
        }

        public void Update(int id, ProductDb product)
        {
            product.Id = id;
            _context.Entry(product).State = EntityState.Modified;
        }

        public IEnumerable<ProductDb> Find(Func<ProductDb, Boolean> predicate)
        {
            return _context.Products.Where(s => s.IsDeleted == false).Where(predicate).ToList();
        }

        public void Delete(int id)
        {
            var product = _context.Products.Where(s => s.IsDeleted == false).Where(s => s.Id == id).FirstOrDefault();
            if (product != null)
            {
                _context.Entry(product).State = EntityState.Deleted;
            }
                
        }

        public bool AnyId(int id)
        {
            return _context.Products.Any(p => p.Id == id);
        }
    }
}
