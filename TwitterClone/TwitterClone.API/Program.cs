using TwitterClone.API.Data;
using TwitterClone.Application.Interfaces;
using TwitterClone.Application.Services;
using TwitterClone.Domain.Entities;
using TwitterClone.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddOpenApi();

// Repository
builder.Services.AddSingleton<IUserRepository, UserRepository>();
builder.Services.AddSingleton<ITweetRepository,TweetRepository>();
builder.Services.AddSingleton<IFollowRepository, FollowRepository>();
builder.Services.AddSingleton<IBookmarkRepository, BookmarkRepository>();
builder.Services.AddSingleton<INotificationRepository, NotificationRepository>();
builder.Services.AddSingleton<IMessageRepository, MessageRepository>();
builder.Services.AddSingleton<IRetweetRepository, RetweetRepository>();
builder.Services.AddSingleton<ILikeRepository, LikeRepository>();

// Service
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ITweetService, TweetService>();
builder.Services.AddScoped<IFollowService, FollowService>();
builder.Services.AddScoped<IBookmarkService, BookmarkService>();
builder.Services.AddScoped<INotificationService, NotificationService>();
builder.Services.AddScoped<IMessageService, MessageService>();
builder.Services.AddScoped<IRetweetService, RetweetService>();
builder.Services.AddScoped<ILikeService, LikeService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();

