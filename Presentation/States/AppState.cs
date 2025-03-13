using Microsoft.AspNetCore.Components;
using Presentation.DTOs;
using static System.Net.WebRequestMethods;

namespace Presentation.States
{
    public class AppState
    {
        public List<CategoryDtoApi> Categories { get; set; } = new List<CategoryDtoApi>();

        public async Task InitializeAsync(HttpClient http)
        {
            if (Categories.Count == 0)
            {
                Categories =
                    await http.GetFromJsonAsync<List<CategoryDtoApi>>("api/categories")
                    ?? new List<CategoryDtoApi>();
            }
        }
    }
}
