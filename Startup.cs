using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DotNetCoreWebApi.Filters;
using DotNetCoreWebApi.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;
using DotNetCoreWebApi.Data;
using DotNetCoreWebApi.ServiceInterfaces;
using DotNetCoreWebApi.Services;
using AutoMapper;
using DotNetCoreWebApi.Profiles;

namespace DotNetCoreWebApi
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
            //Get properties from appsettings.json
            services.Configure<HotelInfo>(Configuration.GetSection("Info"));

            //Add services for dependency injection
            services.AddScoped<IRoomService, DefaultRoomService>();

            //using in-memory database for dev and testing
            //TODO: Swapp to a real database
            services.AddDbContext<ApiDbContext>(options =>
            {
                options.UseInMemoryDatabase("apidb");
            });

            services.AddMvc(options =>
            {
                options.Filters.Add<JsonExceptionFilter>();
                options.Filters.Add<RequireHttpsOrCloseAttribute>();
                options.Filters.Add<LinkRewritingFilter>();
            })
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddOpenApiDocument(); // add OpenAPI v3 document
            //services.AddSwaggerDocument(); // add Swagger v2 document

            //Configure lowercase for controller names
            services.AddRouting(options => options.LowercaseUrls = true);

            services.AddApiVersioning(options =>
            {
                options.DefaultApiVersion = new ApiVersion(1, 0);
                options.ApiVersionReader = new MediaTypeApiVersionReader();
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.ReportApiVersions = true;
                options.ApiVersionSelector = new CurrentImplementationApiVersionSelector(options);
            });

            services.AddAutoMapper(typeof(Startup));

            //// Auto Mapper Configurations
            //var mappingConfig = new MapperConfiguration(mc =>
            //{
            //    mc.AddProfile(new MappingProfile());
            //});

            //IMapper mapper = mappingConfig.CreateMapper();
            //services.AddSingleton(mapper);

            //services.AddAutoMapper(
            //    options => options.AddProfile<MappingProfile>());

            // If CORS is needed, use options bellow
            //services.AddCors(options =>
            //{
            //    options.AddPolicy("AllowApp",
            //        policy => policy.AllowAnyOrigin());//policy.WithOrigins("https://example.com"));
            //});
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

                app.UseOpenApi();
                app.UseSwaggerUi3();
                app.UseReDoc();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            // If CORS is needed, use options bellow
            //app.UseCors("AllowApp");

            // Disabled redirection since we added RequireHttpsOrCloseAttribute filter to reject any HTTP request
            //app.UseHttpsRedirection();

            app.UseMvc();
        }
    }
}
