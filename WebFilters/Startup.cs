using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.Swagger;
using WebFilters.Filters;

namespace WebFilters
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddMvc(o =>
            {
                o.Filters.Add<GlobalAuthorizationFilter>();
                o.Filters.Add<GlobalResourceFilter>();
                o.Filters.Add<GlobalActonFilter>();
                o.Filters.Add<GlobalAsyncResourceFilter>();
                o.Filters.Add<GlobalExceptionFilter>();
                o.Filters.Add<GlobalResultFilter>();
            });


            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info
                {
                    Version = "v1",
                    Title = "Filters 过滤器测试Api",
                    Description = @"1、Authorization Filter
　　                                2、Resource Filter
　　                                3、Acton Filter
　　                                4、Exception Filter
　　                                5、Result Filter"
                });
                c.IncludeXmlComments(this.GetType().Assembly.Location.Replace(".dll", ".xml"), true);  //是需要设置 XML 注释文件的完整路径
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();

            app.UseSwagger();
            app.UseSwaggerUI(o =>
            {
                o.SwaggerEndpoint("/swagger/v1/swagger.json", "Filters 过滤器测试Api");
            });
        }
    }
}
