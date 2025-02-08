using EntityLayer.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.ValidationRules
{
    public class CommentValidator : AbstractValidator<Comment>
    {
        public CommentValidator()
        {
            RuleFor(x => x.CommentUserName)
            .NotEmpty().WithMessage("Ad soyad alanı boş bırakılamaz!");

            RuleFor(x => x.CommentContent)
                .NotEmpty().WithMessage("Yorum içeriği boş bırakılamaz!");

            RuleFor(x => x.BlogScore)
                .InclusiveBetween(0, 10).WithMessage("Puan 0 ile 10 arasında olmalıdır!");
        }
    }
}
