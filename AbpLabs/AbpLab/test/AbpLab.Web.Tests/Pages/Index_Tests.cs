using System.Threading.Tasks;
using Shouldly;
using Xunit;

namespace AbpLab.Pages;

public class Index_Tests : AbpLabWebTestBase
{
    [Fact]
    public async Task Welcome_Page()
    {
        var response = await GetResponseAsStringAsync("/");
        response.ShouldNotBeNull();
    }
}
