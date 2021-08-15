using ConsoleApp;
using Microsoft.Extensions.DependencyInjection;
using Services;
using Services.Interfaces;

namespace PokerFace
{
    class Program
    {
        static void Main(string[] args)
        {
            var serviceProvider = new ServiceCollection()
                .AddSingleton<IPokerService, PokerService>()
                .AddSingleton<PokerApp>()
                .BuildServiceProvider();

            serviceProvider.GetService<PokerApp>().Run();
        }
    }
}
