using GloboDelivery.API.Models;
using GloboDelivery.Application.Models;
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

        public static PaginationMetadata CreatePaginationMetadata<TEntity, TParameters>(PagedList<TEntity> entities, IUrlHelper urlHelper, string actionName, TParameters? routeParameters = null) 
            where TParameters : PaginationModel
        {
            return new PaginationMetadata
            {
                TotalCount = entities.TotalCount,
                PageSize = entities.PageSize,
                CurrentPage = entities.CurrentPage,
                TotalPages = entities.TotalPages,
                CurrentPageLink = CreateResourceUri(entities, urlHelper, actionName, routeParameters, ResourceUriType.Current),
                PreviousPageLink = entities.HasPrevious ? CreateResourceUri(entities, urlHelper, actionName, routeParameters, ResourceUriType.PreviousPage) : null,
                NextPageLink = entities.HasNext ? CreateResourceUri(entities, urlHelper, actionName, routeParameters, ResourceUriType.NextPage) : null,
            };
        }

        private static string? CreateResourceUri<TEntity, TParameters>(PagedList<TEntity> entities, IUrlHelper urlHelper, string actionName, TParameters? routeParameters, ResourceUriType resourceUriType)
            where TParameters : PaginationModel
        {
            switch (resourceUriType)
            {
                case ResourceUriType.PreviousPage:
                    if (routeParameters != null)
                    {
                        routeParameters.PageNumber = entities.CurrentPage - 1;
                        routeParameters.PageSize = entities.PageSize;
                    }
                    return urlHelper.Link(actionName,
                         routeParameters != null ? 
                         routeParameters : 
                         new
                         {
                             PageNumber = entities.CurrentPage,
                             PageSize = entities.PageSize
                         });
                case ResourceUriType.NextPage:
                    if (routeParameters != null)
                    {
                        routeParameters.PageNumber = entities.CurrentPage + 1;
                        routeParameters.PageSize = entities.PageSize;
                    }
                    return urlHelper.Link(actionName,
                        routeParameters != null ?
                        routeParameters :
                        new
                        {
                            PageNumber = entities.CurrentPage + 1,
                            PageSize = entities.PageSize
                        });
                case ResourceUriType.Current:
                default:
                    if (routeParameters != null)
                    {
                        routeParameters.PageNumber = entities.CurrentPage;
                        routeParameters.PageSize = entities.PageSize;
                    }
                    return urlHelper.Link(actionName,
                        routeParameters != null ?
                        routeParameters :
                        new
                        {
                            PageNumber = entities.CurrentPage,
                            PageSize = entities.PageSize
                        });
            }
        }
    }
}
