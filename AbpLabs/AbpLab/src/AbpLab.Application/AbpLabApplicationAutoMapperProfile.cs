using AbpLab.Item;
using AbpLab.Organization;
using AutoMapper;
using Volo.Abp.Identity;

namespace AbpLab;

public class AbpLabApplicationAutoMapperProfile : Profile
{
    public AbpLabApplicationAutoMapperProfile()
    {
        /* You can configure your AutoMapper mapping configuration here.
         * Alternatively, you can split your mapping configurations
         * into multiple profile classes for a better organization. */

        CreateMap<OrganizationUnit, OrganizationUnitDto>();
        CreateMap<CreateOrganizationUnitDto, OrganizationUnit>();

        CreateMap<Item.Item, ItemDto>();
        CreateMap<CreateUpdateItemDto, Item.Item>();
    }
}
