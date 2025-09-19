using Catalog.Application.Responses;
using Catalog.Core.Specs;
using MediatR;

namespace Catalog.Application.Queries
{
    public class GetAllProductQuery: IRequest<Pagination<ProductReponseDto>>
    {
        public CatalogSpecParams SpecParams { get; set; }
        public GetAllProductQuery(CatalogSpecParams catalogSpec)
        {
            SpecParams = catalogSpec;
        }
    }
}
