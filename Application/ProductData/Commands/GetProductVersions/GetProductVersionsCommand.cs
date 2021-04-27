using System.Collections.Generic;
using Application.Common.Model;
using MediatR;

namespace Application.ProductData.Commands.GetProductVersions
{
    public class GetProductVersionsCommand : IRequest<SalesCatalogueResponseModel>
    {
        public List<ProductVersion> ProductVersions { get; set; }
        public string CallbackUri { get; set; }
    }

    public class ProductVersion
    {
        public string ProductName { get; set; }
        public int EditionNumber { get; set; }
        public int UpdateNumber { get; set; }
    }
}