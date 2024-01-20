using DataAccessLayer.Concrete;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllersWithViews();



builder.Services.AddMvc(config=>         //ÝDENTÝTYDEN SONRA YAZDIM***
{
    var policy =new AuthorizationPolicyBuilder()
    .RequireAuthenticatedUser()
    .Build();
    config.Filters.Add(new AuthorizeFilter(policy));
});



builder.Services.AddDbContext<Context>();                   //ÝDENTÝTY KÜTÜPHANESÝ ÝÇÝN EKLENDÝ



builder.Services.AddIdentity<AppUser, AppRole>(x=>
{
    x.Password.RequireUppercase = false;                    //ÝDENTÝTYDE ÝSTEMEDÝÐÝMÝZ KURALLARI FALSE YAPMAK
    x.Password.RequireNonAlphanumeric = false;              //ÝDENTÝTYDE ÝSTEMEDÝÐÝMÝZ KURALLARI FALSE YAPMAK
})
    .AddEntityFrameworkStores<Context>();    






builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)    // Çerez Tabanlý Kimlik Doðrulama
    .AddCookie(options =>
    {
        options.Cookie.Name = "deneme";
        options.LoginPath = "/Login/index";                                             //cookie bulunamazsa buraya gider
        options.AccessDeniedPath = "login/index";                                       //yetkisiz kullanýcýlar buraya gider
    });



builder.Services.ConfigureApplicationCookie(options =>                              //ÝDENTÝTY ÝÞLEMÝNDEN SONRA YAZDIM.***
{
    options.Cookie.HttpOnly = true;
    options.ExpireTimeSpan = TimeSpan.FromMinutes(100);                             //session süresi 100 dk
    options.AccessDeniedPath = new PathString("/Login/AccessDenied");               //yetkisiz kullanýcýnýn yönlendirileceði sayfa
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
    endpoints.MapAreaControllerRoute(                                     //AREAS ROUTE tanýmý
        name: "Admin",
        areaName: "Admin",
        pattern: "Admin/{controller=Category}/{action=Index}/{id?}"
    );

    endpoints.MapControllerRoute(                                        //AREAS ROUTE tanýmý
        name: "area",
        pattern: "{area:exists}/{controller}/{action}/{id?}"
    );

    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}"
    );
});



app.Run();
