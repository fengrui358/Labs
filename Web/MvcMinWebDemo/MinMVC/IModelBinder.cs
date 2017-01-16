using System;

namespace MinMVC
{
public interface IModelBinder
{
    object BindModel(ControllerContext controllerContext, string modelName, Type modelType);
}
}
