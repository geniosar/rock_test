using Microsoft.AspNetCore.HttpOverrides;
using RockTest.Core.Extensions;
using RockTest.Services.Extensions;
using RockTest.Games.Extensions;
using RockTest.Rand.Extensions;
using Serilog;
using Newtonsoft.Json.Converters;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((context, services, configuration) => configuration
	.ReadFrom.Configuration(context.Configuration)
	.ReadFrom.Services(services)
	.Enrich.FromLogContext());

builder.Services.AddControllers()
	.AddNewtonsoftJson(opts =>
		opts.SerializerSettings.Converters.Add(new StringEnumConverter()));

builder.Services.AddSwagger();
builder.Services.AddDistributedMemoryCache();

builder.Services.AddHealthChecks();
builder.Services.ConfigureCors();

builder.Services.AddRockApiService();

builder.Services.AddRockGameLogic();

builder.Services.AddRandomizer(builder.Configuration);

var app = builder.Build();

app.UseRouting();

app.UseHttpsRedirection();

app.UseForwardedHeaders(new ForwardedHeadersOptions
{
	ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
});

app.UseCors();
if (builder.Environment.IsDevelopment())
{
	app.AddSwaggerEndpoint();
}

app.AddExceptionMiddleware();
app.MapControllers();
app.MapHealthChecks("/health")
	.AllowAnonymous();

app.Run();
