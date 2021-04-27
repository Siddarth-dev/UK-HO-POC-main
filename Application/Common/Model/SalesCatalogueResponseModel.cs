using System;
using System.Collections.Generic;

namespace Application.Common.Model
{
    public class SalesCatalogueResponseModel
    {
        public Links _links { get; set; }
        public DateTime ExchangeSetUrlExpiryDateTime { get; set; }
        public int RequestedProductCount { get; set; }
        public int ExchangeSetCellCount { get; set; }
        public int RequestedProductsAlreadyUpToDateCount { get; set; }
        public List<ProductsNotInExchangeSet> RequestedProductsNotInExchangeSet { get; set; }
    }

    public class Links
    {
        public ExchangeSetUrl ExchangeSetBatchStatusUri { get; set; }
        public ExchangeSetUrl ExchangeSetFileUri { get; set; }
    }

    public class ExchangeSetUrl
    {
        public string Href { get; set; }
    }

    public class ProductsNotInExchangeSet
    {
        public string ProductName { get; set; }
        public string Reason { get; set; }
    }
}