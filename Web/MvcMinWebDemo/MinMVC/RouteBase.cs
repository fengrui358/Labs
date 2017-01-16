using System.Web;

namespace MinMVC
{
    public abstract class RouteBase
    {
        public abstract RouteData GetRouteData(HttpContextBase httpContext);
    }
}
