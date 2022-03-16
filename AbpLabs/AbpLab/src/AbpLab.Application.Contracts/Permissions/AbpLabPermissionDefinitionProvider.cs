using AbpLab.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace AbpLab.Permissions;

public class AbpLabPermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var myGroup = context.AddGroup(AbpLabPermissions.GroupName);
        //Define your own permissions here. Example:
        //myGroup.AddPermission(AbpLabPermissions.MyPermission1, L("Permission:MyPermission1"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<AbpLabResource>(name);
    }
}
