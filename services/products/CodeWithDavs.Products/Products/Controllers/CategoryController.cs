using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Products.Models;
using Products.Models.Dto;
using Products.Repository.IRepository;
using static System.Reflection.Metadata.BlobBuilder;

namespace Products.Controllers
{
    [ApiController]
    [Route("api/categories")]
    public class CategoryController : ControllerBase
    {
        private ResponseDto _response;
        private readonly IMapper _mapper;
        private readonly ICategoryRepository _categoryRepos;

        public CategoryController(ICategoryRepository categoryRepo, IMapper mapper)
        {
            _mapper = mapper;
            _response = new ResponseDto();
            _categoryRepos = categoryRepo;
        }

        [HttpGet]
        public async Task<ActionResult<ResponseDto>> GetCategories()
        {
            try
            {
                var categories = await _categoryRepos.GetAllCategoryAsync();
                _response.Result =  _mapper.Map<List<CategoryDto>>(categories);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        [HttpGet("id:string")]
        public async Task<ActionResult<ResponseDto>> GetCategory(string id)
        {
            try
            {
                if (string.IsNullOrEmpty(id))
                {
                    _response.IsSuccess = false;
                    return BadRequest(_response);
                }

                var category = await _categoryRepos.GetCategoryAsync(id);

                if (category == null)
                {
                    _response.Message = "Category not found.";
                    _response.IsSuccess = false;
                    return NotFound(_response);
                }

                _response.Result = _mapper.Map<CategoryDto>(category);
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
        public async Task<ActionResult<ResponseDto>> CreateCategory([FromBody] CategoryDto categoryDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if(categoryDto == null)
            {
                return BadRequest(categoryDto);
            }
            try
            {
                var category = _mapper.Map<Category>(categoryDto);
                category.Id = Guid.NewGuid().ToString();
                category.CreatedAt = DateTime.Now;

                await _categoryRepos.CreateCategoryAsync(category);
                _response.IsSuccess = true;
                _response.Message = "Category successfully created.";
                return Ok(_response);
            }catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
                return BadRequest(_response);
            }
        }

        [HttpPut("id:string")]
        public async Task<ActionResult<ResponseDto>> UpdateCategory(string id, [FromBody] CategoryDto categoryDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (categoryDto == null || categoryDto.Id != id)
            {
                _response.Message = "Bad request!";
                _response.IsSuccess = false;
                return BadRequest(categoryDto);
            }
            try
            {
                var category = _mapper.Map<Category>(categoryDto);
                category.UpdatedAt = DateTime.Now;

                await _categoryRepos.UpdateCategoryAsync(category);
                _response.IsSuccess = true;
                _response.Message = "Category successfully updated.";
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
        public async Task<ActionResult<ResponseDto>> DeleteCategory(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                _response.IsSuccess = false;
                _response.Message = "Bad Request!";
                return BadRequest(_response);
            }
            try
            {
                await _categoryRepos.DeleteCategoryAsync(id);
                _response.IsSuccess = true;
                _response.Message = "Category successfully deleted.";
                return Ok(_response);
            }    
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
                return BadRequest(_response);
            }
        }
    };
}