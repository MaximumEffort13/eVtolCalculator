using Application;
using Infrastructure;
using Infrastructure.Persistence.DataAccess;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

var MyAllowedSpecificOrigins = "_myAllowSpecificOrigins";

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(x =>
{
    x.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please Provide a valid token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });

    x.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type =  ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },

            Array.Empty<string>()
        }
    });
});

builder.Services
    .AddApplication()
    .AddInfrastructure();

builder.Services.AddCors(policy =>
{
    policy.AddPolicy(MyAllowedSpecificOrigins, 
        
        polbuilder => polbuilder.WithOrigins("https://localhost:7232", "Postman")
         .AllowAnyMethod()
         .AllowAnyHeader()
         .AllowCredentials());
});

//builder.Services.AddAuthentication(options =>
//{
//    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
//    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
//    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
//})
//.AddJwtBearer("JwtBearer", jwtBearerOptions =>
//{
//    jwtBearerOptions.TokenValidationParameters = new TokenValidationParameters
//    {
//        ValidateIssuerSigningKey = true,
//        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration.GetValue<string>("Secrets:SecurityKey")!)),
//        ValidateIssuer = false,
//        ValidateAudience = false,
//        ValidateLifetime = true,
//        ClockSkew = TimeSpan.FromMinutes(5)
//    };
//}).AddCookie(options =>
//{
//    options.LoginPath = "localhost:7197:/account/login";
//    options.AccessDeniedPath = "/";
//});

builder.Services.AddAuthentication().AddBearerToken(IdentityConstants.BearerScheme);


builder.Services.AddAuthorizationBuilder();

builder.Services.AddIdentityCore<IdentityUser>().AddRoles<IdentityRole>().AddEntityFrameworkStores<ApplicationDbContext>().AddApiEndpoints();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors(MyAllowedSpecificOrigins);

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.MapGroup("/account").MapIdentityApi<IdentityUser>();

app.Run();
