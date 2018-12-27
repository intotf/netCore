using Microsoft.AspNetCore.Http;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EasenApi.Filters
{
    public class CustomOperationFilter : IOperationFilter
    {
        public void Apply(Operation operation, OperationFilterContext context)
        {

            #region Swagger版本描述处理
            foreach (var parameter in operation.Parameters.OfType<NonBodyParameter>())
            {
                var description = context.ApiDescription.ParameterDescriptions.First(p => p.Name == parameter.Name);

                if (parameter.Description == null)
                {
                    parameter.Description = "填写版本号如:1.0";
                    parameter.Default = context.ApiDescription.GroupName.Replace("v", "");
                }
            }
            #endregion

            #region Swagger授权过期器处理
            if (operation.Security == null)
                operation.Security = new List<IDictionary<string, IEnumerable<string>>>();
            var oAuthRequirements = new Dictionary<string, IEnumerable<string>>
                                        {

                                              {"oauth2", new List<string> { "openid", "profile", "examinationservicesapi" }}
                                        };
            operation.Security.Add(oAuthRequirements);
            #endregion

        }
    }
}
