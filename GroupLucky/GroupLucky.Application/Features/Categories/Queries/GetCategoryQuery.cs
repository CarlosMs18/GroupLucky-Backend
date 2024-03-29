﻿using AutoMapper;
using GroupLucky.Application.Contracts.Persistences;
using GroupLucky.Application.Mappings;
using GroupLucky.Domain;
using MediatR;

namespace GroupLucky.Application.Features.Categories.Queries
{
    public class GetCategoryQuery : IRequest<IEnumerable<GetCategoryQueryResponse>>
    {
    }

    public class GetCategoryQueryResponse
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
    }

    public static class GetCategoryQueryMapping
    {
        public static void AddMapGetCategoryQuery(this MappingProfile mappingProfile)
        {
            mappingProfile.CreateMap<Category,  GetCategoryQueryResponse>();
        }
    }

    public class GetCategoryQueryResponseHandler : IRequestHandler<GetCategoryQuery, IEnumerable<GetCategoryQueryResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public GetCategoryQueryResponseHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;   
            _mapper = mapper;
        }
        public async Task<IEnumerable<GetCategoryQueryResponse>> Handle(GetCategoryQuery request, CancellationToken cancellationToken)
        {
            var categories = await _unitOfWork.CategoryRepository.GetAll();
            return _mapper.Map<IEnumerable<GetCategoryQueryResponse>>(categories);
        }
    }
}
