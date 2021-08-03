using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.NodeServices;
using Microsoft.Extensions.DependencyInjection;

#pragma warning disable 618

namespace NodeServicesLab
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var services = new ServiceCollection();
            services.AddNodeServices(options => {
                // Set any properties that you want on 'options' here
            });

            var serviceProvider = services.BuildServiceProvider();
            var nodeServices = serviceProvider.GetRequiredService<INodeServices>();

            var c = await nodeServices.InvokeAsync<string>("./js/edge");
            Console.WriteLine(c);

            var c2 = await nodeServices.InvokeExportAsync<string>("./js/edge", "", null);
            Console.WriteLine(c2);
        }
    }
}
