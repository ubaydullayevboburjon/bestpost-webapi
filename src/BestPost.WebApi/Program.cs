using BestPost.DataAccsess.Interfaces.Posts;
using BestPost.DataAccsess.Interfaces.Users;
using BestPost.DataAccsess.Repositories.Posts;
using BestPost.DataAccsess.Repositories.Users;
using BestPost.Service.Interfaces.Auth;
using BestPost.Service.Interfaces.Common;
using BestPost.Service.Interfaces.Notifications;
using BestPost.Service.Interfaces.Posts;
using BestPost.Service.Interfaces.Users;
using BestPost.Service.Services.Auth;
using BestPost.Service.Services.Common;
using BestPost.Service.Services.Notifications;
using BestPost.Service.Services.Posts;
using BestPost.Service.Services.Users;
using BestPost.WebApi.Configurations;
using BestPost.WebApi.Middlewares;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpContextAccessor();
builder.Services.AddMemoryCache();


builder.ConfigureJwtAuth();
builder.ConfigureSwaggerAuth();
builder.ConfigureCORSPolicy();

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IPostRepository, PostRepository>();

builder.Services.AddScoped<IIdentityService, IdentityService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IFileService, FileService>();
builder.Services.AddScoped<IPostService, PostService>();
builder.Services.AddScoped<IPaginator, Paginator>();

builder.Services.AddSingleton<IEmailSender, EmailSender>();



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("AllowAll");

app.UseCors("OnlySite");

app.UseStaticFiles();

app.UseMiddleware<ExceptionHandlerMiddleware>();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
