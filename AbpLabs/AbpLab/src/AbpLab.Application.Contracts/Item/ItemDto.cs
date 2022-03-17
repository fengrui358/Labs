using System;
using Volo.Abp.Application.Dtos;

namespace AbpLab.Item
{
    public class ItemDto : EntityDto<Guid>
    {
        public string Name { get; set; }
    }
}
