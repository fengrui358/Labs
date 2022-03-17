using System;
using System.Threading.Tasks;
using AbpLab.BaseServices;
using Volo.Abp;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Identity;

namespace AbpLab.Organization
{
    public class OrganizationService : CutCrudAppService<OrganizationUnit, OrganizationUnitDto, Guid, OrganizationUnitDto, CreateOrganizationUnitDto>
    {
        private readonly OrganizationUnitManager _organizationUnitManager;

        public OrganizationService(IRepository<OrganizationUnit, Guid> repository, OrganizationUnitManager organizationUnitManager) : base(repository)
        {
            _organizationUnitManager = organizationUnitManager;
        }

        [RemoteService]
        public override async Task<OrganizationUnitDto> CreateAsync(CreateOrganizationUnitDto input)
        {
            var entity = await MapToEntityAsync(input);
            await _organizationUnitManager.CreateAsync(entity);

            return await MapToGetOutputDtoAsync(entity);
        }
    }
}
