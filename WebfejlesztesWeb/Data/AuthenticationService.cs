using System.ComponentModel.DataAnnotations;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using WebfejlesztesWeb.Data.Dto;

namespace WebfejlesztesWeb.Data
{
    public class AuthenticationService
    {
        private readonly HttpClient _httpClient;

        public bool IsAuthenticated { get; private set; }
        public event Action? AuthenticationStateChanged;

        public AuthenticationService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<string> SignIn(string email, string password)
        {
            var signInData = new SignInDto(email, password);
            var content = new StringContent(JsonSerializer.Serialize(signInData), Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("api/auth/signin", content);

            if (response.IsSuccessStatusCode)
            {
                var token = await response.Content.ReadAsStringAsync();
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                IsAuthenticated = true;
                AuthenticationStateChanged?.Invoke();
                return token;
            }
            else
            {
                return null;
            }
        }

        public void SignOut()
        {
            _httpClient.DefaultRequestHeaders.Authorization = null;
            IsAuthenticated = false;
            AuthenticationStateChanged?.Invoke();
        }

        public async Task<string> SignUp(string email, string password, string name, string phoneNumber, string address, string role, string gender)
        {
            var signUpData = new SignUpDto(email, password, name, phoneNumber, address, role = "USER", gender = "Male");
            var content = new StringContent(JsonSerializer.Serialize(signUpData), Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("api/auth/signup", content);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsStringAsync();
            }
            else
            {
                return null;
            }
        }
    }
}
