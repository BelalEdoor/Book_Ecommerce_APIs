using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace BOOKSTORE.Filters // غيّر BOOKSTORE حسب الـ namespace تبع مشروعك
{
    public class RemoveAuthFromLoginFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            var path = context.ApiDescription.RelativePath?.ToLower();

            if (path != null && (path.Contains("auth/login") || path.Contains("auth/register")))
            {
                // 🔥 شيل الـ security من login/register
                operation.Security.Clear();
            }
        }
    }
}
