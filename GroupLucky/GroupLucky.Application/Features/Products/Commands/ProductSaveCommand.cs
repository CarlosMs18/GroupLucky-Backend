using GroupLucky.Application.Contracts.Persistences;
using GroupLucky.Application.Exceptions;
using GroupLucky.Application.Features.Categories.Commands;
using GroupLucky.Application.Features.Categories.Queries;
using GroupLucky.Domain;
using MediatR;

namespace GroupLucky.Application.Features.Products.Commands
{
    public class ProductSaveCommand : IRequest<ProductSaveCommandResponse>
    {
        public string? Code { get; set; }
        public string? Name { get; set; }
        public int StockMin { get; set; }
        public int StockMax { get; set; }
        public decimal UnitSalePrice { get; set; }
        public int CategoryId { get; set; }

    }

    public class ProductSaveCommandResponse
    {
        public bool Success { get; set; }
        public int ProductId { get; set; }
    }

    public class ProductSaveCommandHandler : IRequestHandler<ProductSaveCommand, ProductSaveCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMediator _mediator;

        public ProductSaveCommandHandler(IUnitOfWork unitOfWork, IMediator mediator)
        {
            _unitOfWork = unitOfWork;
            _mediator = mediator;   
        }

        public async Task<ProductSaveCommandResponse> Handle(ProductSaveCommand request, CancellationToken cancellationToken)
        {
            int productId;
            try
            {
                var category = await _mediator.Send(new GetCategoryByIdQuery { CategoryId = request.CategoryId });
                var product = new Product
                {
                    Name = request.Name,
                    Code = request.Code,
                    StockMin = request.StockMin,
                    StockMax = request.StockMax,
                    UnitSalePrice = request.UnitSalePrice,
                    CategoryId = request.CategoryId,
                };

                productId = await _unitOfWork.ProductRepository.Add(product);
                _unitOfWork.Commit();
            }
            catch
            {
                throw new BadRequestException("An error occurred while creating a product");
            }

            return new ProductSaveCommandResponse
            {
                Success = true,
                ProductId = productId
            };
        }
    }
}
