using FluentValidation;

namespace Application.Batch.Queries.GetBatchDetail
{
    public class BatchDetailQueryValidator : AbstractValidator<BatchDetailQuery>
    {
        public BatchDetailQueryValidator()
        {
            RuleFor(v => v.BatchId).NotEmpty().NotNull();
        }
    }
}