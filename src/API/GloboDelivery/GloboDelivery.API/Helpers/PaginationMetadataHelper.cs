using GloboDelivery.API.Models;
using GloboDelivery.Domain.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace GloboDelivery.API.Helpers
{
    public static class PaginationMetadataHelper
    {
        private enum ResourceUriType
        {
            PreviousPage,
            NextPage,
            Current
        }

        public static PaginationMetadata CreatePaginationMetadata<T>(PagedList<T> entities, IUrlHelper urlHelper, string actionName)
        {
            return new PaginationMetadata
            {
                TotalCount = entities.TotalCount,
                PageSize = entities.PageSize,
                CurrentPage = entities.CurrentPage,
                TotalPages = entities.TotalPages,
                CurrentPageLink = CreateResourceUri(entities, urlHelper, actionName, ResourceUriType.Current),
                PreviousPageLink = entities.HasPrevious ? CreateResourceUri(entities, urlHelper, actionName, ResourceUriType.PreviousPage) : null,
                NextPageLink = entities.HasNext ? CreateResourceUri(entities, urlHelper, actionName, ResourceUriType.NextPage) : null,
            };
        }

        private static string? CreateResourceUri<T>(PagedList<T> entities, IUrlHelper urlHelper, string actionName, ResourceUriType resourceUriType)
        {
            switch (resourceUriType)
            {
                case ResourceUriType.PreviousPage:
                    return urlHelper.Link(actionName,
                    new
                    {
                        pageNumber = entities.CurrentPage - 1,
                        pageSize = entities.PageSize
                    });
                case ResourceUriType.NextPage:
                    return urlHelper.Link(actionName,
                    new
                    {
                        pageNumber = entities.CurrentPage + 1,
                        pageSize = entities.PageSize
                    });
                case ResourceUriType.Current:
                default:
                    return urlHelper.Link(actionName,
                    new
                    {
                        pageNumber = entities.CurrentPage,
                        pageSize = entities.PageSize
                    });
            }
        }
    }
}
