using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Contilog.Repositories;
using Contilog.Handlers.Topics;
using Contilog.Handlers.Categories;
using Contilog.Handlers.Posts;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

// Register repositories
builder.Services.AddSingleton<ICategoryRepository, CategoryRepository>();
builder.Services.AddSingleton<ITopicRepository, TopicRepository>();
builder.Services.AddSingleton<IPostRepository, PostRepository>();

// Register topic handlers
builder.Services.AddScoped<IGetAllTopicsHandler, GetAllTopicsHandler>();
builder.Services.AddScoped<IGetTopicByIdHandler, GetTopicByIdHandler>();
builder.Services.AddScoped<ICreateTopicHandler, CreateTopicHandler>();
builder.Services.AddScoped<IArchiveTopicHandler, ArchiveTopicHandler>();
builder.Services.AddScoped<IDeleteTopicHandler, DeleteTopicHandler>();

// Register category handlers
builder.Services.AddScoped<IGetAllCategoriesHandler, GetAllCategoriesHandler>();
builder.Services.AddScoped<IGetCategoryByIdHandler, GetCategoryByIdHandler>();

// Register post handlers
builder.Services.AddScoped<IGetPostsByTopicIdHandler, GetPostsByTopicIdHandler>();
builder.Services.AddScoped<IGetPostByIdHandler, GetPostByIdHandler>();
builder.Services.AddScoped<IGetPostCountByTopicIdHandler, GetPostCountByTopicIdHandler>();
builder.Services.AddScoped<ICreatePostHandler, CreatePostHandler>();
builder.Services.AddScoped<IUpdatePostHandler, UpdatePostHandler>();
builder.Services.AddScoped<IDeletePostHandler, DeletePostHandler>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
