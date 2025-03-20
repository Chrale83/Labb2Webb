using Presentation.DTOs;
using Presentation.Models;

namespace Presentation.States
{
    public class AppState
    {
        public List<CategoryDTO> Categories { get; set; } = new List<CategoryDTO>();
        public UserInfo UserInfo { get; set; } = new();
        public ProductDTO? SelectedProduct { get; set; }
        private bool _isLoggedin;
        public bool IsLoggedIn
        {
            get => _isLoggedin;
            set
            {
                if (_isLoggedin != value)
                {
                    _isLoggedin = value;
                    NotifyStateChanged();
                }
            }
        }

        public event Action? OnChange;

        private void NotifyStateChanged() => OnChange?.Invoke();

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
