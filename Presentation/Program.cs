using MudBlazor.Services;
using Presentation.Components;
using Presentation.DTOs;
using Presentation.States;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents().AddInteractiveServerComponents();

//builder.Services.AddSingleton(sp => new HttpClient
//{
//    BaseAddress = new Uri("https://localhost:7262"),
//});

builder.Services.AddSingleton<AppState>();
builder.Services.AddMudServices();
builder.Services.AddHttpClient(
    "MyAPI",
    client =>
    {
        client.BaseAddress = new Uri("https://localhost:7262");
    }
);

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider; //    scope.ServiceProvider används för att hämta tjänster inom det nya scopet.
    var appState = services.GetRequiredService<AppState>(); //services.GetRequiredService<AppState>() hämtar AppState-instansen från DI.
    var httpClientFactory = services.GetRequiredService<IHttpClientFactory>();
    var httpClient = httpClientFactory.CreateClient("MyAPI");

    try
    {
        appState.Categories =
            await httpClient.GetFromJsonAsync<List<CategoryDTO>>("api/categories")
            ?? new List<CategoryDTO>();
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
