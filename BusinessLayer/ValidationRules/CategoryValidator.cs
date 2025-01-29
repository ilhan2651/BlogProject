using EntityLayer.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.ValidationRules
{
    public class CategoryValidator : AbstractValidator<Category>
    {
        public CategoryValidator()
        {
                        RuleFor(x => x.CategoryName)
    .NotEmpty().WithMessage("Kategori adını boş geçemezsiniz")
    .Length(2, 50).WithMessage("Kategori adı 2 ile 50 karakter arasında olmalıdır");


            RuleFor(x => x.CategoryDescription).NotEmpty().WithMessage("Kategori açıklamasını boş geçemezsiniz")
              .MinimumLength(5).WithMessage("Kategori açıklaması en az 5 karakter olmalıdır");
        }

    }
}
