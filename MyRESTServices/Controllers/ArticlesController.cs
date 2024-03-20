using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using MyRESTServices.BLL.DTOs;
using MyRESTServices.BLL.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MyRESTServices.Controllers
{
	[Route("api/v1/[controller]")]
	[ApiController]
	public class ArticlesController : ControllerBase
	{
		private readonly IArticleBLL _articleBLL;
		private readonly IValidator<ArticleCreateDTO> _validatorArticleCreate;
		private readonly IValidator<ArticleUpdateDTO> _validatorArticleUpdate;
		public ArticlesController(IArticleBLL articleBLL, IValidator<ArticleCreateDTO> validatorArticleCreate, IValidator<ArticleUpdateDTO> validatorArticleUpdate)
		{
			_articleBLL = articleBLL;
			_validatorArticleCreate = validatorArticleCreate;
			_validatorArticleUpdate = validatorArticleUpdate;
		}

		// GET: api/<ArticlesController>
		[HttpGet]
		public async Task<IEnumerable<ArticleDTO>> Get()
		{
			var results = await _articleBLL.GetArticleWithCategory();
			return results;
		}

		[HttpGet("Count")]
		public async Task<int> GetCount()
		{
			var results = await _articleBLL.GetCountArticles();
			return results;
		}

		[HttpGet("Category/{id}")]
		public async Task<IEnumerable<ArticleDTO>> GetbyCategory(int id)
		{
			var results = await _articleBLL.GetArticleByCategory(id);
			return results;
		}

		// GET api/<ArticlesController>/5
		[HttpGet("{id}")]
		public async Task<ArticleDTO> Get(int id)
		{
			var result = await _articleBLL.GetArticleById(id);
			return result;
		}

		// POST api/<ArticlesController>
		[HttpPost]
		public async Task<IActionResult> Post(ArticleCreateDTO articleCreateDTO)
		{
			if (articleCreateDTO == null)
			{
				return BadRequest();
			}

			try
			{
				var validatorResult = await _validatorArticleCreate.ValidateAsync(articleCreateDTO);
				if (!validatorResult.IsValid)
				{
					Helpers.Extensions.AddToModelState(validatorResult, ModelState);
					return BadRequest(ModelState);
				}
				_articleBLL.Insert(articleCreateDTO);
				return Ok("Insert data success");
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

		// PUT api/<ArticlesController>/5
		[HttpPut("{id}")]
		public async Task<IActionResult> Put(int id, ArticleUpdateDTO articleUpdateDTO)
		{
			if (_articleBLL.GetArticleById(id) == null)
			{
				return NotFound();
			}

			try
			{
				var validatorResult = await _validatorArticleUpdate.ValidateAsync(articleUpdateDTO);
				if (!validatorResult.IsValid)
				{
					Helpers.Extensions.AddToModelState(validatorResult, ModelState);
					return BadRequest(ModelState);
				}
				_articleBLL.Update(articleUpdateDTO);
				return Ok("Update data success");
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

		// DELETE api/<ArticlesController>/5
		[HttpDelete("{id}")]
		public IActionResult Delete(int id)
		{
			if (_articleBLL.GetArticleById(id) == null)
			{
				return NotFound();
			}

			try
			{
				_articleBLL.Delete(id);
				return Ok("Delete data success");
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}
	}
}
