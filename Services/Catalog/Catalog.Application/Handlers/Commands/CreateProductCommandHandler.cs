

using Catalog.Application.Commands;
using Catalog.Application.Responses;
using Catalog.Core.Interfaces.Repositories;
using Catalog.Core.Entities;
using MediatR;
using AutoMapper;

namespace Catalog.Application.Handlers.Commands
{
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, ProductReponseDto>
    {
        private readonly IProductRepository repository;
        private readonly IMapper mapper;

        public CreateProductCommandHandler(IProductRepository productRepository, IMapper _mapper)
        {
            repository = productRepository;
            mapper = _mapper;
        }


        public async Task<ProductReponseDto> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var product = mapper.Map<Product>(request);
            await repository.AddProductAsync(product);
            return mapper.Map<ProductReponseDto>(product);
        }
    }   
}
