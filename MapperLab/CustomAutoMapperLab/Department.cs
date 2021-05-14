using System.Collections.ObjectModel;

namespace CustomAutoMapperLab
{
    public class Department
    {
        /// <summary>
        /// 负责人Id
        /// </summary>
        public long? ChargePersonId { get; set; }

        /// <summary>
        /// 纬度
        /// </summary>
        public long? Latitude { get; set; }

        /// <summary>
        /// 经度
        /// </summary>
        public long? Longitude { get; set; }

        /// <summary>
        /// 部门联系邮件
        /// </summary>
        public string OrgDepartmentEmail { get; set; }

        /// <summary>
        /// 部门Id
        /// </summary>
        public long OrgDepartmentId { get; set; }

        /// <summary>
        /// 所在地址
        /// </summary>
        public string OrgDepartmentLocation { get; set; }

        /// <summary>
        /// 部门名称
        /// </summary>
        public string OrgDepartmentName { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string OrgDepartmentNotes { get; set; }

        /// <summary>
        /// 电话号码
        /// </summary>
        public string OrgDepartmentPhone { get; set; }

        /// <summary>
        /// 父部门Id，如果值为-1是顶级节点
        /// </summary>
        public long ParentId { get; set; }

        /// <summary>
        /// 父级部门
        /// </summary>
        public Department Parent { get; set; }

        /// <summary>
        /// 子级部门
        /// </summary>
        public ObservableCollection<Department> Children { get; set; }

        /// <summary>
        /// 部门下的人员
        /// </summary>
        public ObservableCollection<OrgPerson> Persons { get; set; }
    }
}
