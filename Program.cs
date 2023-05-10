using System.Text.Json.Serialization;
using PoliVagas.Core.Domain;
using PoliVagas.Core.Infrastructure.Persistence;
using PoliVagas.Core.Infrastructure.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var emailSettings = builder.Configuration.GetSection("EmailSettings").Get<EmailSettings>();
if (emailSettings != null) builder.Services.AddSingleton(emailSettings);

builder.Services.AddControllers()
                .AddJsonOptions(o => {
                    o.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                });
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c => {
    c.DescribeAllParametersInCamelCase();
});

builder.Services.AddTransient<IMailService, MailService>();
builder.Services.AddTransient<PoliVagas.Core.Application.CreateJob.CreateJobHandler, PoliVagas.Core.Application.CreateJob.CreateJobHandler>();
builder.Services.AddTransient<PoliVagas.Core.Application.CreateNotification.Handler, PoliVagas.Core.Application.CreateNotification.Handler>();
builder.Services.AddTransient<PoliVagas.Core.Application.NotifyNewJobs.Handler, PoliVagas.Core.Application.NotifyNewJobs.Handler>();
builder.Services.AddScoped<ICompanyRepository, SqlCompanyRepository>();
builder.Services.AddScoped<ICourseRepository, SqlCourseRepository>();
builder.Services.AddScoped<IIntegrationAgentRepository, SqlIntegrationAgentRepository>();
builder.Services.AddScoped<IJobRepository, SqlJobRepository>();
builder.Services.AddScoped<INotificationRepository, SqlNotificationRepository>();
builder.Services.AddScoped<SqlContext, SqlContext>();

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
