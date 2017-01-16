namespace MinMVC
{
public interface IActionInvoker
{
    void InvokeAction(ControllerContext controllerContext, string actionName);
}
}