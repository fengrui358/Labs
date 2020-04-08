using System.Threading.Tasks;

namespace AspNetCoreMini
{
    public delegate Task RequestDelegate(HttpContext context);
}
