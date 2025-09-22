using Basket.Application.Commands;
using Basket.Core.Repositories;
using MediatR;

namespace Basket.Application.Handlers.Commands
{
    public class DeleteBasketByUserNameCommandHandler : IRequestHandler<DeleteBasketByUserNameCommand, Unit>
    {
        private readonly IBasketRepository _basketRepository;

        public DeleteBasketByUserNameCommandHandler(IBasketRepository basketRepository)
        {
            _basketRepository = basketRepository;
        }

        public Task<Unit> Handle(DeleteBasketByUserNameCommand request, CancellationToken cancellationToken)
        {
            return _basketRepository.DeleteBasket(request.UserName)
                .ContinueWith(t => Unit.Value, cancellationToken);
        }
    }
}
