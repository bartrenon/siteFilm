var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "film_genre",
    pattern: "films/{genre}/{action=Index}",
    defaults: new { Controller = "Film" }
    ).WithStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Film}/{action=Index}/{id?}")
    .WithStaticAssets();

app.Run();
