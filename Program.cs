using Microsoft.AspNetCore.OData;
using Microsoft.EntityFrameworkCore;
using Microsoft.OData.Edm;
using Microsoft.OData.ModelBuilder;
using ODataTutorial.Entities;
using ODataTutorial.EntityFramework;
using ODataTutorial.Models;

static IEdmModel GetEdmModel()
{
    ODataConventionModelBuilder builder = new();
    builder.EntitySet<UserActivityLog>("Notes");
    return builder.GetEdmModel();
}

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<db_sfsyncContext>();
//builder.Services.AddControllers(mvcoptions => mvcoptions.EnableEndpointRouting = false).AddOData(options => options.Select().Filter().Count().OrderBy().Expand().SetMaxTop(1000));
builder.Services.AddControllers().AddOData(opt => opt.AddRouteComponents("v1", GetEdmModel()).Filter().Select().Expand().OrderBy().SetMaxTop(null).Count());
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() { Title = "ODataTutorial", Version = "v1" });
});


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddScoped<DbContext, db_sfsyncContext>();
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




