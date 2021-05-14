namespace CustomAutoMapperLab
{
    public class OrgPerson
    {
        /// <summary>
        /// 工号
        /// </summary>
        public string EmployeeNumber { get; set; }

        /// <summary>
        /// 固定电话
        /// </summary>
        public string FixedPhone { get; set; }

        /// <summary>
        /// 身份证号
        /// </summary>
        public string IdNumber { get; set; }

        /// <summary>
        /// 所属部门Id
        /// </summary>
        public long OrgDepartmentId { get; set; }

        /// <summary>
        /// 所属部门名字
        /// </summary>
        public string OrgDepartmentName { get; set; }

        /// <summary>
        /// 部门地址
        /// </summary>
        public string OrgDepartmentLocation { get; set; }

        /// <summary>
        /// 部门人员Email
        /// </summary>
        public string OrgPersonEmail { get; set; }

        /// <summary>
        /// 部门人员Id
        /// </summary>
        public long OrgPersonId { get; set; }

        /// <summary>
        /// 部门人员移动电话
        /// </summary>
        public string OrgPersonMobilePhone { get; set; }

        /// <summary>
        /// 部门人员姓名
        /// </summary>
        public string OrgPersonName { get; set; }

        /// <summary>
        /// 职位
        /// </summary>
        public string Position { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remarks { get; set; }

        /// <summary>
        /// 用户Id
        /// </summary>
        public long? UserId { get; set; }

        /// <summary>
        /// 对应的所属部门
        /// </summary>
        public Department Department { get; set; }
    }
}
