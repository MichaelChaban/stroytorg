using Stroytorg.Application.Constants;
using Stroytorg.Contracts.Models.Order;
using Stroytorg.Contracts.ResponseModels;
using DB = Stroytorg.Domain;

namespace Stroytorg.Application.Extensions;

public static class OrderExtensions
{
    private const int DeliveryToAddress = 1;

    public static bool ValidateOrder(this OrderCreate order, IEnumerable<DB.Data.Entities.Material> entityMaterials, out BusinessResponse<int>? businessResponse)
    {
        if (order.ShippingType == Contracts.Enums.ShippingType.DeliveryToAddress && string.IsNullOrEmpty(order.ShippingAddress))
        {
            businessResponse = new BusinessResponse<int>(
                IsSuccess: false,
                BusinessErrorMessage: BusinessErrorMessage.NoInformationProvided);
            return false;
        }

        if (!order.MaterialMap.ValidateOrderMaterials(entityMaterials, out var materialsBusinessResponse))
        {
            businessResponse = materialsBusinessResponse;
            return false;
        }

        businessResponse = null;
        return true;
    }

    private static bool ValidateOrderMaterials(this IEnumerable<MaterialMapCreate> materialMaps, IEnumerable<DB.Data.Entities.Material> entityMaterials, out BusinessResponse<int>? businessResponse)
    {
        if (entityMaterials is null || entityMaterials.Count() != materialMaps.Count())
        {
            businessResponse = new BusinessResponse<int>(
                IsSuccess: false,
                BusinessErrorMessage: BusinessErrorMessage.NotExistingEntities);
            return false;
        }

        foreach (var materialMap in materialMaps)
        {
            var material = entityMaterials.FirstOrDefault(x => x.Id == materialMap.MaterialId) ?? throw new ArgumentNullException(nameof(materialMap.MaterialId));
            if (materialMap.TotalMaterialAmount > material.StockAmount)
            {
                businessResponse = new BusinessResponse<int>(
                    IsSuccess: false,
                    BusinessErrorMessage: BusinessErrorMessage.NotExistingEntity);
                return false;
            }
        }

        businessResponse = null;
        return true;
    }
}
