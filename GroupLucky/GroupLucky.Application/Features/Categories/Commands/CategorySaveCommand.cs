using GroupLucky.Application.Contracts.Persistences;
using GroupLucky.Application.Mappings;
using GroupLucky.Domain;
using MediatR;

namespace GroupLucky.Application.Features.Categories.Commands
{
    public class CategorySaveCommand : IRequest<CategorySaveCommandResponse>
    {
        public string Name { get; set; }
        public string Description { get; set; }


    }
    public class CategorySaveCommandResponse
    {
        public bool Success { get; set; }
        public int CategoryId { get; set; }
    }

   
    public class CategorySaveCommandHandler : IRequestHandler<CategorySaveCommand, CategorySaveCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CategorySaveCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<CategorySaveCommandResponse> Handle(CategorySaveCommand request, CancellationToken cancellationToken)
        {
            var category = new Category
            {
                Name = request.Name,
                Description = request.Description,  
            };

            var id = await _unitOfWork.CategoryRepository.Add(category);
             _unitOfWork.Commit();
            return new CategorySaveCommandResponse
            {
                Success = true,
                CategoryId = id
            };
        }
    }
}
