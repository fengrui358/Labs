using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace AspnetCoreIdentityLab.Models
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        /// <summary>
        /// 家庭地址
        /// </summary>
        [MaxLength(256)]
        public string FamilyAddress { get; set; }
    }
}
