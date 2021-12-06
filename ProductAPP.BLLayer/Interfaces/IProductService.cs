using ProductAPP.BLLayer.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductAPP.BLLayer.Interfaces
{
    public interface IProductService
    {
        IEnumerable<ProductDTO> GetProducts();
        ProductDTO GetProduct(int id);
        void CreateProduct(ProductDTO product);
        void Dispose();
    }
}
