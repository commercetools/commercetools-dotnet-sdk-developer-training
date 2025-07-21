using commercetools.Base.Client;
using commercetools.Sdk.Api;
using commercetools.Sdk.Api.Client;
using commercetools.Sdk.Api.Client.RequestBuilders.Projects;
using commercetools.Sdk.Api.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using Training.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.UseCommercetoolsApi(builder.Configuration.GetSection("Commercetools"));

builder.Services.AddScoped<ByProjectKeyRequestBuilder>(sp =>
{
    var client = sp.GetRequiredService<IClient>();
    var config = sp.GetRequiredService<IConfiguration>();
    var projectKey = config["Commercetools:ProjectKey"]
                     ?? throw new InvalidOperationException("Missing ProjectKey in config.");

    return client.WithApi().WithProjectKey(projectKey);
});

builder.Services.AddControllers();
builder.Services.AddScoped<ICustomerService, CustomerService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseRouting();
app.UseAuthorization();
app.MapControllers();
app.Run();
