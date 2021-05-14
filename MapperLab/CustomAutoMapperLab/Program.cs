using System.Collections.ObjectModel;
using AutoMapper;

namespace CustomAutoMapperLab
{
    class Program
    {
        static void Main(string[] args)
        {
            var department1 = new Department
            {
                OrgDepartmentId = 1,
                OrgDepartmentName = "department1"
            };

            var subDepartment1 = new Department
            {
                OrgDepartmentId = 3,
                OrgDepartmentName = "subDepartment1",
                Parent = department1
            };

            department1.Children = new ObservableCollection<Department>
            {
                subDepartment1
            };

            var department2 = new Department
            {
                OrgDepartmentId = 2,
                OrgDepartmentName = "department2"
            };

            var person1 = new OrgPerson
            {
                OrgPersonId = 1,
                OrgPersonName = "person1"
            };

            var person2 = new OrgPerson
            {
                OrgPersonId = 2,
                OrgPersonName = "person2"
            };

            var mapper = new Mapper(new MapperConfiguration(config =>
            {
                //目前可通过忽略Children来传递引用
                config.CreateMap<Department, Department>().ForMember(s => s.Children, b => b.Ignore())
                    .ForMember(s => s.Parent, b => b.MapFrom(s => s.Parent));
                config.CreateMap<OrgPerson, OrgPerson>();
            }));

            //测试，接受到新的部门变更
            var newDepartment = new Department
            {
                OrgDepartmentId = 1,
                OrgDepartmentName = "newDepartment1"
            };

            newDepartment.Children = department1.Children;
            newDepartment.Parent = department1.Parent;

            var code1 = department1.Children.GetHashCode();

            mapper.Map(newDepartment, department1); //map后children丢失？


            var code2 = department1.Children.GetHashCode();
        }
    }
}
