using System.Collections.Generic;
using System.IO;
using DotNetify;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.SpaServices.Webpack;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace HelloWorld
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSignalR();
            services.AddDotNetify();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseWebSockets();
            app.UseDotNetify();

            if (env.IsDevelopment())
            {
            #pragma warning disable 618
                app.UseWebpackDevMiddleware(new WebpackDevMiddlewareOptions
                {
                    HotModuleReplacement = true,
                    HotModuleReplacementClientOptions = new Dictionary<string, string> { { "reload", "true" } },
                });
            #pragma warning restore 618
            }

            app.UseStaticFiles();
            app.UseRouting();
            app.UseEndpoints(endpoints => endpoints.MapHub<DotNetifyHub>("/dotnetify"));

            app.Run(async (context) =>
            {
                //using var reader = new StreamReader(File.OpenRead("wwwroot/index.html"));
                using var reader = new StreamReader(File.OpenRead("wwwroot/StocksExample.html"));
                await context.Response.WriteAsync(reader.ReadToEnd());
            });
        }
    }
}
