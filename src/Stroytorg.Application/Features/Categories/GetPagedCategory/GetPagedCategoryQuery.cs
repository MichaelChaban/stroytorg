﻿using MediatR;
using Stroytorg.Contracts.Filters;
using Stroytorg.Contracts.Models.Category;
using Stroytorg.Contracts.ResponseModels;
using Stroytorg.Contracts.Sorting;
using System.ComponentModel.DataAnnotations;

namespace Stroytorg.Application.Features.Categories.GetPagedCategory;

public record GetPagedCategoryQuery<TFilter>(
    TFilter? Filter,
    SortDefinition Sort = default!,

    [Range(0, 500)]
    int Offset = 0,

    [Range(0, 1000000)]
    int Limit = 50)
    : IRequest<PagedData<Category>>
    where TFilter : CategoryFilter;
