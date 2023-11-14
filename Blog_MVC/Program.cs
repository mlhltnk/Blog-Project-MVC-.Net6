using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSession();
builder.Services.AddControllersWithViews();

// Cookie Authentication
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
	.AddCookie(options =>
	{
		options.LoginPath = "/Login/Index";
		options.AccessDeniedPath = "/Error/AccessDenied"; // Opsiyonel: Eriþim reddedildiðinde yönlendirilecek sayfa
		options.ReturnUrlParameter = "returnUrl"; // Opsiyonel: Yönlendirme için kullanýlacak parametre adý
	});

// Global Authorization Policy
var globalAuthorizationPolicy = new AuthorizationPolicyBuilder()
	.RequireAuthenticatedUser()
	.Build();

builder.Services.AddAuthorization(options =>
{
	options.DefaultPolicy = globalAuthorizationPolicy;
});

var app = builder.Build();

app.UseSession();
app.UseExceptionHandler("/Home/Error");
app.UseStatusCodePagesWithReExecute("/ErrorPage/Error1", "?code={0}");

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

// UseAuthorization should come before UseAuthentication
app.UseAuthorization();

// Cookie Authentication
app.UseAuthentication();

app.MapControllerRoute(
	name: "default",
	pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
