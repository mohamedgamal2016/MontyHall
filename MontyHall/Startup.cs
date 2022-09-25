using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using MontyHall.Core.Commands;
using MontyHall.Core.Common.Behaviors;
using MontyHall.Core.Validators;
using MontyHall.Extensions;
using MontyHall.Factories;
using MontyHall.Mapping;
using MontyHall.Middleware;
namespace MontyHall
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors();
            services.AddMvc().SetCompatibilityVersion(Microsoft.AspNetCore.Mvc.CompatibilityVersion.Version_2_1);

            services.AddSwaggerGen(c => {
                c.SwaggerDoc("v1", new Swashbuckle.AspNetCore.Swagger.Info
                {
                    Version = "v1",
                    Title = "MontyHall API Documentation",
                    Description = "This documentation provides the information about MontyHall API endpoints",
                    TermsOfService = "None"
                });
            });

            services.AddValidatorsFromAssemblyContaining<PlayGameCommandValidator>();

            services.AddMediatR(typeof(Startup));
            services.AddMediatR(typeof(PlayGameCommand).Assembly);

            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidatorBehavior<,>));

            services.AddHttpClient();

            services.AddSingleton<IResponseFactory, ResponseFactory>();

            services.AddMontyHallCollection();
            services.AddAutoMapper(typeof(Startup));
            services.AddAutoMapper(typeof(MontyHallMappingProfile));
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMiddleware<UnhandledExceptionCatchingMiddleware>();

            app.UseCors(options => {
                options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
            });
            app.UseHttpsRedirection();
            app.UseMvc();

            app.UseSwagger();
            app.UseSwaggerUI(c => 
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json","MontyHall API");
                c.RoutePrefix = "swagger";
            });

            app.UseMiddleware<RequestResponseLoggingMiddleware>();
        }
    }
}
