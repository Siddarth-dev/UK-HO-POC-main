using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using FluentValidation;
using FluentValidation.Validators;

namespace Application.ProductData.Commands.GetProductVersions
{
    public class GetProductVersionsCommandValidator : AbstractValidator<GetProductVersionsCommand>
    {
        public GetProductVersionsCommandValidator()
        {
            RuleFor(v => v.CallbackUri).NotNull().When(v => !string.IsNullOrEmpty(v.CallbackUri)).Matches(@"^http(s)?://([\w-]+.)+[\w-]+(/[\w- ./?%&=])?$");
            RuleFor(v => v.ProductVersions).NotNull().NotEmpty();
            // RuleFor(v => v.ProductVersions).Must(x => !IsDuplicate(x))
            // .WithMessage("The item with values has duplicates in collection of items");
            RuleFor(x => x.ProductVersions)
            .Must(coll => coll.Distinct(new SubEntityComparer()).Count() == coll.Count)
            .WithMessage("One or more items in collection of items are duplicates");
            //, (model, coll) => coll.Count); // has access to collection and to main model
            RuleForEach(v => v.ProductVersions).SetValidator(new ProductVersionsValidator());
        }

        private bool IsDuplicate(List<ProductVersion> productVersions)
        {
            return productVersions.GroupBy(n => n).Any(c => c.Count() > 1);
        }
    }

    public class ProductVersionsValidator : AbstractValidator<ProductVersion>
    {
        public ProductVersionsValidator()
        {
            RuleFor(v=>v.EditionNumber).GreaterThanOrEqualTo(0);
            RuleFor(v=>v.ProductName).NotNull().NotEmpty();
            RuleFor(v=>v.UpdateNumber).GreaterThanOrEqualTo(0);
        }
    }

    public class SubEntityComparer : IEqualityComparer<ProductVersion>
    {
        public bool Equals(ProductVersion x, ProductVersion y)
        {
            if (x == null ^ y == null)
                return false;

            if (ReferenceEquals(x, y))
                return true;

            // your equality comparison logic goes here:
            return x.EditionNumber == y.EditionNumber &&
                x.ProductName == y.ProductName &&
                x.UpdateNumber == y.UpdateNumber;
        }

        public int GetHashCode(ProductVersion obj)
        {
            return obj.EditionNumber.GetHashCode() + 37 * obj.ProductName.GetHashCode();
        }
    }
}