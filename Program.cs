using System.Diagnostics;
using System.Text.Json.Serialization;
using PoliVagas.Core.Domain;
using PoliVagas.Core.Infrastructure.Background;
using PoliVagas.Core.Infrastructure.Persistence;
using PoliVagas.Core.Infrastructure.Services;

var builder = WebApplication.CreateBuilder(args);

// Cors settings
var corsPolicy = "corsPolicy";
builder.Services.AddCors(o => o.AddPolicy(corsPolicy, policy => {
    policy.AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod()
            .WithExposedHeaders();
    })
);

builder.Services.AddControllers()
                .AddJsonOptions(o => {
                    o.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                });

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(o => {
    o.SwaggerDoc("v0", new Microsoft.OpenApi.Models.OpenApiInfo {
        Title = "PoliVagas",
        Version = "0.1.0",
        Description = "PoliVagas REST API",
        // TermsOfService = new Uri("http://tempuri.org/terms"),
        // Contact = new OpenApiContact
        // {
        //     Name = "Joe Developer",
        //     Email = "joe.developer@tempuri.org"
        // },
        // License = new OpenApiLicense
        // {
        //     Name = "Apache 2.0",
        //     Url = new Uri("http://www.apache.org/licenses/LICENSE-2.0.html")
        // }
    });

    o.DescribeAllParametersInCamelCase();

    var xmlFilename = $"{System.Reflection.Assembly.GetExecutingAssembly().GetName().Name}.xml";
    o.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
});

// Dependency Injection
// Settings
// var emailSettings = builder.Configuration.GetSection("EmailSettings").Get<EmailSettings>();

if (!int.TryParse(Environment.GetEnvironmentVariable("MAIL_PORT"), out var port)) {
    port = 0;
};
var emailSettings = new EmailSettings() {
    Mail = Environment.GetEnvironmentVariable("MAIL_FROM") ?? "",
    DisplayName = Environment.GetEnvironmentVariable("MAIL_NAME") ?? "",
    Username = Environment.GetEnvironmentVariable("MAIL_USERNAME") ?? "",
    Password = Environment.GetEnvironmentVariable("MAIL_PASSWORD") ?? "",
    Host = Environment.GetEnvironmentVariable("MAIL_HOST") ?? "",
    Port = port,
};
if (emailSettings != null) builder.Services.AddSingleton(emailSettings);
// Handlers
builder.Services.AddTransient<PoliVagas.Core.Application.RegisterJob.RegisterJobHandler>();
builder.Services.AddTransient<PoliVagas.Core.Application.FindJob.FindJobHandler>();
builder.Services.AddTransient<PoliVagas.Core.Application.SearchJobs.SearchJobsHandler>();
builder.Services.AddTransient<PoliVagas.Core.Application.Subscribe.SubscribeHandler>();
builder.Services.AddTransient<PoliVagas.Core.Application.NotifyNewJobs.NotifyNewJobsHandler>();
builder.Services.AddTransient<PoliVagas.Core.Application.ListCompanies.ListCompaniesHandler>();
builder.Services.AddTransient<PoliVagas.Core.Application.ListCourses.ListCoursesHandler>();
// Services
builder.Services.AddTransient<IMailService, MailService>();
// Repositories
builder.Services.AddScoped<ICompanyRepository, SqlCompanyRepository>();
builder.Services.AddScoped<ICourseRepository, SqlCourseRepository>();
builder.Services.AddScoped<IIntegrationAgentRepository, SqlIntegrationAgentRepository>();
builder.Services.AddScoped<IJobRepository, SqlJobRepository>();
builder.Services.AddScoped<INotificationRepository, SqlNotificationRepository>();
builder.Services.AddScoped<SqlContext>();
// Background
builder.Services.AddSingleton<NotificationService>();
builder.Services.AddHostedService(p => p.GetRequiredService<NotificationService>());

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment()) {
    app.UseCors(corsPolicy);
} else {
    app.UseHttpsRedirection();
    app.UseCors(corsPolicy);
}

app.UseSwagger();
app.UseSwaggerUI(c => {
    c.SwaggerEndpoint("/swagger/v0/swagger.json", "v0 Docs");
});

app.UseAuthorization();

app.MapControllers();

app.Run();
