using labAPI;
using Microsoft.EntityFrameworkCore;      
using labAPI.Controllers;
using AutoMapper;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors();

// Add services to the container.
builder.Services.AddDbContext<LabDBContext>(options =>
{
    var connectionString = builder.Configuration.GetConnectionString("LabDBConnectionString");
    options.UseSqlServer(connectionString);
}); 
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddSwaggerGen();
var mappingConfig = new MapperConfiguration(mc =>
{
    mc.AddProfile(new MappingProfiles());
});
IMapper autoMapper = mappingConfig.CreateMapper();
builder.Services.AddSingleton(autoMapper);
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors(x => x
.AllowAnyMethod()
.AllowAnyHeader()
.AllowAnyOrigin()
.SetIsOriginAllowed(origin =>true)
);

app.UseAuthorization();

app.MapControllers();

app.MapLabEndpoints();

app.MapAcademicEndpoints();

app.MapChemicalsEndpoints();

app.MapEquipmentEndpoints();

app.MapNonAcademicEndpoints();

app.MapElementsEndpoints();

app.MapExperimentEndpoints();

app.Run();
