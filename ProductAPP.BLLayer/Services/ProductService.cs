using AutoMapper;
using ProductAPP.BLLayer.DTO;
using ProductAPP.BLLayer.Infrastructure;
using ProductAPP.BLLayer.Interfaces;
using ProductAPP.DBLayer.Entities;
using ProductAPP.DBLayer.Interfaces;
using System.Collections.Generic;


namespace ProductAPP.BLLayer.Services
{
    public class ProductService : IProductService
    {
        private  IUnitOfWork _database { get; set; }
        private readonly IMapper _mapper;
        private readonly IHttpService _httpService;
        private readonly string _brandServiceUri;

        public ProductService(IUnitOfWork uow, IMapper mapper, IHttpService httpService)
        {
            _database = uow;
            _mapper = mapper;
            _httpService = httpService;
            _brandServiceUri = "https://localhost:44376/api/";
        }

        public IEnumerable<ProductDTO> GetProducts()
        {
            var products = new List<ProductDTO>();
            var productsDb = _database.Products.GetAll();
            foreach(var product in productsDb)
            {
                products.Add(_mapper.Map<ProductDTO>(product));
            }
            if (products.Count > 0)
                return products;
            else
                throw new ValidationException("There is no products in database", "");
        }

        public ProductDTO GetProduct(int id)
        {
            var product = _database.Products.Get(id);
            if(product == null)
                throw new ValidationException("Product not found", "");
            else
                return _mapper.Map<ProductDTO>(product);
        }

        public void CreateProduct(ProductDTO product)
        {
            var sizes = _httpService.Get<List<SizeDTO>>(_brandServiceUri + "size/" + product.BrandId).Result;
            if(sizes == null)
                throw new ValidationException("There is no sizes in this brand", "");

            bool brandHasSize = false;
            foreach (var size in sizes)
            {
                if(size.RFSize == product.RFSize)
                    brandHasSize = true;
            }

            if (brandHasSize)
            {
                _database.Products.Create(_mapper.Map<ProductDb>(product));
                _database.Save();
            }   
            else
                throw new ValidationException($"Brand have no \"{product.RFSize}\" size", "");
        }

        public void Dispose()
        {
            _database.Dispose();
        }
    } 
}
