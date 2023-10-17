using AutoMapper;
using GloboDelivery.Domain.Helpers;

namespace GloboDelivery.Application.MappingProfiles
{
    public class PagedListConverter<TSource, TDestination> : ITypeConverter<PagedList<TSource>, PagedList<TDestination>>
    {
        public PagedList<TDestination> Convert(PagedList<TSource> source, PagedList<TDestination> destination, ResolutionContext context)
        {
            var items = context.Mapper.Map<List<TDestination>>(source);
            return new PagedList<TDestination>(items, source.TotalCount, source.CurrentPage, source.PageSize);
        }
    }
}
