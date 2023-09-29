using logger_extension_sanitizer;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);
builder.Logging.ClearProviders();
builder.Logging.AddConsole();

// Add services to the container.
builder.Services.AddRazorPages();

var app = builder.Build();

app.MapGet("/Test", async (ILogger<Program> logger, HttpResponse response) =>
{
    logger.LogInformation("Testing logging in Program.cs");
    await response.WriteAsync("Testing");
});

app.MapGet("/Finding", (ILogger<Program> logger, [FromQuery] string UserInput) =>
{
    logger.LogInformation("Unsanitized user input " + UserInput);
    return "Hello World!";
});

app.MapGet("/Sanitized", (ILogger<Program> logger, [FromQuery] string UserInput) =>
{
    logger.LogInformation("Sanitized user input " + UserInput.Replace(Environment.NewLine, string.Empty));
    return "Hello World!";
});

app.MapGet("/SanitizedWithFinding", (ILogger<Program> logger, [FromQuery] string UserInput) =>
{
    logger.LogInformation("Sanitized user input" + logger.Sanitize(UserInput));
    return "Hello World!";
});


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
