using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mechanic.API
{
    public class SwaggerHeaderFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {

            //if (operation.Parameters == null)
            //    operation.Parameters = new List<OpenApiParameter>();

            //operation.Parameters.Add(new OpenApiParameter
            //{
            //    Name = "Culture",
            //    In = ParameterLocation.Header,
            //    Description = "Supported cultures are en-us, es-pr and fr-ca",
            //    Required = false,
            //    Schema = new OpenApiSchema
            //    {
            //        Format = "String"
            //    }

            //});

        }
    }
}
