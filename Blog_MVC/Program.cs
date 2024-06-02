using DataAccessLayer.Concrete;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllersWithViews();



builder.Services.AddMvc(config=>         
{
    var policy =new AuthorizationPolicyBuilder()
    .RequireAuthenticatedUser()
    .Build();
    config.Filters.Add(new AuthorizeFilter(policy));
});



builder.Services.AddDbContext<Context>();                   



builder.Services.AddIdentity<AppUser, AppRole>(x=>
{
    x.Password.RequireUppercase = false;                    
    x.Password.RequireNonAlphanumeric = false;              
})
    .AddEntityFrameworkStores<Context>();    






builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)    
    .AddCookie(options =>
    {
        options.Cookie.Name = "deneme";
        options.LoginPath = "/Login/index";                                         
        options.AccessDeniedPath = "login/index";                                       
    });



builder.Services.ConfigureApplicationCookie(options =>                              
{
    options.Cookie.HttpOnly = true;
    options.ExpireTimeSpan = TimeSpan.FromMinutes(100);                            
    options.AccessDeniedPath = new PathString("/Login/AccessDenied");               
    options.LoginPath = "/Login/Index/";
    options.SlidingExpiration = true;

});




builder.Services.AddSession();


var app = builder.Build();

// HTTP isteði pipeline'ýný yapýlandýr.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseStatusCodePagesWithReExecute("/ErrorPage/Error1", "?code={0}");

app.UseHttpsRedirection();


app.UseRouting();

app.UseSession();

app.UseStaticFiles();

app.UseAuthentication();  

app.UseAuthorization();


app.UseEndpoints(endpoints =>
{
    endpoints.MapAreaControllerRoute(                                     //AREAS ROUTE 
        name: "Admin",
        areaName: "Admin",
        pattern: "Admin/{controller=Category}/{action=Index}/{id?}"
    );

    endpoints.MapControllerRoute(                                        //AREAS ROUTE 
        name: "area",
        pattern: "{area:exists}/{controller}/{action}/{id?}"
    );

    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}"
    );
});



app.Run();
