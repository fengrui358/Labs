using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Repositories;

namespace AbpLab.BaseServices
{
    public abstract class CutCrudAppService<TEntity, TEntityDto, TKey, TGetListInput, TCreateInput> : CrudAppService<TEntity, TEntityDto, TKey, TGetListInput,
                TCreateInput, TCreateInput> where TEntity : class, IEntity<TKey> where TEntityDto : IEntityDto<TKey>
    {
        protected CutCrudAppService(IRepository<TEntity, TKey> repository) : base(repository)
        {
        }

        [RemoteService(false)]
        public override Task<TEntityDto> CreateAsync(TCreateInput input)
        {
            return base.CreateAsync(input);
        }

        [RemoteService(false)]
        public override Task<PagedResultDto<TEntityDto>> GetListAsync(TGetListInput input)
        {
            return base.GetListAsync(input);
        }

        [RemoteService(false)]
        public override Task<TEntityDto> UpdateAsync(TKey id, TCreateInput input)
        {
            return base.UpdateAsync(id, input);
        }

        [RemoteService(false)]
        public override Task DeleteAsync(TKey id)
        {
            return base.DeleteAsync(id);
        }

        [RemoteService(false)]
        public override Task<TEntityDto> GetAsync(TKey id)
        {
            return base.GetAsync(id);
        }
    }

    public abstract class CutCrudAppService<TEntity, TGetOutputDto, TGetListOutputDto, TKey, TGetListInput, TCreateInput, TUpdateInput> : CrudAppService<TEntity,
        TGetOutputDto, TGetListOutputDto, TKey, TGetListInput, TCreateInput, TUpdateInput>
        where TEntity : class, IEntity<TKey> where TGetOutputDto : IEntityDto<TKey> where TGetListOutputDto : IEntityDto<TKey>
    {
        protected CutCrudAppService(IRepository<TEntity, TKey> repository) : base(repository)
        {
        }

        [RemoteService(false)]
        public override Task<PagedResultDto<TGetListOutputDto>> GetListAsync(TGetListInput input)
        {
            return base.GetListAsync(input);
        }

        [RemoteService(false)]
        public override Task<TGetOutputDto> UpdateAsync(TKey id, TUpdateInput input)
        {
            return base.UpdateAsync(id, input);
        }

        [RemoteService(false)]
        public override Task<TGetOutputDto> CreateAsync(TCreateInput input)
        {
            return base.CreateAsync(input);
        }

        [RemoteService(false)]
        public override Task DeleteAsync(TKey id)
        {
            return base.DeleteAsync(id);
        }

        [RemoteService(false)]
        public override Task<TGetOutputDto> GetAsync(TKey id)
        {
            return base.GetAsync(id);
        }
    }
}
