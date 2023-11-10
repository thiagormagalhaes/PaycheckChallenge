using Microsoft.EntityFrameworkCore;
using PaycheckChallenge.Api.AutoMapper;
using PaycheckChallenge.Api.Configurations;
using PaycheckChallenge.Application.Commands.CreateEmployee;
using PaycheckChallenge.Infra.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Configuration.AddJsonFile("ef-configuration.json", false, true);

builder.Services.AddDbContext<PaycheckContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddDependencyInjectionConfiguration();

builder.Services.AddAutoMapper(typeof(AutoMapperConfig));

builder.Services.AddMediatR(cfg =>
     cfg.RegisterServicesFromAssembly(typeof(CreateEmployeeCommand).Assembly));

builder.Services.AddMvc(options => options.Filters.Add<NotificationFilter>());

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var dataContext = scope.ServiceProvider.GetRequiredService<PaycheckContext>();
    if (dataContext.Database.ProviderName.Equals("Microsoft.EntityFrameworkCore.SqlServer"))
        dataContext.Database.Migrate();
}

app.UseSwagger();
app.UseSwaggerUI();

app.UseAuthorization();

app.MapControllers();

app.Run();

public partial class Program { }