using AutoMapper;
using Microsoft.Extensions.Configuration;
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

        public ProductService(IUnitOfWork uow, IMapper mapper, IHttpService httpService, IConfiguration configuration)
        {
            _database = uow;
            _mapper = mapper;
            _httpService = httpService;
            _brandServiceUri = configuration.GetSection("BrandServiceUri").Value;
        }

        public IEnumerable<ProductDTO> GetProducts()
        {
            return _mapper.Map<IList<ProductDTO>>(_database.Products.GetAll());
        }

        public ProductDTO GetProduct(int id)
        {
            var product = _database.Products.Get(id);
            if(product == null)
                throw new ValidationException("Product not found");
            else
                return _mapper.Map<ProductDTO>(product);
        }

        public void CreateProduct(ProductDTO product)
        {
            if (IsSizesOk(product.BrandId, product.RFSize))
            {
                _database.Products.Create(_mapper.Map<ProductDb>(product));
                _database.Save();
            }   
            else
                throw new ValidationException($"Brand have no \"{product.RFSize}\" size");
        }

        public void UpdateProduct(int id, ProductDTO productDto)
        {
            if(_database.Products.AnyId(id))
            {
                if (IsSizesOk(productDto.BrandId, productDto.RFSize))
                {
                    _database.Products.Update(id, _mapper.Map<ProductDb>(productDto));
                    _database.Save();
                }
                else
                {
                    throw new ValidationException($"Brand have no \"{productDto.RFSize}\" size");
                }
            }
            else
            {
                throw new ValidationException($"There is no product with ID = {id}");
            }
        }

        public void DeleteProduct(int id)
        {
            var product = _database.Products.Get(id);
            if(product != null)
            {
                _database.Products.Delete(id);
                _database.Save();
            }
            else
            {
                throw new ValidationException($"There is no product with ID = {id}");
            }
        }

        private bool IsSizesOk(int brandId, float rfSize)
        {
            var sizes = _httpService.Get<List<SizeDTO>>(_brandServiceUri + "size/" + brandId).Result;
            if (sizes == null)
                return false;

            foreach (var size in sizes)
            {
                if (size.RFSize == rfSize)
                    return true;
            }

            return false;
        }

        public void Dispose()
        {
            _database.Dispose();
        }
    } 
}
