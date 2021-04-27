using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Model;
using MediatR;

namespace Application.ProductData.Commands.GetProductVersions
{
    public class GetProductVersionsCommandHandler : IRequestHandler<GetProductVersionsCommand, SalesCatalogueResponseModel>
    {
        public GetProductVersionsCommandHandler()
        {
        }

        public async Task<SalesCatalogueResponseModel> Handle(GetProductVersionsCommand request, CancellationToken cancellationToken)
        {
            await Task.CompletedTask;
            return new SalesCatalogueResponseModel();
        }
    }
}