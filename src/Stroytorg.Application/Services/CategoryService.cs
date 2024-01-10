using Stroytorg.Application.Constants;
using Stroytorg.Application.Services.Interfaces;
using Stroytorg.Contracts.Filters;
using Stroytorg.Contracts.Models.Category;
using Stroytorg.Contracts.RequestModels;
using Stroytorg.Contracts.ResponseModels;
using DB = Stroytorg.Domain;
using Stroytorg.Domain.Data.Repositories.Interfaces;
using Stroytorg.Domain.Sorting;
using Stroytorg.Domain.Specifications;
using Stroytorg.Infrastructure.AutoMapperTypeMapper;

namespace Stroytorg.Application.Services;

public class CategoryService : ICategoryService
{
    private readonly IAutoMapperTypeMapper autoMapperTypeMapper;
    private readonly ICategoryRepository categoryRepository;

    public CategoryService(
        IAutoMapperTypeMapper autoMapperTypeMapper,
        ICategoryRepository categoryRepository)
    {
        this.autoMapperTypeMapper = autoMapperTypeMapper ?? throw new ArgumentNullException(nameof(autoMapperTypeMapper));
        this.categoryRepository = categoryRepository ?? throw new ArgumentNullException(nameof(categoryRepository));
    }

    public async Task<PagedData<Category>> GetPagedAsync(DataRangeRequest<CategoryFilter> request)
    {
        var specification = autoMapperTypeMapper.Map<CategorySpecification>(request?.Filter!);
        var filter = specification?.SatisfiedBy();

        var totalItems = await categoryRepository.GetCountAsync(filter!);
        var items = await categoryRepository.GetPagedSortAsync<CategorySort>(request!.Offset, request.Limit, filter!, autoMapperTypeMapper.Map<DB.Sorting.Common.SortDefinition>(request.Sort));

        var mappedItems = autoMapperTypeMapper.Map<Category>(items);
        return new PagedData<Category>(
            Data: mappedItems,
            Total: totalItems);
    }

    public async Task<BusinessResponse<Category>> GetByIdAsync(int categoryId)
    {
        var category = await categoryRepository.GetAsync(categoryId);
        if(category is null)
        {
            return new BusinessResponse<Category>(
                IsSuccess: false,
                BusinessErrorMessage: BusinessErrorMessage.NotExistingEntity);
        }

        return new BusinessResponse<Category>(
            Value: autoMapperTypeMapper.Map<Category>(category));
    }

    public async Task<BusinessResponse<int>> CreateAsync(CategoryEdit category)
    {
        var categoryEntity = await categoryRepository.GetByNameAsync(category.Name);
        if (categoryEntity is not null)
        {
            return new BusinessResponse<int>(
                IsSuccess: false,
                BusinessErrorMessage: BusinessErrorMessage.AlreadyExistingEntity);
        }

        categoryEntity = autoMapperTypeMapper.Map<DB.Data.Entities.Category>(category);

        await categoryRepository.AddAsync(categoryEntity);
        await categoryRepository.UnitOfWork.Commit();

        return new BusinessResponse<int>(
            Value: categoryEntity.Id);
    }

    public async Task<BusinessResponse<int>> UpdateAsync(int categoryId, CategoryEdit category)
    {
        var categoryEntity = await categoryRepository.GetAsync(categoryId);
        if (categoryEntity is null)
        {
            return new BusinessResponse<int>(
                IsSuccess: false,
                BusinessErrorMessage: BusinessErrorMessage.NotExistingEntity);
        }

        categoryEntity = autoMapperTypeMapper.Map<DB.Data.Entities.Category>(category);

        categoryRepository.Update(categoryEntity);
        await categoryRepository.UnitOfWork.Commit();

        return new BusinessResponse<int>(
            Value: categoryEntity.Id);
    }

    public async Task<BusinessResponse<int>> RemoveAsync(int categoryId)
    {
        var categoryEntity = await categoryRepository.GetAsync(categoryId);
        if (categoryEntity is null)
        {
            return new BusinessResponse<int>(
                IsSuccess: false,
                BusinessErrorMessage: BusinessErrorMessage.NotExistingEntity);
        }

        categoryRepository.Remove(categoryEntity);
        await categoryRepository.UnitOfWork.Commit();

        return new BusinessResponse<int>(
            Value: categoryEntity.Id);
    }
}
