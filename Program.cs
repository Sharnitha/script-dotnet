var builder = WebApplication.CreateBuilder(args);
 
// Register GitHubService with HttpClient
builder.Services.AddHttpClient<GitHubService>();
 
var app = builder.Build();
app.UseStaticFiles();
app.UseRouting();
app.MapControllerRoute(name: "default", pattern: "{controller=Home}/{action=Index}/{id?}");
app.Run();
