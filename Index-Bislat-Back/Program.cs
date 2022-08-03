// dotnet ef dbcontext scaffold Name=DefaultConnection Pomelo.EntityFrameworkCore.MySql --output-dir Models --context-dir Data --namespace index_bislatContext -f --no-onconfiguring
// https://www.youtube.com/watch?v=pzFY45La2LE
// https://github.com/CuriousDrive/EFCore.AllDatabasesConsidered/tree/main/MySQL/Northwind.MySQL
// https://github.com/teddysmithdev/pokemon-review-api
// https://www.youtube.com/watch?v=v7q3pEK1EA0
using Index_Bislat_Back.Interfaces;
using Index_Bislat_Back.Repository;
using index_bislatContext;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IAifBase, AifbaseRepository>();
builder.Services.AddScoped<ICourse, CourseRepository>();
builder.Services.AddScoped<ISortCycle, SortCycleRepository>();
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

app.UseAuthorization();

app.MapControllers();

 app.Run();
