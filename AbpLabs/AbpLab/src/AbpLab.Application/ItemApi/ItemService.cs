using System;
using AbpLab.Item;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace AbpLab.ItemApi
{
    public class ItemService : CrudAppService<Item.Item, ItemDto, Guid, ItemDto, CreateUpdateItemDto, CreateUpdateItemDto>
    {
        public ItemService(IRepository<Item.Item, Guid> repository) : base(repository)
        {
        }
    }
}
