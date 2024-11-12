using Microsoft.AspNetCore.Authorization;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Linq;

namespace CelularesAPI.Utils
{
    public class AuthFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            var attributes = context.ApiDescription.CustomAttributes();
            bool isAuthRequired = attributes.OfType<AuthorizeAttribute>().Any();
            bool isAllowAnonymous = attributes.OfType<AllowAnonymousAttribute>().Any();

            if (isAuthRequired && !isAllowAnonymous)
            {
                operation.Security = new List<OpenApiSecurityRequirement>
                {
                    new OpenApiSecurityRequirement
                    {
                        [new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Token" }
                        }] = new string[] { }
                    }
                };
            }
        }
    }
}