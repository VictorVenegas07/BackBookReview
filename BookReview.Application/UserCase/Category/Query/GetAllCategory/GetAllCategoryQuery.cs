using AutoMapper;
using BookReview.Application.UserCase.Category.Dtos;
using BookReview.Domain.Common.Wrappers;
using BookReview.Domain.Ports;
using MediatR;

namespace BookReview.Application.UserCase.Category.Query.GetAllCategory;

public record GetAllCategoryQuery : IRequest<Response<IEnumerable<CategoryResponse>>>;

public class GetAllCategoryQueryHandler : IRequestHandler<GetAllCategoryQuery, Response<IEnumerable<CategoryResponse>>>
{
    private readonly IGenericRepository<Domain.Entities.Category> _categoryRepository;
    private readonly IMapper _mapper;
    public GetAllCategoryQueryHandler(IGenericRepository<Domain.Entities.Category> categoryRepository, IMapper mapper)
    {
        _categoryRepository = categoryRepository;
        _mapper = mapper;
    }
    public async Task<Response<IEnumerable<CategoryResponse>>> Handle(GetAllCategoryQuery request, CancellationToken cancellationToken)
    {
        var categories = await _categoryRepository.GetAsync();
        var mappedCategories = _mapper.Map<IEnumerable<CategoryResponse>>(categories);
        return new Response<IEnumerable<CategoryResponse>>(mappedCategories, "Successful query.");
    }
}
