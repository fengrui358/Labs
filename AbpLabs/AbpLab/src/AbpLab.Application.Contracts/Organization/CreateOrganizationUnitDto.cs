using System;

namespace AbpLab.Organization
{
    public class CreateOrganizationUnitDto : UpdateOrganizationUnitDto
    {
        public virtual Guid? ParentId { get; set; }
    }
}
