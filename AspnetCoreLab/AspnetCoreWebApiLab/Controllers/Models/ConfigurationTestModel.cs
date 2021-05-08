namespace AspnetCoreWebApiLab.Controllers.Models
{
    public class ConfigurationTestModel
    {
        public string ConfigurationTest = nameof(ConfigurationTest);

        public string MyKey { get; set; }

        public long MyNumber { get; set; }

        public SubConfigurationTestModel Position { get; set; }
    }

    public class SubConfigurationTestModel
    {
        public string Name { get; set; }
    }
}
