using System;
using System.Threading.Tasks;
using ApartmentReservation.Application.Features.Apartments.Commands;
using ApartmentReservation.Application.Features.Hosts;
using ApartmentReservation.Application.Helpers;
using ApartmentReservation.Application.Interfaces;
using ApartmentReservation.Common.Constants;
using ApartmentReservation.Common.Interfaces;
using ApartmentReservation.Infrastructure;
using ApartmentReservation.Infrastructure.Replicators;
using ApartmentReservation.Persistence;
using ApartmentReservation.Persistence.Authentication;
using ApartmentReservation.Persistence.Read;
using ApartmentReservation.WebUI.Filters;
using AutoMapper;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SpaServices.ReactDevelopmentServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace ApartmentReservation.WebUI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add DbContext using SQL Server Provider
            string connectionString = this.Configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<IApartmentReservationDbContext, ApartmentReservationDbContext>(optionsAction: (options) =>
          options.UseSqlServer(connectionString, b => b.MigrationsAssembly("ApartmentReservation.Persistence")));

            // No SQL
            services.Configure<QueryDatabaseSettings>(
                Configuration.GetSection(nameof(QueryDatabaseSettings)));

            services.AddSingleton<IQueryDatabaseSettings>(sp =>
                sp.GetRequiredService<IOptions<QueryDatabaseSettings>>().Value);

            services.AddSingleton<IQueryDbContext, QueryDbContext>();

            // Services
            services.AddScoped<IAuthService, AuthService>();

            services.AddTransient<IHolidayService, HolidayService>();
            services.AddTransient<ICostCalculator, CostCalculator>();

            // Add AutoMapper
            services.AddAutoMapper(typeof(AutoMapperProfile).GetType().Assembly);

            // Add MediatR
            services.AddMediatR(typeof(GetHostQueryHandler));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestValidationBehavior<,>));

            // Setup custom exception filter & fluent validation
            services.AddMvc(options => options.Filters.Add(typeof(CustomExceptionFilterAttribute)))
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_1)
                .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<CreateApartmentCommandValidation>());

            // Setup Authentication and Authorization
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.LoginPath = "";
                    options.LogoutPath = "/api/Account/Logout";
                    options.Events.OnRedirectToLogin = context =>
                    {
                        context.Response.StatusCode = 401;
                        return Task.CompletedTask;
                    };
                });

            services.AddAuthorization(Policies.AddPolicies);

            // Add background (hosted) services

            services.Configure<DbReplicationSettings>(
                Configuration.GetSection(nameof(DbReplicationSettings)));

            services.AddHostedService<UserReplicatorService>();
            services.AddHostedService<ReservationReplicationService>();
            services.AddHostedService<ApartmentReplicationService>();
            services.AddHostedService<AmenityReplicatorService>();

            // In production, the React files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/build";
            });

            // Register the Swagger services
            services.AddSwaggerDocument(options => options.Title = "Apartment Reservation");
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseSpaStaticFiles();

            app.UseAuthentication();

            // Register the Swagger generator and the Swagger UI middlewares
            app.UseOpenApi();
            app.UseSwaggerUi3();
            app.UseReDoc(options =>
            {
                options.Path = "/redoc";
                options.DocumentPath = "/swagger/v1/swagger.json";
            });

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller}/{action=Index}/{id?}");
            });

            app.UseSpa(spa =>
            {
                spa.Options.SourcePath = "ClientApp";

                if (env.IsDevelopment())
                {
                    spa.Options.StartupTimeout = TimeSpan.FromSeconds(120);
                    spa.UseReactDevelopmentServer(npmScript: "start");
                }
            });
        }
    }
}