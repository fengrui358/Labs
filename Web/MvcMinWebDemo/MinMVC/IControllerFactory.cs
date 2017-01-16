namespace MinMVC
{
public interface IControllerFactory
{
    IController CreateController(RequestContext requestContext, string controllerName);
}
}
