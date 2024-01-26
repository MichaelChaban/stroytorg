using FluentValidation;
using Stroytorg.Application.Constants;
using Stroytorg.Contracts.Models.Order;
using Stroytorg.Domain.Data.Repositories.Interfaces;
using DB = Stroytorg.Domain.Data.Entities;

namespace Stroytorg.Application.Features.Orders.CreateOrder;

internal class CreateOrderCommandValidator : AbstractValidator<CreateOrderCommand>
{
    private readonly IMaterialRepository materialRepository;

    public CreateOrderCommandValidator(IMaterialRepository materialRepository)
    {
        this.materialRepository = materialRepository ?? throw new ArgumentNullException(nameof(materialRepository));

        RuleFor(order => order.ShippingAddress)
            .NotEmpty()
            .When(order => order.ShippingType == Contracts.Enums.ShippingType.DeliveryToAddress)
            .WithMessage(BusinessErrorMessage.InvalidShippingInformation);

        RuleForEach(order => order.Materials)
            .MustAsync(CheckMaterialExistanceStockQuantityAsync);
    }

    private async Task<bool> CheckMaterialExistanceStockQuantityAsync(MaterialMapCreate material, CancellationToken cancellationToken)
    {
        var entityMaterial = await materialRepository.GetAsync(material.MaterialId, cancellationToken);

        return entityMaterial is not null
            && MaterialStockQuantityValidAsync(entityMaterial, material);
    }

    private static bool MaterialStockQuantityValidAsync(DB.Material entityMaterial, MaterialMapCreate material)
    {
        return material.TotalMaterialAmount > entityMaterial.StockAmount;
    }
}
