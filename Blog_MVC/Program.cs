using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;



var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();



var app = builder.Build();

//-----------------------------------------------------------------------
void ConfigureServices(IServiceCollection services)
{
	services.AddControllersWithViews();               /*services.Addmvc metodu ile proje seviyesinde autherize iþlemi yaptýk*/

	services.AddMvc(config =>
	{
		var policy = new AuthorizationPolicyBuilder()
		.RequireAuthenticatedUser()
		.Build();

		config.Filters.Add(new AuthorizeFilter(policy));
	});
}
//-----------------------------------------------------------------------


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseStatusCodePagesWithReExecute("/ErrorPage/Error1", "?code={0}");   //ERROR SAYFASI

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
