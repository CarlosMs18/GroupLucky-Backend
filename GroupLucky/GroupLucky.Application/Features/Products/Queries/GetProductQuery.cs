using AutoMapper;
using GroupLucky.Application.Contracts.Persistences;
using GroupLucky.Application.Mappings;
using GroupLucky.Domain;
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
        public string State { get; set; }
    }

    public static class GetProductQueryMapping
    {
        public static void AddMapGetProductQuery(this MappingProfile mappingProfile)
        {
            mappingProfile.CreateMap<Product, GetProductQueryResponse>()
                .ForMember(d => d.ProductName, opt => opt.MapFrom(x => x.Name))
                .ForMember(d => d.CategoryName, opt => opt.MapFrom(x => x.Category.Name))   
                .ForMember(d => d.State, opt => opt.MapFrom(opt => opt.Active.Equals(1) ? "Activo" : "Inactivo"));
        }
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
            var products = await _unitOfWOrk.ProductRepository.GetAll();
            return _mapper.Map<IEnumerable<GetProductQueryResponse>>(products);
        }
    }
}
