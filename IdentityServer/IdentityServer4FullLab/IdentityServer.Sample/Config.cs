using IdentityServer4.Models;
using IdentityResource = IdentityServer4.Models.IdentityResource;

namespace IdentityServer.Sample
{
    public static class Config
    {
        /// <summary>
        /// 定义 identity resource
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new IdentityResource[]
            {
                new IdentityResources.OpenId()
            };
        }

        /// <summary>
        /// 定义 Api 资源
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<ApiResource> GetApis()
        {
            return new List<ApiResource>
            {
                new ApiResource("api1", "app1 api resource")
            };
        }

        /// <summary>
        /// 定义 Client
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<Client> GetClients()
        {
            return new List<Client>
            {
                new Client
                {
                    ClientId = "consoleClient",
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    ClientSecrets = { new Secret("consoleClientSecrets".Sha256()) },

                    // 允许的 api 范围
                    AllowedScopes = { "api1" }
                }
            };
        }
    }
}
