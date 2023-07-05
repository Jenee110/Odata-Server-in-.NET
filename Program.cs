using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.OData;
using Microsoft.EntityFrameworkCore;
using Microsoft.OData.Edm;
using Microsoft.OData.ModelBuilder;
using OData.Models;
using System;
using System.Diagnostics;


static IEdmModel GetEdmModel()
{
    ODataConventionModelBuilder builder = new();
    builder.EntitySet<UserActivityLog>("User_Activity_Logs");
    builder.EntitySet<PbisInteraction>("PBIS_Interactions");
    builder.EntitySet<PbisHistory>("PBIS_History");
    builder.EntitySet<NewsInteractionsC>("News_Interactions");
    builder.EntitySet<SocialFeed>("Social_Feed");
    Console.WriteLine("Inside GetEdmodel!");
    return builder.GetEdmModel();
}


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<db_sfsyncContext>();
builder.Services.AddControllers().AddOData(opt => opt.AddRouteComponents("v1", GetEdmModel()).Filter().Select().Expand().OrderBy().SetMaxTop(null).Count());
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() { Title = "OData", Version = "v1" });
});


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddAWSLambdaHosting(LambdaEventSource.HttpApi);

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
