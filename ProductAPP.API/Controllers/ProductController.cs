using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductAPP.API.Models.Requests;
using ProductAPP.API.Models.Responses;
using ProductAPP.BLLayer.DTO;
using ProductAPP.BLLayer.Interfaces;
using System;
using System.Collections.Generic;

namespace ProductAPP.API.Controllers
{
    [Controller]
    [Route("api/[controller]/")]
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly IMapper _mapper;

        public ProductController(IProductService productService, IMapper mapper)
        {
            _productService = productService;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult GetAll()
        {
            try
            {
                var prod = _productService.GetProducts();
                var products = _mapper.Map<IEnumerable<ProductResponse>>(_productService.GetProducts());
                return Ok(products);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet]
        [Route("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Get(int id)
        {
            try
            {
                var product = _mapper.Map<ProductResponse>(_productService.GetProduct(id));
                return Ok(product);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Create(ProductCreateRequest product)
        {
            try
            {
                _productService.CreateProduct(_mapper.Map<ProductDTO>(product));
                return Ok(new { Message = "Product was succesfully created" });
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Update(int id, ProductCreateRequest product)
        {
            try
            {
                _productService.UpdateProduct(id, _mapper.Map<ProductDTO>(product));
                return Ok(new { Message = "Product was succesfully updated" });
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Delete(int id)
        {
            try
            {
                _productService.DeleteProduct(id);
                return Ok(new { Message = "Product was succesfully updated" });
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
