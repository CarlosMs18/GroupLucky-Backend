using AutoMapper;
using GroupLucky.Application.Contracts.Persistences;
using GroupLucky.Application.Exceptions;
using GroupLucky.Application.Mappings;
using GroupLucky.Domain;
using GroupLucky.Domain.Enum;
using MediatR;

namespace GroupLucky.Application.Features.Products.Queries
{
    public class GetProductByIdQuery : IRequest<GetProductByIdQueryResponse>
    {
        public int ProductId { get; set; }
    }
    public class GetProductByIdQueryResponse
    {
        public int Id { get; set; }
        public string? Code { get; set; }   
        public string? Name { get; set; }
        public int StockMin { get; set; }
        public int StockMax { get; set;}
        public decimal UnitSalePrice { get; set; }
        public string? State { get; set; }
    }

    public static class GetProductByIdQueryMapping
    {
        public static void AddMapGetProductByIdQuery(this MappingProfile mappingProfile)
        {
            mappingProfile.CreateMap<Product, GetProductByIdQueryResponse>()
                .ForMember(d => d.State, opt => opt.MapFrom(opt => opt.Active.Equals((int)StateTypes.Active) ? "Activo" : "Inactivo"));
        }
    }

    public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, GetProductByIdQueryResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        

        public GetProductByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<GetProductByIdQueryResponse> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            
            var product = await _unitOfWork.ProductRepository.GetProduct(request.ProductId);
            if(product is null)
            {
                throw new BadRequestException("The product does not exist");
            }
            return _mapper.Map<GetProductByIdQueryResponse>(product);
        }
    }
}
