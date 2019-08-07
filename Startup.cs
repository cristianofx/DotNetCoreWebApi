using DotNetCoreWebApi.Filters;
using DotNetCoreWebApi.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using DotNetCoreWebApi.ServiceInterfaces;
using DotNetCoreWebApi.Services;
using AutoMapper;
using DotNetCoreWebApi.Models;
using Newtonsoft.Json;
using DotNetCoreWebApi.Framework.Response;

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
            services.Configure<HotelOptions>(Configuration);
            services.Configure<PagingOptions>(Configuration.GetSection("DefaultPagingOptions"));

            //Add services for dependency injection
            services.AddScoped<IRoomService, DefaultRoomService>();
            services.AddScoped<IBookingService, DefaultBookingService>();
            services.AddScoped<IDateLogicService, DefaultDateLogicService>();
            services.AddScoped<IOpeningService, DefaultOpeningService>();

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
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
                .AddJsonOptions(options =>
                {
                    // These should be the defaults, but we can be explicit:
                    options.SerializerSettings.DateTimeZoneHandling = DateTimeZoneHandling.Utc;
                    options.SerializerSettings.DateFormatHandling = DateFormatHandling.IsoDateFormat;
                    options.SerializerSettings.DateParseHandling = DateParseHandling.DateTimeOffset;

                }); ;

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

            services.Configure<ApiBehaviorOptions>(options => {
                                                        options.InvalidModelStateResponseFactory = context =>
                                                        {
                                                            var errorResponse = new ApiError(context.ModelState);
                                                            return new BadRequestObjectResult(errorResponse);
                                                        };
                                                    });

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
                //app.UseSwaggerUi();
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
