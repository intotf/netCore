using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EasenApi.Filters;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Serialization;
using Swashbuckle.AspNetCore.Swagger;

namespace EasenApi
{
    /// <summary>
    /// 程序入口
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="configuration"></param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        /// <summary>
        /// 程序配置
        /// </summary>
        public IConfiguration Configuration { get; }

        /// <summary>
        /// Api版本提者信息
        /// </summary>
        private IApiVersionDescriptionProvider provider;

        /// <summary>
        /// 配置服务 
        /// This method gets called by the runtime. Use this method to add services to the container.
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc(o =>
            {
                o.Filters.Add<ApiGlobalExceptionFilter>();
                o.Filters.Add<HttpGlobalResultFilter>();
            })
            .SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddMvc().AddJsonOptions(o =>
            {
                o.SerializerSettings.ContractResolver = new DefaultContractResolver();
                o.SerializerSettings.DateFormatString = "yyyy-MM-dd HH:mm:ss";
            });

            //services.AddSwaggerGen(c =>
            //{
            //    c.SwaggerDoc("v1", new Info
            //    {
            //        Version = "v1",
            //        Title = "易胜人脸识别接口",
            //        Description = ""
            //    });
            //    //需要nugut Microsoft.Extensions.PlatformAbstractions
            //    //var basePath = PlatformServices.Default.Application.ApplicationBasePath; // 获取到应用程序的根路径
            //    c.IncludeXmlComments(this.GetType().Assembly.Location.Replace(".dll", ".xml"), true);  //是需要设置 XML 注释文件的完整路径
            //    c.OperationFilter<SwaggerFileUploadFilter>();
            //});

            services.AddApiVersioning(option =>
            {
                option.AssumeDefaultVersionWhenUnspecified = true;
                option.ReportApiVersions = false;
            })
            .AddMvcCore().AddVersionedApiExplorer(option =>
            {
                option.GroupNameFormat = "'v'VVV";
                option.AssumeDefaultVersionWhenUnspecified = true;
            });

            this.provider = services.BuildServiceProvider().GetRequiredService<IApiVersionDescriptionProvider>();
            services.AddSwaggerGen(options =>
            {
                foreach (var description in provider.ApiVersionDescriptions)
                {
                    options.SwaggerDoc(description.GroupName,
                            new Info()
                            {
                                Title = $"易胜人脸识别接口 v{description.ApiVersion}",
                                Version = description.ApiVersion.ToString(),
                                Description = "切换版本请点右上角版本切换"
                            }
                    );
                }
                //需要nugut Microsoft.Extensions.PlatformAbstractions
                //var basePath = PlatformServices.Default.Application.ApplicationBasePath; // 获取到应用程序的根路径
                options.IncludeXmlComments(this.GetType().Assembly.Location.Replace(".dll", ".xml"), true);  //是需要设置 XML 注释文件的完整路径
                options.OperationFilter<SwaggerFileUploadFilter>();
            });
        }

        /// <summary>
        /// 配置HTTP请求管道
        /// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {

            loggerFactory.AddLog4Net();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            loggerFactory.AddDebugger().AddLog4Net();
            app.UseMvc();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                foreach (var description in provider.ApiVersionDescriptions)
                {
                    c.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json", description.GroupName.ToUpperInvariant());
                }
                //o.SwaggerEndpoint("/swagger/v1/swagger.json", "易胜人脸数据上报 V1");
            });
        }
    }
}
