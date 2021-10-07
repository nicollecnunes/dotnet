
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RestWithASPNET.Model.Context;
using RestWithASPNET.Business;
using RestWithASPNET.Business.Implementations;
using Serilog;
using System;
using System.Collections.Generic;
using RestWithASPNET.Repository;
using RestWithASPNET.Repository.Generic;
using Microsoft.Net.Http.Headers;
using RestWithASPNET.Hypermedia.Filters;
using RestWithASPNET.Hypermedia.Enricher;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Rewrite;
using RestWithASPNET.Services;
using RestWithASPNET.Configurations;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Authorization;

namespace RestWithASPNET
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public IWebHostEnvironment Environment {get ;}
        public Startup(IConfiguration configuration, IWebHostEnvironment environment)
        {
            Configuration = configuration;
            Environment = environment;

            Log.Logger = new LoggerConfiguration()
                .WriteTo.Console()
                .CreateLogger();

        }

        

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var TokenConfigurations =  new TokenConfiguration();

            new ConfigureFromConfigurationOptions<TokenConfiguration>(
                Configuration.GetSection("TokenConfigurations") //vem la do app setings
            ).Configure(TokenConfigurations);

            services.AddSingleton(TokenConfigurations); //so uma config

            services.AddAuthentication(options =>{
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options => {
                options.TokenValidationParameters = new TokenValidationParameters{
                    ValidateAudience = true,
                    ValidateIssuer = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(TokenConfigurations.secret)),
                    ValidateIssuerSigningKey = true,
                    ValidateLifetime = true,
                    ValidIssuer = TokenConfigurations.issuer,
                    ValidAudience = TokenConfigurations.audience,

                };
            });

            services.AddAuthorization(auth => {
                auth.AddPolicy("Bearer", new AuthorizationPolicyBuilder()
                .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
                .RequireAuthenticatedUser().Build());
            });

            services.AddCors(options => {
                options.AddDefaultPolicy(builder =>{
                    builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader();

                });
            });
            services.AddControllers();

            var connection = Configuration["MySQLConnection:MySQLConnectionString"]; //pega a string de conexao
            services.AddDbContext<MySQLContext>(options => options.UseMySql(connection, ServerVersion.AutoDetect(connection)));

            if (Environment.IsDevelopment()) {
                MigrateDatabase(connection);

            }

            services.AddMvc(options => {
                options.RespectBrowserAcceptHeader = true; //aceita a propriedade do cabeçalho
                options.FormatterMappings.SetMediaTypeMappingForFormat("xml", MediaTypeHeaderValue.Parse("application/xml"));
                options.FormatterMappings.SetMediaTypeMappingForFormat("json", MediaTypeHeaderValue.Parse("application/json"));
            });//.AddXmlSerializerFormatters();

            var filteropt = new hypermediafilteroptions();
            //filteropt.contentresponseenricherlist.Add(new personEnricher());

            //filteropt.contentresponseenricherlist.Add(new booknEnricher());

            services.AddSingleton(filteropt);

            //versioning api
            services.AddApiVersioning();

            //sweggar
            services.AddSwaggerGen(c => {
                c.SwaggerDoc("v1",
                    new OpenApiInfo{
                        Title = "dotnet nicolle",
                        Version = "v1",
                        Description = "c# fictício para estudo backend",
                        Contact = new OpenApiContact{
                            Name = "nicolle nunes",
                            Url = new Uri("https://github.com/nicollecnunes")
                        }
                    });
            });

            //dependency injection
            services.AddScoped<ipersonbusiness, personBusinessImplementation>();
            services.AddScoped<ibookbusiness, bookBusinessImplementation>();
            services.AddScoped<iloginbusiness, loginbusinessImplementation>();
            services.AddScoped<isheetbusiness, sheetBusinessImplementation>();

            services.AddTransient<ITokenService, TokenService>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IPersonRepository, PersonRepository>();


            services.AddScoped(typeof(irepository<>), typeof(GenericRepository<>));

    
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting(); 
            //cors deve estar depois de usehttpsredirevction, depois de useroting
            //e antes de use endpoints
            app.UseCors();

            app.UseSwagger(); //gera o JSON com a documentacao

            app.UseSwaggerUI(c =>{
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "nico v1");
            }); //gera a pagina HTML

            var option = new RewriteOptions();
            option.AddRedirect("^$", "swagger");
            app.UseRewriter(option);

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapControllerRoute("DefaultApi", "{controller=values}/{id?}");
            });
        }

        private void MigrateDatabase(string connection) {
            try {
                var evolveConnection = new MySql.Data.MySqlClient.MySqlConnection(connection);
                var evolve = new Evolve.Evolve(evolveConnection, msg => Log.Information(msg)){
                    Locations = new List<string> {"db/migrations", "db/dataset"}, //diretorios
                    IsEraseDisabled = true,
                };
                evolve.Migrate();
            } catch (Exception ex) {
                Log.Error("Database migration failed", ex);

                throw;
            }
        }
    }
}
