using WebfejlesztesWeb.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton(x => new HttpClient { BaseAddress = new Uri("http://localhost:5189") });

builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddSingleton<AuthenticationService>();
builder.Services.AddTransient<CharterService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}

#if RELEASE
app.Urls.Add("http://0.0.0.0:5000");
#endif

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
