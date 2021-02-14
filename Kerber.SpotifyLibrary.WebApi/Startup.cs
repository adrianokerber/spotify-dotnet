using Kerber.SpotifyLibrary.Domain.Contratos;
using Kerber.SpotifyLibrary.Domain.Servicos;
using Kerber.SpotifyLibrary.Infra.Adapters;
using Kerber.SpotifyLibrary.Infra.Configs;
using Kerber.SpotifyLibrary.Infra.Repository;
using Kerber.SpotifyLibrary.WebApi.Configs;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace Kerber.SpotifyLibrary.WebApi
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
            services.AddControllers();

            // TODO: re-enable filters for endpoint failure, but do not exposing system behaviour or internal dependencies
            //services.AddMvc(options =>
            //{
            //    options.Filters.Add(typeof(ValidateModelAttribute));
            //});

            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = Configuration["App:Title"],
                    Version = Configuration["App:Version"],
                    Description = Configuration["App:Description"]
                });

                options.CustomSchemaIds(x => x.FullName);
            });
            var mongoSettings = MongoConfigurationLoader.Load(Configuration, databaseConfigKey: "SpotifyMongoDB");
            services.AddSingleton<MongoSettings>(mongoSettings);
            services.AddScoped<MongoAdapter, MongoAdapter>();
            services.AddScoped<MusicaService, MusicaService>();
            services.AddScoped<AlbumService, AlbumService>();
            services.AddScoped<IMusicaRepository, MusicaRepository>();
            services.AddScoped<IAlbumRepository, AlbumRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Spotify API"));

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => {
                endpoints.MapControllers();
            });
        }
    }
}
