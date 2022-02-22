using DataBasePerformanceLab.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace DataBasePerformanceLab.Permissions;

public class DataBasePerformanceLabPermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var myGroup = context.AddGroup(DataBasePerformanceLabPermissions.GroupName);
        //Define your own permissions here. Example:
        //myGroup.AddPermission(DataBasePerformanceLabPermissions.MyPermission1, L("Permission:MyPermission1"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<DataBasePerformanceLabResource>(name);
    }
}
