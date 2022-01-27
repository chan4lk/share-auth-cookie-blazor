using AuthDemo.Blazor.Shared;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Xml.Serialization;

namespace AuthDemo.Blazor.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ILogger<AuthController> _logger;

        HttpClient _httpClient { get; set; }

        public AuthController(IHttpClientFactory httpClientFactory, ILogger<AuthController> logger)
        {
            _httpClient = httpClientFactory.CreateClient("internal");
            _logger = logger;
        }
        [HttpGet("")]
        public async Task<UserModel> Get()
        {
            try
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

                    _logger.LogInformation("auth result: {@data}", data);

                    var model = DeserializeObject<UserModel>(data);

                    if (model.IsAuthenticated)
                    {
                        var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, model.Name),
                    };

                        var claimsIdentity = new ClaimsIdentity(
                            claims, CookieAuthenticationDefaults.AuthenticationScheme);

                        var authProperties = new AuthenticationProperties
                        {

                        };

                        await HttpContext.SignInAsync(
                            CookieAuthenticationDefaults.AuthenticationScheme,
                            new ClaimsPrincipal(claimsIdentity),
                            authProperties);
                    }

                    return model;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
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
