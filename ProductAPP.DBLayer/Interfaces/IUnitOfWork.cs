using ProductAPP.DBLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductAPP.DBLayer.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<ProductDb> Products { get; }
        void Save();
    }
}
