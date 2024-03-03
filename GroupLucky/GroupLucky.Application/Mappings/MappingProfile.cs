using AutoMapper;
using GroupLucky.Application.Features.Categories.Commands;
using GroupLucky.Application.Features.Categories.Queries;
using GroupLucky.Application.Features.Products.Queries;

namespace GroupLucky.Application.Mappings
{
    public class MappingProfile : Profile
    {   
        public MappingProfile()
        {
            this.AddMapGetCategoryQuery();
            this.AddMapGetProductByIdQuery();
 
        }
    }
}
