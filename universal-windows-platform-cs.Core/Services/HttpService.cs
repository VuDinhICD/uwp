using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;
using universal_windows_platform_cs.Core.Helpers;

namespace universal_windows_platform_cs.Core.Services
{
    public class PostData
    {
        [JsonProperty("form")]
        public Dictionary<string, string> Form { get; set; }
    }

    public class GetData
    {
        [JsonProperty("url")]
        public string Url { get; set; }
    }

    public static class HttpService
    {
        private static readonly HttpClient client = new HttpClient();

        public static async Task<PostData> Post(string Url, Dictionary<string, string> values)
        {

            try
            {
                var content = new FormUrlEncodedContent(values);
                var response = await client.PostAsync(Url, content);
                string data = await response.Content.ReadAsStringAsync();
                return await Json.ToObjectAsync<PostData>(data);
            } catch (Exception ex)
            {
                Debug.WriteLine($"HttpClient: {ex}");
                return null;
            }
        }

        public static async Task<GetData> Get(string Url)
        {
            try
            {
                var data = await client.GetStringAsync(Url);
                return await Json.ToObjectAsync<GetData>(data);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"HttpClient: {ex}");
                return null;
            }
        }
    }
}
