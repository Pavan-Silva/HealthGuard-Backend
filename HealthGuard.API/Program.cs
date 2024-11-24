using FluentValidation;
using FluentValidation.AspNetCore;
using HealthGuard.API.Hubs;
using HealthGuard.API.Middleware;
using HealthGuard.Application;
using HealthGuard.Application.Validators;
using HealthGuard.DataAccess;

var builder = WebApplication.CreateBuilder(args);
var CORS_POLICY = "react_app";

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: CORS_POLICY,
        policy =>
        {
            policy.WithOrigins("http://localhost:3000")
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials();
        });
});

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// SignalR
builder.Services.AddSignalR();

// Fluent Validation
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddValidatorsFromAssemblyContaining<UserRegistrationValidator>();

var configuration = builder.Configuration;

// Layers
builder.Services.AddDataAccess(configuration);
builder.Services.AddApplication();

// Authentication
builder.Services.AddAuthorization();

builder.Services.ConfigureApplicationCookie(options =>
{
    options.Cookie.Name = "hg_session";

    //Disabled on development environment
    //options.SessionStore = new AppSessionStore();

    options.Events.OnRedirectToAccessDenied = context =>
    {
        context.Response.StatusCode = StatusCodes.Status403Forbidden;
        return Task.CompletedTask;
    };

    options.Events.OnRedirectToLogin = context =>
    {
        context.Response.StatusCode = StatusCodes.Status401Unauthorized;
        return Task.CompletedTask;
    };
});

// Urls to lowercase
builder.Services.Configure<RouteOptions>(options =>
options.LowercaseUrls = true
);

// Exception Handling
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(CORS_POLICY);
app.UseExceptionHandler(_ => { });
app.UseHttpsRedirection();
app.UseAuthorization();

app.MapControllers();
app.MapHub<NotificationHub>("/notificationHub");

app.Run();
