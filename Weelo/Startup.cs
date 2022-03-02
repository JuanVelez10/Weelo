using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using System.Net;
using System.Net.NetworkInformation;
using System.Text;
using WeeloAPI.Helpers;
using WeeloCore.Entities;
using WeeloCore.Helpers;
using static WeeloCore.Helpers.EnumType;

namespace Weelo
{
    public class Startup
    {
        private Tools tools = new Tools();

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Weelo", Version = "v1" });
            });

            //Mapper Configuration
            var mapperConfig = new MapperConfiguration(m =>
            {
                m.AddProfile(new MappingProfile());
            });

            IMapper mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);

            //Authentication Configuration
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {

                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = Configuration.GetSection("Jwt")["Issuer"],
                        ValidAudience = Configuration.GetSection("Jwt")["Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration.GetSection("Jwt")["Key"]))
                    };
                });

            //Corn Configurate 
            services.AddHealthChecks().AddCheck("ping", () => {
                try
                {
                    using (var ping = new Ping())
                    {
                        var reply = ping.Send("localhost");
                        if (reply.Status != IPStatus.Success)
                        {
                            return HealthCheckResult.Unhealthy();
                        }

                        if (reply.RoundtripTime >= 100)
                        {
                            return HealthCheckResult.Degraded();
                        }

                        return HealthCheckResult.Healthy();
                    }
                }
                catch
                {
                    return HealthCheckResult.Unhealthy();
                }
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Weelo v1"));
            }

            //Exception Control
            app.UseExceptionHandler(appError =>
            {
                appError.Run(async context =>
                {
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    context.Response.ContentType = "application/json";

                    var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                    if (contextFeature != null)
                    {

                        var metadata = new ErrorResponse
                        {
                            Code = context.Response.StatusCode,
                            Message = tools.GetMessage(1,MessageType.Error),
                            StackTrace = contextFeature.Error.StackTrace,
                            ExceptionMessage =  contextFeature.Error.Message,
                            ExceptionType = contextFeature.Error.GetType().FullName
                        };

                        await context.Response.WriteAsync(JsonConvert.SerializeObject(metadata));
                    }

                });
            });

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
