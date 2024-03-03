using GroupLucky.Application.Contracts.Persistences;
using GroupLucky.Application.Exceptions;
using GroupLucky.Application.Features.Products.Queries;
using MediatR;

namespace GroupLucky.Application.Features.Products.Commands
{
    public class ProductDeleteCommand : IRequest<bool>
    {
        public int ProductId { get; set; }
    }

    public class ProductDeleteCommandHandler : IRequestHandler<ProductDeleteCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMediator _mediator;

        public ProductDeleteCommandHandler(IUnitOfWork unitOfWork, IMediator mediator)
        {
            _unitOfWork = unitOfWork;
            _mediator = mediator;
        }
        public async Task<bool> Handle(ProductDeleteCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var product = await _mediator.Send(new GetProductByIdQuery { ProductId = request.ProductId });

                await _unitOfWork.ProductRepository.Delete(product.Id);
                _unitOfWork.Commit();
            }
            catch
            {
                throw new BadRequestException("An error occurred while delete a product");
            }
            return true;
        }
    }
}
