using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MyRESTServices.BLL.DTOs;
using MyRESTServices.BLL.Interfaces;
using MyRESTServices.Data.Interfaces;

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

		public Task<bool> Delete(int id)
		{
			throw new NotImplementedException();
		}

		public Task<IEnumerable<ArticleDTO>> GetArticleByCategory(int categoryId)
		{
			throw new NotImplementedException();
		}

		public Task<ArticleDTO> GetArticleById(int id)
		{
			throw new NotImplementedException();
		}

		public async Task<IEnumerable<ArticleDTO>> GetArticleWithCategory()
		{
			var articles = await _articleData.GetAll();
			var articlesDto = _mapper.Map<ArticleDTO>(articles);
			return (IEnumerable<ArticleDTO>)articlesDto;
		}

		public Task<int> GetCountArticles()
		{
			throw new NotImplementedException();
		}

		public Task<IEnumerable<ArticleDTO>> GetWithPaging(int categoryId, int pageNumber, int pageSize)
		{
			throw new NotImplementedException();
		}

		public Task<ArticleDTO> Insert(ArticleCreateDTO article)
		{
			throw new NotImplementedException();
		}

		public Task<int> InsertWithIdentity(ArticleCreateDTO article)
		{
			throw new NotImplementedException();
		}

		public Task<ArticleDTO> Update(ArticleUpdateDTO article)
		{
			throw new NotImplementedException();
		}
	}
}
