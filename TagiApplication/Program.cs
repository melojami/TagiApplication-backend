using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using TagiApplication.Converters;
using TagiApplication.DAL;
using TagiApplication.Models;
using TagiApplication.Operations;
using TagiApplication.Repositories;

var builder = WebApplication.CreateBuilder(args);

// CORS
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
        policy =>
        {
            //policy.WithOrigins("http://localhost:8080");
            //policy.WithMethods("GET","POST");
            policy.SetIsOriginAllowed(origin => new Uri(origin).Host == "localhost");
            policy.AllowAnyHeader().AllowAnyMethod();
        });
});

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<TagiContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("TagiDb")));

//builder.Services.AddScoped<ITagiRepository, TagiRepository>();
builder.Services.AddScoped(typeof(ICommonRepository<,,>), typeof(CommonRepository<,,>));
//builder.Services.AddScoped(typeof(ICommonOperations<,,>), typeof(CommonOperations<,,>));

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();


builder.Services.AddScoped<ITagiOperations, TagiOperations>();
builder.Services.AddScoped<ITagityyppiOperations, TagityyppiOperations>();
builder.Services.AddScoped<IResurssiOperations, ResurssiOperations>();
builder.Services.AddScoped<IResurssiTagiOperations, ResurssiTagiOperations>();
builder.Services.AddScoped<IJonoOperations, JonoOperations>();
builder.Services.AddScoped<IJonottajaOperations, JonottajaOperations>();
builder.Services.AddScoped<IJonottajaTagiOperations, JonottajaTagiOperations>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

    // CORS
    app.UseCors();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
