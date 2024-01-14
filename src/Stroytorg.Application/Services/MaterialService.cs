using Stroytorg.Application.Constants;
using Stroytorg.Application.Services.Interfaces;
using Stroytorg.Contracts.Filters;
using Stroytorg.Contracts.RequestModels;
using Stroytorg.Contracts.ResponseModels;
using Stroytorg.Domain.Data.Repositories.Interfaces;
using DB = Stroytorg.Domain;
using Stroytorg.Domain.Sorting;
using Stroytorg.Domain.Specifications;
using Stroytorg.Infrastructure.AutoMapperTypeMapper;
using Stroytorg.Contracts.Models.Material;

namespace Stroytorg.Application.Services;

public class MaterialService : IMaterialService
{
    private readonly IAutoMapperTypeMapper autoMapperTypeMapper;
    private readonly IMaterialRepository materialRepository;
    private readonly ICategoryRepository categoryRepository;

    public MaterialService(
        IAutoMapperTypeMapper autoMapperTypeMapper,
        IMaterialRepository materialRepository,
        ICategoryRepository categoryRepository)
    {
        this.autoMapperTypeMapper = autoMapperTypeMapper ?? throw new ArgumentNullException(nameof(autoMapperTypeMapper));
        this.materialRepository = materialRepository ?? throw new ArgumentNullException(nameof(materialRepository));
        this.categoryRepository = categoryRepository ?? throw new ArgumentNullException(nameof(categoryRepository));
    }

    public async Task<PagedData<Material>> GetPagedAsync(DataRangeRequest<MaterialFilter> request)
    {
        var specification = autoMapperTypeMapper.Map<MaterialSpecification>(request?.Filter!);
        var filter = specification?.SatisfiedBy();

        var totalItems = await materialRepository.GetCountAsync(filter!);
        var items = await materialRepository.GetPagedSortAsync<MaterialSort>(request!.Offset, request.Limit, filter!, autoMapperTypeMapper.Map<DB.Sorting.Common.SortDefinition>(request.Sort));

        var mappedItems = autoMapperTypeMapper.Map<Material>(items);
        return new PagedData<Material>(
            Data: mappedItems,
            Total: totalItems);
    }

    public async Task<BusinessResponse<Material>> GetByIdAsync(int materialId)
    {
        var material = await materialRepository.GetAsync(materialId);
        if (material is null)
        {
            return new BusinessResponse<Material>(
                IsSuccess: false,
                BusinessErrorMessage: BusinessErrorMessage.NotExistingEntity);
        }

        return new BusinessResponse<Material>(
            Value: autoMapperTypeMapper.Map<Material>(material));
    }

    public async Task<BusinessResponse<int>> CreateAsync(MaterialCreate material)
    {
        var materialEntity = await materialRepository.GetByNameAsync(material.Name);
        if (materialEntity is not null)
        {
            return new BusinessResponse<int>(
                IsSuccess: false,
                BusinessErrorMessage: BusinessErrorMessage.AlreadyExistingEntity);
        }

        var category = await categoryRepository.GetAsync(material.CategoryId);
        if (category is null)
        {
            return new BusinessResponse<int>(
               IsSuccess: false,
               BusinessErrorMessage: BusinessErrorMessage.NotExistingEntity);
        }

        materialEntity = autoMapperTypeMapper.Map(material, materialEntity);

        await materialRepository.AddAsync(materialEntity!);
        await materialRepository.UnitOfWork.Commit();

        return new BusinessResponse<int>(
            Value: materialEntity!.Id);
    }

    public async Task<BusinessResponse<int>> UpdateAsync(int materialId, MaterialEdit material)
    {
        var materialEntity = await materialRepository.GetAsync(materialId);
        if (materialEntity is null)
        {
            return new BusinessResponse<int>(
                IsSuccess: false,
                BusinessErrorMessage: BusinessErrorMessage.NotExistingEntity);
        }

        var category = await categoryRepository.GetAsync(material.CategoryId);
        if (category is null)
        {
            return new BusinessResponse<int>(
               IsSuccess: false,
               BusinessErrorMessage: BusinessErrorMessage.NotExistingEntity);
        }

        materialEntity = autoMapperTypeMapper.Map(material, materialEntity);

        materialRepository.Update(materialEntity);
        await materialRepository.UnitOfWork.Commit();

        return new BusinessResponse<int>(
            Value: materialEntity.Id);
    }

    public async Task<BusinessResponse<int>> RemoveAsync(int materialId)
    {
        var materialEntity = await materialRepository.GetAsync(materialId);
        if (materialEntity is null)
        {
            return new BusinessResponse<int>(
                IsSuccess: false,
                BusinessErrorMessage: BusinessErrorMessage.NotExistingEntity);
        }

        materialRepository.Deactivate(materialEntity);
        await materialRepository.UnitOfWork.Commit();

        return new BusinessResponse<int>(
            Value: materialEntity.Id);
    }
}
