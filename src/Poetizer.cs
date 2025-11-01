using System.Net.Http;
using System.Text.Json;
using System.Net.Http.Json;
using System.Net.Http.Headers;

namespace PoetizerApi
{
    public class Poetizer
    {
        private string deviceToken;
        private readonly HttpClient httpClient;
        private readonly string apiUrl = "https://api.poetizer.com/v6";
        public Poetizer()
        {
            httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.UserAgent.ParseAdd("Poetizer/3.0.9 (30693) Android/9 (samsung; SM-N9860)");
            httpClient.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<string> Login(string email, string password)
        {
            var data = JsonContent.Create(new
            {
                email = email,
                password = password,
                platform = "android",
                device_name = "samsung SM-N9860"
            });
            var response = await httpClient.PostAsync($"{apiUrl}/devices/email", data);
            var content = await response.Content.ReadAsStringAsync();
            var document = JsonDocument.Parse(content);
            if (document.RootElement.TryGetProperty("device_token", out var tokenElement))
            {
                deviceToken = tokenElement.GetString();
                httpClient.DefaultRequestHeaders.Add("Authorization", $"Device-token {deviceToken}");
            }
            return content;
        }

        public async Task<string> Register(string email, string password)
        {
            var data = JsonContent.Create(new
            {
                email = email,
                password = password,
                platform = "android",
                device_name = "samsung SM-N9860"
            });
            var response = await httpClient.PostAsync($"{apiUrl}/register", data);
            return await response.Content.ReadAsStringAsync();
        }

        public async Task<string> GetSettings()
        {
            var response = await httpClient.GetAsync($"{apiUrl}/settings");
            return await response.Content.ReadAsStringAsync();
        }

        public async Task<string> GetAccountInfo()
        {
            var response = await httpClient.GetAsync($"{apiUrl}/users/me");
            return await response.Content.ReadAsStringAsync();
        }

        public async Task<string> GetNewestPoems(int offset = 0, int limit = 10)
        {
            var response = await httpClient.GetAsync($"{apiUrl}/poems/newest?offset={offset}&limit={limit}");
            return await response.Content.ReadAsStringAsync();
        }

        public async Task<string> GetPopularPoems(string period, int offset = 0, int limit = 10)
        {
            var response = await httpClient.GetAsync($"{apiUrl}/poems/popular?limit={limit}&offset={offset}&period={period}");
            return await response.Content.ReadAsStringAsync();
        }
    }
}
