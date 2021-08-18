﻿using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using IdentityServer4.Extensions;
using IdentityServer4.Models;
using IdentityServer4.Services;
using IdentityServer4.Test;
using Microsoft.Extensions.Logging;

namespace IdentityServer.Quickstart
{
    /// <summary>
    /// Profile service for test users
    /// </summary>
    /// <seealso cref="IdentityServer4.Services.IProfileService" />
    public class TestUserProfileService : IProfileService
    {
        /// <summary>
        /// The logger
        /// </summary>
        protected readonly ILogger Logger;

        /// <summary>
        /// The users
        /// </summary>
        protected readonly TestUserStore Users;

        /// <summary>
        /// Initializes a new instance of the <see cref="TestUserProfileService"/> class.
        /// </summary>
        /// <param name="users">The users.</param>
        /// <param name="logger">The logger.</param>
        public TestUserProfileService(TestUserStore users, ILogger<TestUserProfileService> logger)
        {
            Users = users;
            Logger = logger;
        }

        /// <summary>
        /// This method is called whenever claims about the user are requested (e.g. during token creation or via the userinfo endpoint)
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns></returns>
        public virtual Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            context.LogProfileRequest(Logger);

            if (context.RequestedClaimTypes.Any())
            {
                var user = Users.FindBySubjectId(context.Subject.GetSubjectId());
                if (user != null)
                {
                    // add claims

                    var claimTypes = context.RequestedClaimTypes.ToList();
                    claimTypes.Add("custom_claim");
                    context.RequestedClaimTypes = claimTypes;
                    context.AddRequestedClaims(user.Claims);
                    //context.IssuedClaims.Add(new Claim("custom_claim", "dasfaf"));
                }
            }

            context.LogIssuedClaims(Logger);

            return Task.CompletedTask;
        }

        /// <summary>
        /// This method gets called whenever identity server needs to determine if the user is valid or active (e.g. if the user's account has been deactivated since they logged in).
        /// (e.g. during token issuance or validation).
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns></returns>
        public virtual Task IsActiveAsync(IsActiveContext context)
        {
            Logger.LogDebug("IsActive called from: {caller}", context.Caller);

            var user = Users.FindBySubjectId(context.Subject.GetSubjectId());
            context.IsActive = user?.IsActive == true;

            return Task.CompletedTask;
        }
    }
}
