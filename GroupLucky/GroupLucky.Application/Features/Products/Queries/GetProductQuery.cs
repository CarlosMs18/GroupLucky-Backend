using AutoMapper;
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
    }

    public class GetProductQueryHandler : IRequestHandler<GetProductQuery, IEnumerable<GetProductQueryResponse>>
    {
        private readonly IUnitOfWork _unitOfWOrk;
        private readonly IMapper _mapper;

        public GetProductQueryHandler(IUnitOfWork unitOfWOrk, IMapper mapper)
        {
            _unitOfWOrk = unitOfWOrk;
            _mapper = mapper;
        }

        public async Task<IEnumerable<GetProductQueryResponse>> Handle(GetProductQuery request, CancellationToken cancellationToken)
        {
            return await _unitOfWOrk.ProductRepository.GetAll();
        }
    }
}
