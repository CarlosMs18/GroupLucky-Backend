using AutoMapper;
using GroupLucky.Application.Features.Categories.Queries;

namespace GroupLucky.Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            this.AddMapGetCategoryQuery();
        }
    }
}
