using Blazored.LocalStorage;
using MudBlazor.Services;
using Presentation.Components;
using Presentation.DTOs;
using Presentation.Interfaces;
using Presentation.Services;
using Presentation.States;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents().AddInteractiveServerComponents();

builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<ICustomerService, CustomerService>();
builder.Services.AddScoped<ShoppingCartService>();
builder.Services.AddSingleton<SharedState>();
builder.Services.AddMudServices();
builder.Services.AddHttpClient(
    "MyAPI",
    client =>
    {
        client.BaseAddress = new Uri("https://localhost:7262");
    }
)
/*.AddHttpMessageHandler<AuthTokenHandler>()*/;
;

builder.Services.AddBlazoredLocalStorage();

builder.Services.AddAuthorizationCore();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider; //    scope.ServiceProvider används för att hämta tjänster inom det nya scopet.
    var appState = services.GetRequiredService<SharedState>(); //services.GetRequiredService<AppState>() hämtar AppState-instansen från DI.
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

//app.UseAuthentication();
app.UseAuthorization();

app.UseHttpsRedirection();

app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>().AddInteractiveServerRenderMode();

app.Run();
