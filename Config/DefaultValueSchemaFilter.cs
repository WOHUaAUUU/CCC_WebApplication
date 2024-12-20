using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.ComponentModel;
using System.Reflection;

namespace Shoping_WebAPI.Config
{
    public class DefaultValueSchemaFilter : ISchemaFilter
    {
        public void Apply(OpenApiSchema schema, SchemaFilterContext context)
        {
            if (schema == null) { return; }
            var objectScheam = schema;
            foreach (var item in objectScheam.Properties)
            {
                //按照数据类指定默认值
                if(item.Value.Type == "string" && item.Value.Default== null)
                {
                    item.Value.Default = new OpenApiString("");
                }else if(item.Key == "pageIndex")
                {
                    item.Value.Example = new OpenApiInteger(1);
                }else if (item.Key == "pageSize")
                {
                    item.Value.Example = new OpenApiInteger(10);
                }
                //通过特性来实现
                DefaultValueAttribute defaultValueAttribute =
                    context.ParameterInfo?.GetCustomAttribute<DefaultValueAttribute>();
                if (defaultValueAttribute != null)
                {
                    item.Value.Example = (IOpenApiAny) defaultValueAttribute.Value;
                }
            }
        }
    }
}
