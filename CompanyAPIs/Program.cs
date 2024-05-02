

global using Microsoft.EntityFrameworkCore;
global using CompanyAPIs.Data;
global using CompanyAPIs.Models;
using CompanyAPIs.Services.UsersService;
using CompanyAPIs.Helpers;
using NuGet.Protocol;
using System.Configuration;
using Microsoft.AspNetCore.Identity;
using CompanyAPIs.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using HRCom.Domain.Contracts.Interfaces.Services;
using HRCom.Utilities.Services;

var builder = WebApplication.CreateBuilder(args);



// Add services to the container.

builder.Services.Configure<JWT>(options => builder.Configuration.GetSection("JWT").Bind(options));


//Add Identity Roles 

builder.Services.AddIdentity<ApplicationUser, IdentityRole>().AddEntityFrameworkStores<ApplicationDbContext>();   

//Default Connection String

builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddControllers();

//Added For Reset Pass
builder.Services.Configure<DataProtectionTokenProviderOptions>(opts=>opts.TokenLifespan = TimeSpan.FromHours(10));

//Add Auth Interface and Auth Service Class

builder.Services.AddScoped<IAuthService , AuthService>();
builder.Services.AddScoped<IOperationsService , OperationService>();
builder.Services.AddScoped<IUserDataProvider , UserDataProvider>();


//Add Authentication

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(o =>
{
    o.RequireHttpsMetadata = false;
    o.SaveToken = false;
    o.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidIssuer = builder.Configuration["JWT:Issure"],
        ValidAudience = builder.Configuration["JWT:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Key"]))

    };
});



// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//builder.Services.AddDbContext<DataContext>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

//Add Authentication before authorization

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
