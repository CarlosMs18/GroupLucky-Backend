using GroupLucky.Application.Contracts.Persistences;
using GroupLucky.Application.Features.Categories.Commands;
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

        public ProductSaveCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ProductSaveCommandResponse> Handle(ProductSaveCommand request, CancellationToken cancellationToken)
        {
            var product = new Product
            {
                Name = request.Name,
                Code = request.Code,
                StockMin = request.StockMin,
                StockMax = request.StockMax,
                UnitSalePrice = request.UnitSalePrice,
                CategoryId = request.CategoryId,
            };

            var productId = await _unitOfWork.ProductRepository.Add(product);
            _unitOfWork.Commit();
            return new ProductSaveCommandResponse
            {
                Success = true,
                ProductId = productId
            };
        }
    }
}
