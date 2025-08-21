using commercetools.Sdk.Api;
using commercetools.Sdk.ImportApi;
using Training.Options;

var builder = WebApplication.CreateBuilder(args);

builder.Services.UseCommercetoolsApi(builder.Configuration, Settings.SectionName);
builder.Services.UseCommercetoolsImportApi(builder.Configuration,Settings.ImportSectionName);

builder.Services.Configure<Settings>(
    builder.Configuration.GetSection(Settings.SectionName));


builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.DefaultIgnoreCondition =
            System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull;
    });
builder.Services.AddApplicationServices();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseRouting();
app.UseAuthorization();
app.UseDefaultFiles();
app.UseStaticFiles();
app.MapControllers();
app.Run();
