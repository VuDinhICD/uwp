using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using universal_windows_platform_cs.Core.Services;

namespace universal_windows_platform_cs.Tests.MSTest
{
    [TestClass]
    public class TestsHttpService
    {
        private static readonly string PageName = "TestsHttpService";
        private static readonly string UrlPost = "https://httpbin.org/post";
        private static readonly string UrlGet = "https://httpbin.org/get";

        [TestMethod]
        public async Task TestGet()
        {
            GetData data = await HttpService.Get(UrlGet);
            // Debug.WriteLine(data);
            Assert.IsNotNull(data);
            Assert.AreEqual(data.Url, UrlGet);
        }

        [TestMethod]
        public async Task TestPost()
        {
            var values = new Dictionary<string, string>
            {
                { "string1", "hello" },
                { "string2", "world" }
            };
            PostData data = await HttpService.Post(UrlPost, values);
            // Debug.WriteLine(data);
            Assert.IsNotNull(data);
            Assert.AreEqual(data.Form["string1"], "hello");
            Assert.AreEqual(data.Form["string2"], "world");
        }
    }
}
