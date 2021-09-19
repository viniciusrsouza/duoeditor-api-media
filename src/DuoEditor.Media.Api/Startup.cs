using DuoEditor.Media.App.Config;
using DuoEditor.Media.App.Interfaces;
using DuoEditor.Media.Infra.Config;
using DuoEditor.Media.Infra.Persistence;
using DuoEditor.Media.Infra.Repositories;
using DuoEditor.Media.Service.Clients;
using DuoEditor.Media.Service.Config;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ServiceStack.Redis;

namespace DuoEditor.Media.Api
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
      // Default
      services.AddDbContext<ApiDbContext>(options =>
        {
          options.UseNpgsql(Configuration.GetConnectionString("DefaultConnection"));
        });
      services.AddSwaggerGen(c =>
      {
        c.SwaggerDoc("v1", new() { Title = "DuoEditor.Media.Api", Version = "v1" });
      });
      services.AddControllers();

      // Environment Variables
      DotNetEnv.Env.Load("../../.env");

      // Configurations
      services.Configure<MediaConfig>(Configuration.GetSection("Media"));

      // Repositories
      services.AddScoped<IUserImageRepository, UserImageRepository>();
      services.AddScoped<IMediaRepository, MediaRepository>();

      // Auth
      services.AddSingleton<IRedisClientsManagerAsync>(c => new RedisManagerPool(Configuration.GetConnectionString("RedisConnection")));
      services.Configure<AuthClientConfig>(Configuration.GetSection("Services:Auth"));
      services.AddAuth<CacheRepository>();
      services.AddHttpClient<AuthClient>();

      // Project Dependencies
      services.AddApplication();

      // MediatR
      var assembly = AppDomain.CurrentDomain.Load("DuoEditor.Media.App");
      services.AddMediatR(assembly);
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
        app.UseSwagger();
        app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "DuoEditor.Media.Api v1"));
      }

      // app.UseHttpsRedirection();
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