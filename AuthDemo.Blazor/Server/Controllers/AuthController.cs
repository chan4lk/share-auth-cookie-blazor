using AuthDemo.Blazor.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net;
using System.Text;
using System.Xml.Serialization;

namespace AuthDemo.Blazor.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        HttpClient _httpClient { get; set; }

        public AuthController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("internal");
        }
        public async Task<UserModel> GetUser()
        {
            var baseAddress = _httpClient.BaseAddress;
            var cookieContainer = new CookieContainer();
            using (var handler = new HttpClientHandler() { CookieContainer = cookieContainer })
            using (var client = new HttpClient(handler) { BaseAddress = baseAddress })
            {
                var content = new FormUrlEncodedContent(new[]
                {
                    new KeyValuePair<string, string>("value", HttpContext.Request.Cookies[".ASPXAUTH"]),
                });
                cookieContainer.Add(baseAddress, new Cookie(".ASPXAUTH", HttpContext.Request.Cookies[".ASPXAUTH"]));
                var result = await client.PostAsync("/AuthService.asmx/GetUser", content);
                var data = await result.Content.ReadAsStringAsync();
                var model = DeserializeObject<UserModel>(data);

                return model;
            }           
        }

        public T DeserializeObject<T>(string s)
        {
            var buffer = Encoding.UTF8.GetBytes(s);
            using (var stream = new MemoryStream(buffer))
            {
                var serializer = new XmlSerializer(typeof(T));
                var deliciousSuggest = (T)serializer.Deserialize(stream);
                return deliciousSuggest;
            }
        }
    }
}
