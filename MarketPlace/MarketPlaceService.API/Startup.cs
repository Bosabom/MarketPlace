using MarketPlaceService.API.Options;
using MarketPlaceService.API.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RedSail.SwaggerWrapper;
using RedSail.PostgreSQLWrapper.Extensions;
using RedSail.KafkaWrapper;

namespace MarketPlaceService.API
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
            var postgreConfig = Configuration.GetSection(nameof(PostgreOptions)).Get<PostgreOptions>();
            var kafkaConfig = Configuration.GetSection(nameof(KafkaOptions)).Get<KafkaOptions>();

            services.Configure<PostgreOptions>(Configuration.GetSection(nameof(PostgreOptions)));
            services.AddTransient<IMarketPlaceRepository, MarketPlaceRepository>();

            services.AddSwaggerWrapper("MarketPlaceService");
            services.AddPostgreSQLWrapper(postgre =>
            {
                postgre.ConnectionString = postgreConfig.ConnectionString;
            });

            services.AddKafkaWrapper(producer =>
            {
                producer.BootstrapServers = kafkaConfig.BootstrapServers;
            },
                consumer =>
            {
                    consumer.BootstrapServers = kafkaConfig.BootstrapServers;
            });

            services.AddAutoMapper(typeof(Startup));
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

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
