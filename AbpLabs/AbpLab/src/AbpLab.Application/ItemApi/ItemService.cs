using System;
using System.Threading.Tasks;
using AbpLab.BaseServices;
using AbpLab.Item;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Repositories;

namespace AbpLab.ItemApi
{
    [Authorize]
    public class ItemService : CutCrudAppService<Item.Item, ItemDto, ItemDto, Guid, PagedAndSortedResultRequestDto, CreateUpdateItemDto, CreateUpdateItemDto>
    {
        protected override string CreatePolicyName { get; set; } = ItemPermissions.Items.Create;

        public ItemService(IRepository<Item.Item, Guid> repository) : base(repository)
        {
            
        }

        [RemoteService]
        public override Task<ItemDto> CreateAsync(CreateUpdateItemDto input)
        {
            return base.CreateAsync(input);
        }

        [RemoteService]
        public override Task<PagedResultDto<ItemDto>> GetListAsync(PagedAndSortedResultRequestDto input)
        {
            return base.GetListAsync(input);
        }
    }
}
