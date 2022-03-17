using System;
using AbpLab.Item;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace AbpLab.ItemApi
{
    [Authorize]
    public class ItemService : CrudAppService<Item.Item, ItemDto, Guid, ItemDto, CreateUpdateItemDto, CreateUpdateItemDto>
    {
        public ItemService(IRepository<Item.Item, Guid> repository) : base(repository)
        {
        }
    }
}
