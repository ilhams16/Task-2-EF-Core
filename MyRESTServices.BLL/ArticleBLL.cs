using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MyRESTServices.BLL.DTOs;
using MyRESTServices.BLL.Interfaces;
using MyRESTServices.Data;
using MyRESTServices.Data.Interfaces;
using MyRESTServices.Domain.Models;

namespace MyRESTServices.BLL
{
	public class ArticleBLL : IArticleBLL
	{
		private readonly IArticleData _articleData;
		private readonly IMapper _mapper;

		public ArticleBLL(IArticleData articleData, IMapper mapper)
		{
			_articleData = articleData;
			_mapper = mapper;
		}

		public async Task<bool> Delete(int id)
		{
			return await _articleData.Delete(id);
		}

		public async Task<IEnumerable<ArticleDTO>> GetArticleByCategory(int categoryId)
		{
			var articles = await _articleData.GetArticleByCategory(categoryId);
			var articlesDto = _mapper.Map<IEnumerable<ArticleDTO>>(articles);
			return articlesDto;
		}

		public async Task<ArticleDTO> GetArticleById(int id)
		{
			var article = await _articleData.GetById(id);
			var articlesDto = _mapper.Map<ArticleDTO>(article);
			return articlesDto;
		}

		public async Task<IEnumerable<ArticleDTO>> GetArticleWithCategory()
		{
			var articles = await _articleData.GetArticleWithCategory();
			var articlesDto = _mapper.Map<IEnumerable<ArticleDTO>>(articles);
			return articlesDto;
		}

		public async Task<int> GetCountArticles()
		{
			return await _articleData.GetCountArticles();
		}

		public async Task<IEnumerable<ArticleDTO>> GetWithPaging(int categoryId, int pageNumber, int pageSize)
		{
			var articles = await _articleData.GetWithPaging(categoryId ,pageNumber, pageSize);
			var articlesDto = _mapper.Map<IEnumerable<ArticleDTO>>(articles);
			return articlesDto;
		}

		public async Task<ArticleDTO> Insert(ArticleCreateDTO article)
		{
			var entity = _mapper.Map<Article>(article);
			var insertedArticle = await _articleData.Insert(entity);
			var insertedArticleDto = _mapper.Map<ArticleDTO>(insertedArticle);
			return insertedArticleDto;
		}

		public async Task<int> InsertWithIdentity(ArticleCreateDTO article)
		{
			var entity = _mapper.Map<Article>(article);
			var insertedArticle = await _articleData.Insert(entity);
			var insertedArticleDto = _mapper.Map<ArticleDTO>(insertedArticle);
			return insertedArticleDto.ArticleID;
		}

		public async Task<ArticleDTO> Update(ArticleUpdateDTO article)
		{
			var entity = _mapper.Map<Article>(article);
			var updatedArticle = await _articleData.Update(entity.ArticleId, entity);
			var updatedCategoryDto = _mapper.Map<ArticleDTO>(updatedArticle);
			return updatedCategoryDto;
		}
	}
}
