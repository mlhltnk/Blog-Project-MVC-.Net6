using EntityLayer.Concrete;
using FluentValidation;
using FluentValidation.AspNetCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.ValidationRules
{
    public class BlogValidator:AbstractValidator<Blog>
    {
        public BlogValidator()
        {
            RuleFor(x => x.BlogTitle).NotEmpty().WithMessage("Blog Başlığını Boş Geçemezsiniz");
            RuleFor(x => x.BlogContent).NotEmpty().WithMessage("Blog İçeriğini Boş Geçemezsiniz");
            RuleFor(x => x.BlogImage).NotEmpty().WithMessage("Blog Görselini Boş Geçemezsiniz");
            RuleFor(x => x.BlogTitle).MaximumLength(150).WithMessage("150 Karakterden daha az veri girişi yapın");
            RuleFor(x => x.BlogTitle).MinimumLength(5).WithMessage("4 Karakterden daha çok veri girişi yapın");

        }
    }
}
