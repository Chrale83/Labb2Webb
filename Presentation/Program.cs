using MudBlazor.Services;
using Presentation.Components;
using Presentation.DTOs;
using Presentation.States;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents().AddInteractiveServerComponents();

builder.Services.AddScoped(sp => new HttpClient
{
    BaseAddress = new Uri("https://localhost:7262"),
});

builder.Services.AddSingleton<AppState>();

builder.Services.AddMudServices();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var appState = services.GetRequiredService<AppState>();
    var httpClient = services.GetRequiredService<HttpClient>();

    try
    {
        appState.Categories =
            await httpClient.GetFromJsonAsync<List<CategoryDtoApi>>("api/categories")
            ?? new List<CategoryDtoApi>();
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Fel vid inläsning av kategorier: {ex.Message}");
    }
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>().AddInteractiveServerRenderMode();

app.Run();
