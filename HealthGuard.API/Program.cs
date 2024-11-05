using HealthGuard.API.Hubs;
using HealthGuard.API.Middleware;
using HealthGuard.Application;
using HealthGuard.DataAccess;
using HealthGuard.DataAccess.Identity;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// SignalR
builder.Services.AddSignalR();

var configuration = builder.Configuration;

// Layers
builder.Services.AddDataAccess(configuration);
builder.Services.AddApplication();

// Authentication
builder.Services.AddAuthorization();

builder.Services.ConfigureApplicationCookie(options =>
{
    options.Cookie.Name = "hg_session";
    options.SessionStore = new AppSessionStore();

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
builder.Services.AddProblemDetails();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.UseExceptionHandler(options => { });

app.MapControllers();
app.MapHub<NotificationHub>("/notificationHub");

app.Run();
