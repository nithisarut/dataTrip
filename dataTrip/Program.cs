using Autofac;
using Autofac.Extensions.DependencyInjection;
using dataTrip.Models;
using dataTrip.Setting;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Reflection;
using System.Text;

var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

var builder = WebApplication.CreateBuilder(args);



// Add services to the container.

builder.Services.AddControllers();

//web jwt bearer core 6
//อธิบาย https://devahoy.com/blog/2016/07/understanding-token-and-jwt-create-authentication-with-hapijs/
//JWT https://www.freecodespot.com/blog/jwt-authentication-in-dotnet-core/
var jwtSetting = new JwtSetting();
//builder.Configuration.Bind(nameof(jwtSetting), jwtSetting);
builder.Services.AddSingleton(jwtSetting);

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
      .AddJwtBearer(options =>
      {
          options.TokenValidationParameters =
              new TokenValidationParameters
              {
                  ValidateIssuer = false,
                  ValidIssuer = "First",
                  ValidateAudience = true,
                  ValidAudience = jwtSetting.Audience,
                  ValidateLifetime = true,
                  ValidateIssuerSigningKey = true,


                  ValidateActor = false,

                  IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSetting.Key)),
                  ClockSkew = TimeSpan.Zero //ระยะเวลา
              };
      });

#region คือเอาไว้อนุยาติสิทฟ้อนเอน
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
        policy =>
        {
            policy.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin();
             //policy.AllowAnyHeader()
             //.AllowAnyMethod()
             //.AllowCredentials()
             //.WithOrigins("http://localhost:3000");
        });
});
#endregion

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<DatabaseContext>();

//----------------------- จะได้ไม่ต้องลงทะเบียนหลายอัน ---------------------------
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory(containerBuilder =>
{
    containerBuilder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly())
    //- จะค้นหาชื่อไฟล์ที่ลงท้ายด้วย Service แล้วจะ DI โดยอัตโนมัติ
    .Where(t => t.Name.EndsWith("Service") || t.Name.EndsWith("Test"))
    .AsImplementedInterfaces();
}));
// สร้างเพียงหนึ่งครั้งเท่านั้น refresh ค่าก้ไม่เปลี่ยน
//builder.Services.AddSingleton<IProductService, ProductService>();
// สร้างเพียงครั้งเดียว
//builder.Services.AddScoped<IProductService, ProductService>();
// สร้างหลายครั้ง
//builder.Services.AddTransient<IProductService, ProductService>();





var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();
if (app.Environment.IsDevelopment())
{
    
}


app.UseDefaultFiles(); // อนุญาตให้เรียกไฟล์ต่างๆ ใน wwwroot

app.UseStaticFiles(); // อนุญาตให้เข้าถึงไฟล์ค่าคงที่ได้ 

app.UseRouting();

app.UseCors(MyAllowSpecificOrigins);
//การตรวจสอบสิทธิ์
app.UseAuthentication();
//การอนุญาต
app.UseAuthorization();

app.MapControllers();

app.UseEndpoints(endpoints =>
{
endpoints.MapControllers();
endpoints.MapFallbackToController("Index", "Fallback"); // บอกเส้นทางมันก่อน
});


app.Run();

