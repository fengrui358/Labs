using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AbpLab.BaseServices;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Identity;

namespace AbpLab.Organization
{
    public class OrganizationService : CutCrudAppService<OrganizationUnit, OrganizationUnitDto, Guid, PagedAndSortedResultRequestDto, CreateOrganizationUnitDto>
    {
        private readonly OrganizationUnitManager _organizationUnitManager;
        private readonly IdentityUserManager _identityUserManager;

        public OrganizationService(IRepository<OrganizationUnit, Guid> repository, OrganizationUnitManager organizationUnitManager, IdentityUserManager identityUserManager) : base(repository)
        {
            _organizationUnitManager = organizationUnitManager;
            _identityUserManager = identityUserManager;
        }

        [RemoteService]
        public override async Task<OrganizationUnitDto> CreateAsync(CreateOrganizationUnitDto input)
        {
            var entity = await MapToEntityAsync(input);
            await _organizationUnitManager.CreateAsync(entity);

            return await MapToGetOutputDtoAsync(entity);
        }

        [RemoteService]
        public override Task<PagedResultDto<OrganizationUnitDto>> GetListAsync(PagedAndSortedResultRequestDto input)
        {
            return base.GetListAsync(input);
        }

        public async Task AddRoleToOrganizationUnitAsync(Guid id, Guid roleId)
        {
            await _organizationUnitManager.AddRoleToOrganizationUnitAsync(roleId, id);
        }

        public async Task RemoveRoleFromOrganizationUnitAsync(Guid id, Guid roleId)
        {
            await _organizationUnitManager.RemoveRoleFromOrganizationUnitAsync(roleId, id);
        }

        public async Task SetOrganizationUnitsAsync(Guid userId, params Guid[] organizationUnitIds)
        {
            await _identityUserManager.SetOrganizationUnitsAsync(userId, organizationUnitIds);
        }

        public async Task RemoveFromOrganizationUnitAsync(Guid userId, Guid organizationUnitId)
        {
            await _identityUserManager.RemoveFromOrganizationUnitAsync(userId, organizationUnitId);
        }

        public async Task<IList<string>> GetRolesAsync(Guid userId)
        {
            var identityUser = await _identityUserManager.GetByIdAsync(userId);
            return await _identityUserManager.GetRolesAsync(identityUser);
        }
    }
}
