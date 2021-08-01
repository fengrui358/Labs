using System;
using Microsoft.AspNetCore.Identity;

namespace AspnetCoreIdentityLab.Models
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        /// <summary>
        /// 家庭地址
        /// </summary>
        public string FamilyAddress { get; set; }
    }
}
