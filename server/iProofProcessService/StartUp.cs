namespace CPS.Proof.DFSExtension
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Reflection;    
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using Microsoft.OpenApi.Models;
    using Swashbuckle.AspNetCore.SwaggerUI;
    using System.Configuration;
    using log4net;
    using SRA.Proof.Middleware;    
    using Newtonsoft.Json.Serialization;

    public class Startup
    {

        public const string LogConfigFile = @"Configuration/LogSettings.Config";

        private readonly ILog log = null;

        public const string WindsorConfigFile =
                         @"Configuration/ComponentSettings.Config";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;

            log = LogManager.GetLogger(this.GetType());
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "AssetVerification",
                    Description = "Test",

                    Contact = new OpenApiContact
                    {
                        Name = "AssetVerification",
                    },
                    License = new OpenApiLicense
                    {
                        Name = "Proprietary",
                    }
                });

                c.DocInclusionPredicate((_, api) => true);
                c.TagActionsBy(o => new[] { o.GroupName });

                // Set the comments path for the Swagger JSON and UI.
                var xmlFilePrifix = Assembly.GetExecutingAssembly().GetName().Name.Split('.')[0];
                var xmlFiles = Directory.GetFiles(AppContext.BaseDirectory, "*.xml", SearchOption.TopDirectoryOnly).ToList();

                xmlFiles = xmlFiles.Where(w => Path.GetFileName(w.ToString()).Contains(xmlFilePrifix)).ToList();

                foreach (var filePath in xmlFiles)
                {
                    c.IncludeXmlComments(filePath);
                }
            });
            services.AddControllers()      
                 .AddNewtonsoftJson(options =>
                 {
                     options.SerializerSettings.ContractResolver = new DefaultContractResolver();
                 });

            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI();
                app.UseSwaggerUI(c =>
                {
                    c.DocumentTitle = "AssetVerification";
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "AssetVerification");
                    c.DisplayOperationId();
                    c.DisplayRequestDuration();
                    c.DocExpansion(DocExpansion.List);
                });
            }   

              app.UseCors(builder =>builder
                 //.WithOrigins(Configuration.GetSection("Allow-Origin").Value)
                 .AllowAnyOrigin()
                 .AllowAnyHeader()
                     .AllowAnyMethod()               
                    );

            string path = Directory.GetCurrentDirectory();

             if (!ObjectManager.IsInitalized)
                ObjectManager.Initialize(Path.Combine(path, WindsorConfigFile));

            AppParams.BuildRootSection(path);
            
            log4net.Config.XmlConfigurator.ConfigureAndWatch
                    (new System.IO.FileInfo(string.Concat(System.AppDomain.CurrentDomain.
                        SetupInformation.ApplicationBase, LogConfigFile)));               

            
            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        
    }
}