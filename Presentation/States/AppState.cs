using Presentation.DTOs;

namespace Presentation.States
{
    public class AppState
    {
        public List<CategoryDTO> Categories { get; set; } = new List<CategoryDTO>();
        public ProductDTO SelectedProduct { get; set; }

        public async Task InitializeAsync(HttpClient http)
        {
            if (Categories.Count == 0)
            {
                Categories =
                    await http.GetFromJsonAsync<List<CategoryDTO>>("api/categories")
                    ?? new List<CategoryDTO>();
            }
        }
    }
}
