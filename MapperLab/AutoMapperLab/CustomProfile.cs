using AutoMapper;

namespace AutoMapperLab
{
    class CustomProfile : Profile
    {
        public CustomProfile()
        {
            ShouldMapField = info => true;
            ShouldMapProperty = info => info.GetMethod.IsPublic || info.GetMethod.IsAssembly;
            CreateMap<ProfileClassA, ProfileClassB>();
        }
    }

    class ProfileClassA
    {
        private string _filed;

        public string Test { get; set; }

        private string PrivateProperty { get; }

        public ProfileClassA(string filed, string privateProperty)
        {
            _filed = filed;
            PrivateProperty = privateProperty;
        }
    }

    class ProfileClassB
    {
        private string _filed;

        public string Test { get; set; }

        private string PrivateProperty { get; set; }
    }
}
