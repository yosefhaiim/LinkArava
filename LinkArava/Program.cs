using LinkArava.Dal;
using Microsoft.EntityFrameworkCore;
using LinkArava.Controllers;
using LinkArava.Services;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

string Cs = ""
           + "server=DESKTOP-Q2PJJ02\\SQLEXPRESS ;"
           + "Initial Catalog = LinkArava ;"
           + "user id=sa ;"
           + "password=1234 ;"
           +"TrustServerCertificate=True;";

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// הוספנו את השורה הזו על מנת לבנות דאטה בייס
builder.Services.AddDbContext<DataLayer>(options => options.UseSqlServer(Cs));


//Dependency Injection כאו אנחנו מזריקים את יוזר סרוויס ובשורה הבאה את פוסט סרוויס, כך שבכל קריאה יווצר לי אי פי איי אחד
builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<PostService>();
builder.Services.AddScoped<JWTService>();


builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = false,

            ValidateAudience = false,

            ValidateLifetime = true,

            //ValidIssuer = builder.Configuration["JWT:Issuer"],
            //ValidAudience = builder.Configuration["JWT:Audience"],

            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:key"])),

            ValidateIssuerSigningKey = true
        };
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();


app.Run();
