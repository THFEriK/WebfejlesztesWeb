using WebfejlesztesWeb.Data.Dto;

namespace WebfejlesztesWeb.Data
{
    public class CharterService
    {
        private readonly HttpClient _httpClient;

        public CharterService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<CharterDto>> GetChartersAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<CharterDto>>("api/charter");
        }

        public async Task<CharterDto> GetCharterByIdAsync(long id)
        {
            return await _httpClient.GetFromJsonAsync<CharterDto>($"api/charter/{id}");
        }

        public async Task CreateCharterAsync(CharterDto charter)
        {
            await _httpClient.PostAsJsonAsync("api/charter", charter);
        }

        public async Task UpdateCharterAsync(CharterDto charter)
        {
            await _httpClient.PutAsJsonAsync($"api/charter", charter);
        }

        public async Task DeleteCharterAsync(long id)
        {
            await _httpClient.DeleteAsync($"api/charter/{id}");
        }
    }
}
