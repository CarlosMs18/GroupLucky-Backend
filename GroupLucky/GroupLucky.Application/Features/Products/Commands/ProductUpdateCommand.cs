using GroupLucky.Application.Contracts.Persistences;
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
        public readonly IUnitOfWork _unitOfWork;

        public ProductUpdateCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(ProductUpdateCommand request, CancellationToken cancellationToken)
        {
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
            return true;    
        }
    }
}
