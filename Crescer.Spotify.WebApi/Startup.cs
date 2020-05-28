using Crescer.Spotify.Dominio.Contratos;
using Crescer.Spotify.Dominio.Servicos;
using Crescer.Spotify.Infra.Adapters;
using Crescer.Spotify.Infra.Repository;
using Crescer.Spotify.Infra.Utils;
using Crescer.Spotify.WebApi.Utils;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;

namespace Crescer.Spotify.WebApi
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
            services.AddMvc();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "Spotify API", Version = "v1" });
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
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Spotify API");
            });

            app.UseMvc();
        }
    }
}
