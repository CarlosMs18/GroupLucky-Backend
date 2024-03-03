using AutoMapper;
using GroupLucky.Application.Contracts.Persistences;
using GroupLucky.Application.Exceptions;
using GroupLucky.Application.Mappings;
using GroupLucky.Domain;
using MediatR;

namespace GroupLucky.Application.Features.Categories.Queries
{
    public class GetCategoryByIdQuery : IRequest<GetCategoryByIdQueryResponse>
    {
        public int CategoryId { get; set; }
    }

    public class GetCategoryByIdQueryResponse
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
    }

    public static class GetCategoryByIdQueryMapping
    {
        public static void AddMapGetCategoryByIdQuery(this MappingProfile mappingProfile)
        {
            mappingProfile.CreateMap<Category, GetCategoryByIdQueryResponse>();
        }
    }

    public class GetCategoryByIdQueryHandler : IRequestHandler<GetCategoryByIdQuery, GetCategoryByIdQueryResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetCategoryByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<GetCategoryByIdQueryResponse> Handle(GetCategoryByIdQuery request, CancellationToken cancellationToken)
        {
            var category = await _unitOfWork.CategoryRepository.GetCategory(request.CategoryId);
            if(category.Id == 0)
            {
                throw new BadRequestException("The category does not exist");
            }
            return _mapper.Map<GetCategoryByIdQueryResponse>(category);
        }
    }
}
