using Volo.Abp.Authorization.Permissions;

namespace AbpLab.Item
{
    public class ItemPermissionDefinitionProvider : PermissionDefinitionProvider
    {
        public override void Define(IPermissionDefinitionContext context)
        {
            var itemGroup = context.AddGroup(ItemPermissions.GroupName);

            var itemPermission = itemGroup.AddPermission(ItemPermissions.Items.Default);
            itemPermission.AddChild(ItemPermissions.Items.Create);
        }
    }
}
