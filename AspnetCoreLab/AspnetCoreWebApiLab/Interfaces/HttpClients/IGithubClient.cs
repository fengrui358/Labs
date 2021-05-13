using System.Threading.Tasks;
using Refit;

namespace AspnetCoreWebApiLab.Interfaces.HttpClients
{
    public interface IGithubClient
    {
        [Get("/")]
        Task<string> GetMainPage();
    }
}
