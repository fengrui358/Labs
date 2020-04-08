using System.Threading.Tasks;

namespace AspNetCoreMini
{
    public interface IWebHost
    {
        Task StartAsync();
    }
}