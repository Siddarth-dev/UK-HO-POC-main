using System.Linq;
using Application.Batch.Queries.GetBatchDetail;
using FluentValidation;
using FluentValidation.Validators;
using Persistence;

namespace Application.Batch.Commands.CreateBatch
{
    public class CreateBatchCommandValidator : AbstractValidator<CreateBatchCommand>
    {
        private readonly DataContext _context;
        public CreateBatchCommandValidator(DataContext context)
        {
            _context = context;
            RuleFor(v => v.BusinessUnit).NotEmpty().NotNull().BatchBusinessUnitValidator(context);
            RuleFor(v => v.Attributes).NotEmpty().NotNull();
            RuleForEach(v => v.Attributes).SetValidator(new BatchAttributeValidator());
        }
    }

    public class BatchAttributeValidator : AbstractValidator<BatchAttributeDetailModel>
    {
        public BatchAttributeValidator()
        {
            RuleFor(v => v.Key).NotEmpty().NotNull();
            RuleFor(v => v.Value).NotEmpty().NotNull();
        }
    }

    public class BatchBusinessUnitValidator : PropertyValidator
    {
        private readonly DataContext _context;
        
        public BatchBusinessUnitValidator(DataContext context)
            : base("Business Unit doesn't exist.")
        {
            _context = context;
        }
        protected override bool IsValid(PropertyValidatorContext context)
        {
            if (context.PropertyValue == null) return false;
            return _context.BusinessUnities.Where(a=> a.BusinessUnitName.Contains(context.PropertyValue as string) && a.IsActive).Any();
        }
    }

    public static class CustomValidatorExtensions
    {
        public static IRuleBuilderOptions<T, string> BatchBusinessUnitValidator<T>(
            this IRuleBuilder<T, string> ruleBuilder, DataContext context)
        {
            return ruleBuilder.SetValidator(new BatchBusinessUnitValidator(context));
        }
    }
}