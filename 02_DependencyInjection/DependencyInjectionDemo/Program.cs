
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using DependencyInjectionDemo.Logic;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddTransient<IDemoLogic, DemoLogic>(); //when you ask for the demologic, you'll get a new instance (new values)
//builder.Services.AddSingleton<DemoLogic>();   //when you ask for the demologic, you'll get just one instance (every time the same value)
//builder.Services.AddScoped<DemoLogic>();  // when you ask for the demologic, you'll get a new instance for every time your request
//builder.Services.AddTransient<IDemoLogic, OtherDemoLogic>();  //instead of changing all codes about DemoLogic, we can change only this line

var app = builder.Build();

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

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
