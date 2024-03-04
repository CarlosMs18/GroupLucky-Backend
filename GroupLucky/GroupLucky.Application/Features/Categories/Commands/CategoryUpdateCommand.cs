using GroupLucky.Application.Contracts.Persistences;
using GroupLucky.Application.Exceptions;
using GroupLucky.Application.Features.Categories.Queries;
using GroupLucky.Domain;
using MediatR;

namespace GroupLucky.Application.Features.Categories.Commands
{
    public class CategoryUpdateCommand : IRequest<bool>
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
    }

    public class CategoryUpdateCommandHandler :IRequestHandler<CategoryUpdateCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMediator _mediator;

        public CategoryUpdateCommandHandler(IUnitOfWork unitOfWork, IMediator mediator)
        {
            _unitOfWork = unitOfWork;
            _mediator = mediator;
        }

        public async Task<bool> Handle(CategoryUpdateCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var category = await _mediator.Send(new GetCategoryByIdQuery { CategoryId = request.Id });
                var categoryUpdated = new Category
                {
                    Id = category.Id,
                    Name = request.Name,
                    Description = request.Description,
                };

                await _unitOfWork.CategoryRepository.Update(categoryUpdated);
                _unitOfWork.Commit();
            }
            catch
            {
                throw new BadRequestException("An error occurred while updating a category");
            }
            return true;
        }
    }
}
