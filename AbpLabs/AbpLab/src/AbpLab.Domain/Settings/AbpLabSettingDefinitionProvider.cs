using Volo.Abp.Settings;

namespace AbpLab.Settings;

public class AbpLabSettingDefinitionProvider : SettingDefinitionProvider
{
    public override void Define(ISettingDefinitionContext context)
    {
        //Define your own settings here. Example:
        //context.Add(new SettingDefinition(AbpLabSettings.MySetting1));
    }
}
