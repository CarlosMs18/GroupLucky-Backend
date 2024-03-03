using GroupLucky.Application.Contracts.Persistences;
using MediatR;

namespace GroupLucky.Application.Features.Products.Queries
{
    public class GetProductQuery: IRequest<IEnumerable<GetProductQueryResponse>>
    {
    }

    public class GetProductQueryResponse
    {
        public int Id { get; set; }
        public string? ProductName { get; set; }
        public int StockMin { get; set; }
        public int StockMax { get; set; }
        public decimal UnitSalePrice { get; set; }
        public string? CategoryName { get; set; }
        public int Active { get; set; }
    }

    public class GetProductQueryHandler : IRequestHandler<GetProductQuery, IEnumerable<GetProductQueryResponse>>
    {
        private readonly IUnitOfWork _unitOfWOrk;

        public GetProductQueryHandler(IUnitOfWork unitOfWOrk)
        {
            _unitOfWOrk = unitOfWOrk;
        }

        public async Task<IEnumerable<GetProductQueryResponse>> Handle(GetProductQuery request, CancellationToken cancellationToken)
        {
            return await _unitOfWOrk.ProductRepository.GetAll();
        }
    }
}
