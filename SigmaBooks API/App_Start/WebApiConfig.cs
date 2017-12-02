using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace SigmaBooks_API
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{advancedSearch}/{useTitle}/{useAuthor}/{useDescription}/{limitPrice}/{minPrice}/{maxPrice}/{limitDate}/{minDate}/{maxDate}/{limitGenre}/{genreComputer}/{genreFantasy}/{genreHorror}/{genreRomance}/{genreScienceFiction}/{searchString}",
                defaults: new {
                    advancedSearch = RouteParameter.Optional,
                    useTitle = RouteParameter.Optional,
                    useAuthor = RouteParameter.Optional,
                    useDescription = RouteParameter.Optional,
                    limitPrice = RouteParameter.Optional,
                    minPrice = RouteParameter.Optional,
                    maxPrice = RouteParameter.Optional,
                    limitDate = RouteParameter.Optional,
                    minDate = RouteParameter.Optional,
                    maxDate = RouteParameter.Optional,
                    limitGenre = RouteParameter.Optional,
                    genreComputer = RouteParameter.Optional,
                    genreFantasy = RouteParameter.Optional,
                    genreHorror = RouteParameter.Optional,
                    genreRomance = RouteParameter.Optional,
                    genreScienceFiction = RouteParameter.Optional,
                    searchString = RouteParameter.Optional
                }
            );
        }

        
    }
}
