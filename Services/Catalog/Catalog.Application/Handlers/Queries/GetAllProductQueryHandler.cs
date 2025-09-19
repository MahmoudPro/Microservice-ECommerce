using AutoMapper;
using Catalog.Application.Queries;
using Catalog.Application.Responses;
using Catalog.Core.Interfaces.Repositories;
using MediatR;

namespace Catalog.Application.Handlers.Queries
{
    public class GetAllProductQueryHandler : IRequestHandler<GetAllProductQuery, IList<ProductReponseDto>>
    {
        private readonly IProductRepository repository;
        private readonly IMapper mapper;
        public GetAllProductQueryHandler(IProductRepository _repository, IMapper _mapper)
        {
            repository = _repository;
            mapper = _mapper;
        }

        public async Task<IList<ProductReponseDto>> Handle(GetAllProductQuery request, CancellationToken cancellationToken)
        {
            var products = await repository.GetAllProductsAsync();
            return mapper.Map<IList<ProductReponseDto>>(products);
        }
    }
}
