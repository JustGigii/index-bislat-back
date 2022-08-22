// dotnet ef dbcontext scaffold Name=DefaultConnection Pomelo.EntityFrameworkCore.MySql --output-dir Models --context-dir Data --namespace index_bislatContext -f --no-onconfiguring
// https://www.youtube.com/watch?v=pzFY45La2LE
// https://github.com/CuriousDrive/EFCore.AllDatabasesConsidered/tree/main/MySQL/Northwind.MySQL
// https://github.com/teddysmithdev/pokemon-review-api
// https://www.youtube.com/watch?v=v7q3pEK1EA0

using Index_Bislat_Back.Helper;
using Index_Bislat_Back.Interfaces;
using Index_Bislat_Back.Repository;
using index_bislatContext;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using Swashbuckle.AspNetCore.Filters;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddHttpContextAccessor();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
    {
        Description = "Standard Authorization header using the Bearer scheme)",
        In = ParameterLocation.Header,
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer"
    });

    options.OperationFilter<SecurityRequirementsOperationFilter>();
});
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.Unicode
                .GetBytes(builder.Configuration.GetSection("AppSettings:Token").Value)),
            ValidateIssuer = false,
            ValidateAudience = false
        };
    });
builder.Services.AddScoped<IAifBase, AifbaseRepository>();
builder.Services.AddScoped<ICourse, CourseRepository>();
builder.Services.AddScoped<ISortCycle, SortCycleRepository>();
builder.Services.AddScoped<IChoisetable, ChoisetableRepository>();
builder.Services.AddScoped<IClaimService, ClaimService>();
builder.Services.AddDbContext<indexbislatContext>(options =>
{
    options.UseMySql(builder.Configuration.GetConnectionString("DefaultConnection"),ServerVersion.Parse("5.7.37-log"));
});
builder.Services.AddMvc(option => option.EnableEndpointRouting = false)
    .AddNewtonsoftJson(opt => opt.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore);


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

 app.Run();
