namespace Stroytorg.Infrastructure.AutoMapperTypeMapper;

public interface IAutoMapperTypeMapper
{
    TTarget Map<TSource, TTarget>(TSource source);

    TTarget Map<TSource, TTarget>(TSource source, TTarget target);

    IEnumerable<TTarget> Map<TTarget>(IEnumerable<object> source);

    TTarget Map<TTarget>(object source);
}
