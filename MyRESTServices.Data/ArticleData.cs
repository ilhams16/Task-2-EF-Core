using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MyRESTServices.Data.Interfaces;
using MyRESTServices.Domain.Models;

namespace MyRESTServices.Data
{
	public class ArticleData : IArticleData
	{
		private readonly AppDbContext _context;
		public ArticleData(AppDbContext context)
		{
			_context = context;
		}
		public Task<bool> Delete(int id)
		{
			throw new NotImplementedException();
		}

		public async Task<IEnumerable<Article>> GetAll()
		{
			var articles = await _context.Articles.ToListAsync();
			return articles;
		}

		public async Task<IEnumerable<Article>> GetArticleByCategory(int categoryId)
		{
			var articles = await _context.Articles.Where(a => a.CategoryId == categoryId).ToListAsync();
			return articles;
		}

		public async Task<IEnumerable<Article>> GetArticleWithCategory()
		{
			var articles = await _context.Articles.Include(c => c.Category).ToListAsync();
			return articles;
		}

		public async Task<Article> GetById(int id)
		{
			var article = await _context.Articles.SingleOrDefaultAsync(a => a.ArticleId == id);
			return article;
		}

		public Task<int> GetCountArticles()
		{
			throw new NotImplementedException();
		}

		public Task<IEnumerable<Article>> GetWithPaging(int categoryId, int pageNumber, int pageSize)
		{
			throw new NotImplementedException();
		}

		public Task<Article> Insert(Article entity)
		{
			throw new NotImplementedException();
		}

		public Task<Task> InsertArticleWithCategory(Article article)
		{
			throw new NotImplementedException();
		}

		public Task<int> InsertWithIdentity(Article article)
		{
			throw new NotImplementedException();
		}

		public Task<Article> Update(int id, Article entity)
		{
			throw new NotImplementedException();
		}
	}
}
