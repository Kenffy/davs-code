using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Products.Models;
using Products.Models.Dto;
using Products.Repository.IRepository;

namespace Products.Controllers
{
    [Route("api/products")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private ResponseDto _response;
        private readonly IMapper _mapper;
        private readonly IProductRepository _productRepos;

        public ProductController(IProductRepository productRepo, IMapper mapper)
        {
            _mapper = mapper;
            _response = new ResponseDto();
            _productRepos = productRepo;
        }

        [HttpGet]
        public async Task<ActionResult<ResponseDto>> GetProducts()
        {
            try
            {
                var products = await _productRepos.GetAllProductAsync();
                _response.Result = _mapper.Map<List<ProductDto>>(products);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        [HttpGet("id:string")]
        public async Task<ActionResult<ResponseDto>> GetProduct(string id)
        {
            try
            {
                if (string.IsNullOrEmpty(id))
                {
                    _response.IsSuccess = false;
                    return BadRequest(_response);
                }

                var product = await _productRepos.GetProductAsync(id);

                if (product == null)
                {
                    _response.Message = "Product not found.";
                    _response.IsSuccess = false;
                    return NotFound(_response);
                }

                _response.Result = _mapper.Map<ProductDto>(product);
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
                return BadRequest(_response);
            }
        }

        [HttpPost]
        public async Task<ActionResult<ResponseDto>> CreateProduct([FromBody] ProductRequestDto productDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (productDto == null)
            {
                return BadRequest(productDto);
            }
            try
            {
                var product = _mapper.Map<Product>(productDto);
                await _productRepos.CreateProductAsync(product);
                _response.IsSuccess = true;
                _response.Message = "Product successfully created.";
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
                return BadRequest(_response);
            }
        }

        [HttpPut("id:string")]
        public async Task<ActionResult<ResponseDto>> UpdateProduct(string id, [FromBody] ProductRequestDto productDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (productDto == null || productDto.Id != id)
            {
                _response.Message = "Bad request!";
                _response.IsSuccess = false;
                return BadRequest(productDto);
            }
            try
            {
                var product = _mapper.Map<Product>(productDto);
                await _productRepos.UpdateProductAsync(product);
                _response.IsSuccess = true;
                _response.Message = "Product successfully updated.";
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
                return BadRequest(_response);
            }
        }

        [HttpDelete("id:string")]
        public async Task<ActionResult<ResponseDto>> DeleteProduct(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                _response.IsSuccess = false;
                _response.Message = "Bad Request!";
                return BadRequest(_response);
            }
            try
            {
                await _productRepos.DeleteProductAsync(id);
                _response.IsSuccess = true;
                _response.Message = "Product successfully deleted.";
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
                return BadRequest(_response);
            }
        }
    }
}
