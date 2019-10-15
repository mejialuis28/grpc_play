using GrpcSongService;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace GrpcSongConsoleClient
{
    class Program
    {
        static async Task Main(string[] args)
        {
            IServiceCollection services = new ServiceCollection();
            services.AddTransient<SongClient>();

            services.AddGrpcClient<SongFinder.SongFinderClient>(o =>
            {
                o.Address = new Uri("https://localhost:44347");
            });
            
            var serviceProvider = services.BuildServiceProvider();

            await serviceProvider.GetService<SongClient>().GetSongs();

        }
    }
}
