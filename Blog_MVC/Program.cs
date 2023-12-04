using Microsoft.AspNetCore.Authentication.Cookies;


var builder = WebApplication.CreateBuilder(args);



builder.Services.AddControllersWithViews();

builder.Services.AddSession();


// Çerez Tabanlý Kimlik Doðrulama
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.Cookie.Name = "deneme";
        options.LoginPath = "/Login/Index";         //cookie bulunamazsa buraya gider
        options.AccessDeniedPath = "/Login/Index";  //yetkisiz kullanýcýlar buraya gider
    });



var app = builder.Build();

// HTTP isteði pipeline'ýný yapýlandýr.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseStatusCodePagesWithReExecute("/ErrorPage/Error1", "?code={0}");

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();


app.UseAuthentication();  

app.UseAuthorization();

app.UseSession();


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
