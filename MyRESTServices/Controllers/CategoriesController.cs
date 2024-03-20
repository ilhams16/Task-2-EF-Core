using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using MyRESTServices.BLL.DTOs;
using MyRESTServices.BLL.Interfaces;

namespace MyRESTServices.Controllers
{
	[Route("api/v1/[controller]")]
	[ApiController]
	public class CategoriesController : ControllerBase
	{
		private readonly ICategoryBLL _categoryBLL;
		private readonly IValidator<CategoryCreateDTO> _validatorCategoryCreate;
		private readonly IValidator<CategoryUpdateDTO> _validatorCategoryUpdate;
		public CategoriesController(ICategoryBLL categoryBLL, IValidator<CategoryCreateDTO> validatorCategoryCreate, IValidator<CategoryUpdateDTO> validatorCategoryUpdate)
		{
			_categoryBLL = categoryBLL;
			_validatorCategoryCreate = validatorCategoryCreate;
			_validatorCategoryUpdate = validatorCategoryUpdate;
		}

		[HttpGet]
		public async Task<IEnumerable<CategoryDTO>> Get()
		{
			var results = await _categoryBLL.GetAll();
			return results;
		}

		[HttpGet("pagination/{pageNumber}/{pageSize}/{name}")]
		public async Task<IActionResult> GetPagination(int pageNumber, int pageSize, string name)
		{
			var result = await _categoryBLL.GetWithPaging(pageNumber, pageSize, name);
			if (result == null)
			{
				return NotFound();
			}
			return Ok(result);
		}

		[HttpGet("count/{name}")]
		public async Task<IActionResult> GetCount(string name)
		{
			var result = await _categoryBLL.GetCountCategories(name);
			if (result == null)
			{
				return NotFound();
			}
			return Ok(result);
		}

		[HttpGet("{id}")]
		public async Task<CategoryDTO> Get(int id)
		{
			var result = await _categoryBLL.GetById(id);
			return result;
		}

		[HttpGet("search/{name}")]
		public async Task<IActionResult> Search(string name)
		{
			var result = await _categoryBLL.GetByName(name);
			if (result == null)
			{
				return NotFound();
			}
			return Ok(result);
		}

		[HttpPost]
		public async Task<IActionResult> Post(CategoryCreateDTO categoryCreateDTO)
		{
			if (categoryCreateDTO == null)
			{
				return BadRequest();
			}

			try
			{
				var validatorResult = await _validatorCategoryCreate.ValidateAsync(categoryCreateDTO);
				if (!validatorResult.IsValid)
				{
					Helpers.Extensions.AddToModelState(validatorResult, ModelState);
					return BadRequest(ModelState);
				}
				_categoryBLL.Insert(categoryCreateDTO);
				return Ok("Insert data success");
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

		[HttpPut("{id}")]
		public async Task<IActionResult> Put(int id, CategoryUpdateDTO categoryUpdateDTO)
		{
			if (_categoryBLL.GetById(id) == null)
			{
				return NotFound();
			}

			try
			{
				var validatorResult = await _validatorCategoryUpdate.ValidateAsync(categoryUpdateDTO);
				if (!validatorResult.IsValid)
				{
					Helpers.Extensions.AddToModelState(validatorResult, ModelState);
					return BadRequest(ModelState);
				}
				_categoryBLL.Update(categoryUpdateDTO);
				return Ok("Update data success");
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

		[HttpDelete("{id}")]
		public IActionResult Delete(int id)
		{
			if (_categoryBLL.GetById(id) == null)
			{
				return NotFound();
			}

			try
			{
				_categoryBLL.Delete(id);
				return Ok("Delete data success");
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}
	}
}
