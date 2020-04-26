using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Linq;

namespace McLaren.Web.Filters
{  
    /// <summary>
    /// Represents the Swagger/Swashbuckle operation filter used to document the implicit API version parameter.
    /// </summary>
    /// <remarks>This <see cref="IOperationFilter"/> is only required due to bugs in the <see cref="SwaggerGenerator"/>.
    /// Once they are fixed and published, this class can be removed.</remarks>
    public class SwaggerDefaultValues : IOperationFilter
    {
        /// <summary>
        /// Applies the filter to the specified operation using the given context.
        /// </summary>
        /// <param name="operation">The operation to apply the filter to.</param>
        /// <param name="context">The current operation filter context.</param>
        public void Apply( OpenApiOperation operation, OperationFilterContext context )
        {
            var apiDescription = context.ApiDescription;

            var apiVersion = apiDescription.GetApiVersion();
            var model = apiDescription.ActionDescriptor.GetApiVersionModel( ApiVersionMapping.Explicit | ApiVersionMapping.Implicit );

            operation.Deprecated = model.DeprecatedApiVersions.Contains( apiVersion );


            if ( operation.Parameters == null )
            {
                return;
            }

            foreach ( var parameter in operation.Parameters )
            {
                var description = apiDescription.ParameterDescriptions.First( p => p.Name == parameter.Name );

                if ( parameter.Description == null )
                {
                    parameter.Description = description.ModelMetadata?.Description;
                }

                if ( parameter.Schema.Default == null && description.DefaultValue != null )
                {
                    parameter.Schema.Default = new OpenApiString( description.DefaultValue.ToString() );
                }

                parameter.Required |= description.IsRequired;
            }
        }
    }
}