using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics;
using System.Threading.Tasks;
using universal_windows_platform_cs.Core.Services;

namespace universal_windows_platform_cs.Tests.MSTest
{
    [TestClass]
    public class TestsDbContext
    {
        private static readonly string PageName = "TestsDbContext";

        [TestMethod]
        public async Task TestConnection()
        {
            var result = await DataSourceService.TestConnection();
            if (result)
            {
                Assert.IsNotNull(result);
            }
            else
            {
                Debug.WriteLine($"{PageName}: Connect fail!");
                Assert.Fail();
            }
        }
    }
}
