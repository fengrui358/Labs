using System;

namespace AbpLab.Organization
{
    public class CreateOrganizationUnitDto
    {
        public virtual Guid? ParentId { get; set; }

        /// <summary>
        /// Display name of this OrganizationUnit.
        /// </summary>
        public virtual string DisplayName { get; set; }
    }
}
