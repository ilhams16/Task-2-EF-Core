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
		public async Task<bool> Delete(int id)
		{
			var article = await _context.Articles.FindAsync(id);
			if (article != null)
				return false;
			_context.Articles.Remove(article);
			await _context.SaveChangesAsync();
			return true;
		}

		public async Task<IEnumerable<Article>> GetAll()
		{
			var articles = await _context.Articles.ToListAsync();
			return articles;
		}

		public async Task<IEnumerable<Article>> GetArticleByCategory(int categoryId)
		{
			var articles = await _context.Articles.Include(a => a.Category).Where(a => a.CategoryId == categoryId).ToListAsync();
			return articles;
		}

		public async Task<IEnumerable<Article>> GetArticleWithCategory()
		{
			var articles = await _context.Articles.Include(c => c.Category).ToListAsync();
			return articles;
		}

		public async Task<Article> GetById(int id)
		{
			var article = await _context.Articles.Include(a => a.Category).SingleOrDefaultAsync(a => a.ArticleId == id);
			return article;
		}

		public async Task<int> GetCountArticles()
		{
			var count = await _context.Articles.CountAsync();
			return count;
		}

		public async Task<IEnumerable<Article>> GetWithPaging(int categoryId, int pageNumber, int pageSize)
		{
			var articles = await _context.Articles
				.OrderBy(a => a.Title)
				.Skip((pageNumber - 1) * pageSize)
				.Take(pageSize)
				.ToListAsync();
			return articles;
		}

		public async Task<Article> Insert(Article entity)
		{
			_context.Articles.Add(entity);
			await _context.SaveChangesAsync();
			return entity;
		}

		public Task<Task> InsertArticleWithCategory(Article article)
		{
			throw new NotImplementedException();
		}

		public async Task<int> InsertWithIdentity(Article article)
		{
			_context.Articles.Add(article);
			await _context.SaveChangesAsync();
			return article.ArticleId;
		}

		public async Task<Article> Update(int id, Article entity)
		{
			var existingArticle = await _context.Articles.FindAsync(id);
			if (existingArticle == null)
				return null;

			existingArticle.ArticleId = entity.ArticleId;

			await _context.SaveChangesAsync();
			return existingArticle;
		}
	}
}
