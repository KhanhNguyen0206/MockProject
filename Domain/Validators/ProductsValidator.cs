using Domain.Models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Validators
{
    public class ProductsValidator : AbstractValidator<Product>
    {
        public ProductsValidator()
        {
            RuleFor(x => x.Name).Length(2, 100);
            RuleFor(x => x.Description).Length(2, 1000).NotEmpty().WithMessage("Description cannot be null or empty");
            RuleFor(x => x.Price).ExclusiveBetween(0, 100000000).NotNull().WithMessage("Price cannot be null");
            RuleFor(x => x.ImageUrl).NotNull().WithMessage("Price cannot be null"); ;
        }
    }
}
