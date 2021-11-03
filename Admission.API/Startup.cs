using Admission.Bussiness.IService;
using Admission.Bussiness.Service;
using Admission.Data.IRepository;
using Admission.Data.Models.Context;
using Admission.Data.Repository;
using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Admission.API
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
            services.AddCors();

            services.AddControllers().AddNewtonsoftJson(options =>
            options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Admission.API", Version = "v1" });

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = @"JWT Authorization header using the Bearer scheme. 
                        Enter 'Bearer' [space] and then your token in the text input below. 
                        Example: 'Bearer author_token'",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            },
                            Scheme = "oauth2",
                            Name = "Bearer",
                            In = ParameterLocation.Header,
                        },
                        new List<string>()
                    }
                });
            });

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = Configuration["Jwt:Issuer"],
                        ValidAudience = Configuration["Jwt:Issuer"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:Key"]))
                    };
                });
            services.AddMvc();

            string value = Environment.GetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS");

            FirebaseApp.Create(new AppOptions()
            {
                Credential = GoogleCredential.FromFile("admission-ec512-firebase-adminsdk-rovdq-353429ac0d.json"),
            });

            services.AddDbContext<AdmissionsDBContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("DBConnection")));

            services.AddScoped<ILoginService, LoginService>();

            services.AddScoped<IRoleService, RoleService>();
            services.AddScoped<IUserManagementService, UserManagementService>();
            services.AddScoped<ICounselorService, CounselorService>();
            services.AddScoped<IStudentService, StudentService>();

            services.AddScoped<IDistrictService, DistrictService>();
            services.AddScoped<IMajorService, MajorService>();
            services.AddScoped<IAdmisstionService, AdmissionService>();
            services.AddScoped<IUniversityManagementService, UniversityManagementService>();
            services.AddScoped<IUniversityService, UniversityService>();

            services.AddScoped<IBannerService, BannerService>();

            services.AddScoped<ITalkshowService, TalkshowService>();
            services.AddScoped<ITalkshowManagementService, TalkshowManagementService>();

            services.AddScoped<ISlotService, SlotService>();



            services.AddScoped<IRoleRepository, RoleRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IStudentRepository, StudentRepository>();
            services.AddScoped<ICounselorRepository, CounselorRepository>();

            services.AddScoped<IWalletRepository, WalletRepository>();

            services.AddScoped<IDistrictRepository, DistrictRepository>();
            services.AddScoped<IMajorRepository, MajorRepository>();
            services.AddScoped<IAdmissionRepository, AdmissionRepository>();
            services.AddScoped<IUniversityRepository, UniversityRepository>();

            services.AddScoped<ITalkshowRepository, TalkshowRepository>();
            
            services.AddScoped<ISlotRepository, SlotRepository>();
            services.AddScoped<ITransactionRepository, TransactionRepository>();


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment() || env.IsProduction())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Admission.API v1"));
            }

            app.UseCors(x => x
                .AllowAnyMethod()
                .AllowAnyHeader()
                .SetIsOriginAllowed(origin => true) // allow any origin
                .AllowCredentials());

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
