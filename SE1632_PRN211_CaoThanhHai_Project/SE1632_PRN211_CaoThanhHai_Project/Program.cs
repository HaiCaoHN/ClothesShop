var builder = WebApplication.CreateBuilder(args);

//ADD
builder.Services.AddControllersWithViews();

var app = builder.Build();

//EDIT
//app.MapGet("/", () => "Hello World!");
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}"
    );

app.Run();
app.UseStaticFiles();