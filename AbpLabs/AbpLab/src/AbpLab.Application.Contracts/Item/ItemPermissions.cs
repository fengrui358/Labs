using Volo.Abp.Reflection;

namespace AbpLab.Item
{
    public static class ItemPermissions
    {
        public const string GroupName = "AppItem";

        public static class Items
        {
            public const string Default = GroupName + ".Items";
            public const string Create = Default + ".Create";
            public const string Update = Default + ".Update";
            public const string Delete = Default + ".Delete";
            public const string ManagePermissions = Default + ".ManagePermissions";
        }

        public static string[] GetAll()
        {
            return ReflectionHelper.GetPublicConstantsRecursively(typeof(ItemPermissions));
        }
    }
}
