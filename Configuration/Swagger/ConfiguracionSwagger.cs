using Asp.Versioning.ApiExplorer;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace SavingBack.Configuration.Swagger
{
    public class ConfiguracionSwagger : IConfigureOptions<SwaggerGenOptions>
    {


        //INTERFACE QUE PERMITE TRABAJAR CON LOS PROVEEDORES DE VERSIONES
        private readonly IApiVersionDescriptionProvider provider;

        public ConfiguracionSwagger(IApiVersionDescriptionProvider provider)
        {
            this.provider = provider;
        }

        //METODO IMPLEMENTADO DE LA INTERFACE 
        public void Configure(SwaggerGenOptions options)
        {
            //SE ITERA SOBRE LAS DESCRIPCIONES DE LOS PROVEEDORES
            foreach (var descripcion in provider.ApiVersionDescriptions)
            {
                //SE CREA UN ONJETO PARA LA DESCRIPCION DE LA API
                var informacion = new OpenApiInfo
                {
                    Title = $"API REST full SavingBack",
                    Version = descripcion.ApiVersion.ToString(),
                    Description = "API REST full para la gestión de metas de ahorro, ingresos y egresos, esto con el fin de mejorar las finanzas."
                };


                //SI LA VERSION ESTA DEPRECIADA, SE LE AÑADE A LA DESCRIPCION EL SIGUIENTE MENSAJE
                if (descripcion.IsDeprecated)
                {
                    informacion.Description += "(Esta versión de api esta depreciada.)";
                }

                //SE DEFINE LA INFORMACION DE LA API DE ACUERDO A LA VERSION
                options.SwaggerDoc(descripcion.GroupName, informacion);
            }

            options.DocInclusionPredicate((docName, apiDesc) => apiDesc.GroupName == docName);
        }
    }
}
