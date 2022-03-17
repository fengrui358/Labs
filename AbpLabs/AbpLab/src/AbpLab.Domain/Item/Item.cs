using System;
using Volo.Abp.Domain.Entities.Auditing;

namespace AbpLab.Item
{
    public class Item : FullAuditedAggregateRoot<Guid>
    {
        public string Name { get; set; }
    }
}
