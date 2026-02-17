using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.FileProviders;

namespace GovUKFrontend.Components.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static void AddGovUkAssets(this IApplicationBuilder app)
        {
            var componentAssembly = typeof(ApplicationBuilderExtensions).Assembly;
            app.UseStaticFiles(new StaticFileOptions()
            {
                FileProvider = new EmbeddedFileProvider(componentAssembly, "GovUKFrontend.Components.wwwroot"),
                RequestPath = new PathString("/gdscontents")
            });
        }
    }
}
