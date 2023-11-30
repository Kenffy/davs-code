using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Products.Models.Dto;
using Products.Repository.IRepository;

namespace Products.Controllers
{
    [ApiController]
    [Route("api/categories")]
    public class CategoryController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ICategoryRepository _categoryRepos;

        public CategoryController(ICategoryRepository categoryRepo, IMapper mapper)
        {
            _mapper = mapper;
            _categoryRepos = categoryRepo;
        }

        [HttpGet]
        public async Task<ActionResult> GetCategories()
        {
            var categories = await _categoryRepos.GetAllCategoryAsync();
            return Ok(_mapper.Map<List<CategoryDto>>(categories));
        }

        [HttpGet("id:string")]
        public async Task<ActionResult<CategoryDto>> GetCategory(string id)
        {
            try
            {
                if (string.IsNullOrEmpty(id))
                {
                    return BadRequest();
                }

                var category = await _categoryRepos.GetCategoryAsync(id);

                if (category == null)
                {
                    return NotFound();
                }
                return Ok(_mapper.Map<CategoryDto>(category));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return BadRequest();
            }
        }
    };
}