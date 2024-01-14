﻿using Stroytorg.Domain.Specifications.Common;
using Stroytorg.Infrastructure.Specifications.Common;
using Stroytorg.Infrastructure.Specifications.Interfaces;
using Stroytorg.Infrastructure.Specifications;
using System.Linq.Expressions;
using Stroytorg.Domain.Data.Entities;

namespace Stroytorg.Domain.Specifications;

public class MaterialSpecification : BaseSpecification, ISpecification<Material>
{
    public string? Name { get; set; }

    public int? CategoryId { get; set; }

    public double? MinPrice { get; set; }

    public double? MaxPrice { get; set; }

    public double? MinStockAmount { get; set; }

    public double? MaxStockAmount { get; set; }

    public bool? IsFavorite { get; set; }

    public double? MinHeight { get; set; }

    public double? MaxHeight { get; set; }

    public double? MinWidth { get; set; }

    public double? MaxWidth { get; set; }

    public double? MinLength { get; set; }

    public double? MaxLength { get; set; }

    public double? MinWeight { get; set; }

    public double? MaxWeight { get; set; }

    public Expression<Func<Material, bool>> SatisfiedBy()
    {
        Specification<Material> specification = new TrueSpecification<Material>();

        if (Id != 0)
        {
            specification &= new DirectSpecification<Material>(x => x.Id == Id);
        }

        if (IsActive.HasValue)
        {
            specification &= new DirectSpecification<Material>(x => x.IsActive == IsActive.Value);
        }

        if (Id != 0)
        {
            specification &= new DirectSpecification<Material>(x => x.Id == Id);
        }

        if (!string.IsNullOrEmpty(Name))
        {
            specification &= new DirectSpecification<Material>(x =>
                x.Name.ToUpper().Contains(Name.ToUpper()));
        }

        if (CategoryId.HasValue)
        {
            specification &= new DirectSpecification<Material>(x => x.CategoryId == CategoryId);
        }

        if (MinPrice.HasValue)
        {
            specification &= new DirectSpecification<Material>(x => x.Price >= MinPrice);
        }

        if (MaxPrice.HasValue)
        {
            specification &= new DirectSpecification<Material>(x => x.Price <= MaxPrice);
        }

        if (MinStockAmount.HasValue)
        {
            specification &= new DirectSpecification<Material>(x => x.StockAmount >= MinStockAmount);
        }

        if (MaxStockAmount.HasValue)
        {
            specification &= new DirectSpecification<Material>(x => x.StockAmount <= MaxStockAmount);
        }

        if (IsFavorite.HasValue)
        {
            specification &= new DirectSpecification<Material>(x => x.IsFavorite == IsFavorite);
        }

        if (MinHeight.HasValue)
        {
            specification &= new DirectSpecification<Material>(x => x.Height >= MinHeight);
        }

        if (MaxHeight.HasValue)
        {
            specification &= new DirectSpecification<Material>(x => x.Height <= MaxHeight);
        }

        if (MinWidth.HasValue)
        {
            specification &= new DirectSpecification<Material>(x => x.Height >= MinWidth);
        }

        if (MaxWidth.HasValue)
        {
            specification &= new DirectSpecification<Material>(x => x.Height <= MaxWidth);
        }

        if (MinLength.HasValue)
        {
            specification &= new DirectSpecification<Material>(x => x.Length >= MinLength);
        }

        if (MaxLength.HasValue)
        {
            specification &= new DirectSpecification<Material>(x => x.Length <= MaxLength);
        }

        if (MinWeight.HasValue)
        {
            specification &= new DirectSpecification<Material>(x => x.Weight >= MinWeight);
        }

        if (MaxWeight.HasValue)
        {
            specification &= new DirectSpecification<Material>(x => x.Weight <= MaxWeight);
        }

        return specification.SatisfiedBy();
    }
}