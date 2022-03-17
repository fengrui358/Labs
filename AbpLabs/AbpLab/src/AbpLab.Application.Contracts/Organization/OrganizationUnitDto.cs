using System;
using Volo.Abp.Application.Dtos;

namespace AbpLab.Organization
{
    public class OrganizationUnitDto : EntityDto<Guid>
    {
        public virtual Guid? ParentId { get; set; }

        public virtual string DisplayName { get; set; }
    }
}
