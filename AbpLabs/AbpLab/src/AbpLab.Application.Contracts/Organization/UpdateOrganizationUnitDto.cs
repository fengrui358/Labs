using System.ComponentModel.DataAnnotations;

namespace AbpLab.Organization
{
    public class UpdateOrganizationUnitDto
    {
        /// <summary>
        /// Display name of this OrganizationUnit.
        /// </summary>
        [Required]
        [MaxLength(128)]
        public virtual string DisplayName { get; set; }
    }
}
