﻿using AutoMapper;

namespace Stroytorg.Infrastructure.AutoMapperTypeMapper;

public class AutoMapperTypeMapper : IAutoMapperTypeMapper
{
    private readonly IMapper mapper;

    public AutoMapperTypeMapper(IMapper mapper)
    {
        this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    public TTarget Map<TSource, TTarget>(TSource source)
    {
        return mapper.Map<TSource, TTarget>(source);
    }

    public TTarget Map<TSource, TTarget>(TSource source, TTarget target)
    {
        return mapper.Map(source, target);
    }

    public TTarget Map<TTarget>(object source)
    {
        return mapper.Map<TTarget>(source);
    }

    public IEnumerable<TTarget> Map<TTarget>(IEnumerable<object> source)
    {
        return mapper.Map<IEnumerable<TTarget>>(source);
    }
}
