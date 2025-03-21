using Application;
using Infrastruture;
using Infrastruture.Options;
using khanyisa.api.Middleware;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http.Features;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi

builder.Services.Configure<DatabaseSettings>(builder.Configuration.GetSection(nameof(DatabaseSettings)));
builder.Services.AddOpenApi();
builder.Services.AddApplication();
builder.Services.AddDatabase();
builder.Services.AddInfrastruture();

#pragma warning disable IDE0053 // Use expression body for lambda expression
builder.Services.AddProblemDetails(options =>
{
    options.CustomizeProblemDetails = context =>
    {
        context.ProblemDetails.Instance = $"{context.HttpContext.Request.Method} {context.HttpContext.Request.Path}";
        context.ProblemDetails.Extensions.TryAdd("requestId", context.HttpContext.TraceIdentifier);

        System.Diagnostics.Activity? activity =  context.HttpContext.Features.Get<IHttpActivityFeature>()?.Activity;
        context.ProblemDetails.Extensions.TryAdd("activityId", activity?.Id);
    };
});
#pragma warning restore IDE0053 // Use expression body for lambda expression

builder.Services.AddSingleton<IErrorHandler, ValidationErrorHandler>();
builder.Services.AddSingleton<IErrorHandler, SqlErrorHandler>();
builder.Services.AddSingleton<IErrorHandler, ProblemExceptionHandler>();
builder.Services.AddSingleton<IExceptionHandler, ErrorMiddleware>();
builder.Services.AddExceptionHandler<ErrorMiddleware>();

WebApplication app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();

    app.UseSwaggerUI(options => options.SwaggerEndpoint("/openapi/v1.json", "OpenAPI V1"));
}

app.UseExceptionHandler();
app.UseHttpsRedirection();
app.UseAuthorization();

app.MapControllers();

await app.RunAsync();
