using System.Threading.Tasks;

namespace AspNetCoreMini
{
    public interface IServer
    {
        Task StartAsync(RequestDelegate handler);
    }
}
