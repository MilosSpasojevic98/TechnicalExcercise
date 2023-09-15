var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<ICompanyRepository, CompanyRepository>();
var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.Run();