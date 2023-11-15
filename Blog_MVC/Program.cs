using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSession();

builder.Services.AddControllersWithViews();    //default


// Cookie Authentication
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
	.AddCookie(options =>
	{
		options.LoginPath = "/Login/Index";
		options.AccessDeniedPath = "/Login/Index"; // Opsiyonel: Eriþim reddedildiðinde yönlendirilecek sayfa
		options.ReturnUrlParameter = "returnUrl"; // Opsiyonel: Yönlendirme için kullanýlacak parametre adý
	});




/*
var globalAuthorizationPolicy = new AuthorizationPolicyBuilder()
	.RequireAuthenticatedUser()
	.Build();

builder.Services.AddAuthorization(options =>
{
	options.DefaultPolicy = globalAuthorizationPolicy;
});
*/



var app = builder.Build();

app.UseSession();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())      
{
	app.UseExceptionHandler("/Home/Error");
	// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
	app.UseHsts();
}

app.UseStatusCodePagesWithReExecute("/ErrorPage/Error1", "?code={0}");

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();


app.UseAuthorization();


app.UseAuthentication();

app.MapControllerRoute(
	name: "default",
	pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
