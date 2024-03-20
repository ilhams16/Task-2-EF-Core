using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace MyRESTServices.BLL.DTOs.Validation
{
	public class ArticleUpdateDTOValidator : AbstractValidator<ArticleUpdateDTO>
	{
		public ArticleUpdateDTOValidator() 
		{
			RuleFor(x => x.Title).NotEmpty().WithMessage("Title harus diisi");
			RuleFor(x => x.Title).MaximumLength(255).WithMessage("Title maksimal 255 karakter");
			RuleFor(x => x.CategoryID).NotEmpty().WithMessage("Category harus diisi");
		}
	}
}
