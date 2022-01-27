using AuthDemo.Blazor.Shared;
using Microsoft.AspNetCore.Components.Authorization;
using System.Net.Http.Json;
using System.Security.Claims;

namespace AuthDemo.Blazor.Client.Services
{
    public class AuthStateProvider : AuthenticationStateProvider
    {
        private readonly HttpClient httpClient;

        public AuthStateProvider(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }
        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var model = await httpClient.GetFromJsonAsync<UserModel>("api/Auth");

            var identity = new ClaimsIdentity(new[]
            {
                    new Claim(ClaimTypes.Name, model.Name),
            });

            var user = new ClaimsPrincipal(identity);

            return new AuthenticationState(user);
        }
    }
}
