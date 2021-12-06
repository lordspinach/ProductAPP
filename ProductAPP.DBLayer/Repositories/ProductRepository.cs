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
            return _context.Products;
        }

        public ProductDb Get(int id)
        {
            return _context.Products.Find(id);
        }

        public void Create(ProductDb product)
        {
            _context.Products.Add(product);
        }

        public void Update(ProductDb product)
        {
            _context.Entry(product).State = EntityState.Modified;
        }

        public IEnumerable<ProductDb> Find(Func<ProductDb, Boolean> predicate)
        {
            return _context.Products.Where(predicate).ToList();
        }

        public void Delete(int id)
        {
            ProductDb product = _context.Products.Find(id);
            if (product != null)
                _context.Products.Remove(product);
        }
    }
}
