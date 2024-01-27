using FluentValidation;
using FluentValidation.Results;
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

        RuleForEach(order => order.Materials)
            .CustomAsync(CheckMaterialExistanceStockQuantityAsync);

        RuleFor(order => order.ShippingAddress)
            .NotEmpty()
            .When(order => order.ShippingType == Contracts.Enums.ShippingType.DeliveryToAddress)
            .WithErrorCode(nameof(CreateOrderCommand.ShippingAddress))
            .WithMessage(BusinessErrorMessage.InvalidShippingInformation);
    }

    private async Task<bool> CheckMaterialExistanceStockQuantityAsync(MaterialMapCreate material, ValidationContext<CreateOrderCommand> context, CancellationToken cancellationToken)
    {
        var entityMaterial = await materialRepository.GetAsync(material.MaterialId, cancellationToken);
        
        if (entityMaterial is null)
        {
            context.AddFailure(
                new ValidationFailure()
                {
                    PropertyName = nameof(MaterialMapCreate.MaterialId),
                    ErrorCode = nameof(MaterialMapCreate.MaterialId),
                    ErrorMessage = $"{BusinessErrorMessage.NotExistingMaterialWithId} {material.MaterialId}"
                });
            return false;
        }

        return MaterialStockQuantityValid(entityMaterial, material, context);
    }

    private static bool MaterialStockQuantityValid(DB.Material entityMaterial, MaterialMapCreate material, ValidationContext<CreateOrderCommand> context)
    {
        if (material.TotalMaterialAmount > entityMaterial.StockAmount)
        {
            context.AddFailure(
                new ValidationFailure()
                {
                    PropertyName = nameof(MaterialMapCreate.TotalMaterialAmount),
                    ErrorCode = $"{nameof(MaterialMapCreate.TotalMaterialAmount)}: {nameof(MaterialMapCreate.MaterialId)} {material.MaterialId}",
                    ErrorMessage = string.Format(BusinessErrorMessage.InvalidOrderMaterialAmount, material.MaterialId)
        });
            return false;
        }

        return true;
    }
}
