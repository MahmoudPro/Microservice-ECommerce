using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Catalog.Application.Commands;
using Catalog.Core.Interfaces.Repositories;
using MediatR;

namespace Catalog.Application.Handlers.Commands
{
    public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, bool>
    {
        private readonly IProductRepository repository;
        public DeleteProductCommandHandler(IProductRepository productRepository)
        {
            repository = productRepository;
        }
        public async Task<bool> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            await repository.DeleteProductAsync(request.Id);
            return true;
        }
    }
}
