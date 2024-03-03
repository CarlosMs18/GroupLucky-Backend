using GroupLucky.Application.Contracts.Persistences;
using GroupLucky.Application.Exceptions;
using GroupLucky.Application.Features.Categories.Queries;
using GroupLucky.Application.Features.Products.Queries;
using GroupLucky.Domain;
using MediatR;

namespace GroupLucky.Application.Features.Products.Commands
{
    public class ProductUpdateCommand : IRequest<bool>
    {
        public int Id { get; set; }
        public string? Code { get; set; }
        public string? Name { get; set; }
        public int StockMin { get; set; }
        public int StockMax { get; set; }
        public decimal UnitSalePrice { get; set; }
        public int CategoryId { get; set; }
    }

    public class ProductUpdateCommandHandler : IRequestHandler<ProductUpdateCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMediator _mediator;

        public ProductUpdateCommandHandler(IUnitOfWork unitOfWork, IMediator mediator)
        {
            _unitOfWork = unitOfWork;
            _mediator = mediator;   
        }

        public async Task<bool> Handle(ProductUpdateCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var product = await _mediator.Send(new GetProductByIdQuery { ProductId = request.Id });
                var category = await _mediator.Send(new GetCategoryByIdQuery { CategoryId = request.CategoryId });
               
                var productUpdated = new Product
                {
                    Id = request.Id,
                    Code = request.Code,
                    Name = request.Name,
                    StockMin = request.StockMin,
                    StockMax = request.StockMax,
                    UnitSalePrice = request.UnitSalePrice,
                    CategoryId = request.CategoryId
                };
                await _unitOfWork.ProductRepository.Update(productUpdated);
                _unitOfWork.Commit();
            }
            catch
            {
                throw new BadRequestException("An error occurred while updating a product");
            }      
            return true;    
        }
    }
}
