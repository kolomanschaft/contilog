using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Contilog.Repositories;
using Contilog.Handlers.Topics;
using Contilog.Handlers.Categories;
using Contilog.Handlers.Posts;
using Contilog.Handlers.Decisions;
using Contilog.Handlers.Documents;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

// Register repositories
builder.Services.AddSingleton<ICategoryRepository, CategoryRepository>();
builder.Services.AddSingleton<ITopicRepository, TopicRepository>();
builder.Services.AddSingleton<IPostRepository, PostRepository>();
builder.Services.AddSingleton<IDecisionRepository, DecisionRepository>();
builder.Services.AddSingleton<IDocumentRepository, DocumentRepository>();

// Register topic handlers
builder.Services.AddScoped<IGetAllTopicsHandler, GetAllTopicsHandler>();
builder.Services.AddScoped<IGetTopicByIdHandler, GetTopicByIdHandler>();
builder.Services.AddScoped<ICreateTopicHandler, CreateTopicHandler>();
builder.Services.AddScoped<IArchiveTopicHandler, ArchiveTopicHandler>();
builder.Services.AddScoped<IDeleteTopicHandler, DeleteTopicHandler>();

// Register category handlers
builder.Services.AddScoped<IGetAllCategoriesHandler, GetAllCategoriesHandler>();
builder.Services.AddScoped<IGetCategoryByIdHandler, GetCategoryByIdHandler>();
builder.Services.AddScoped<ICreateCategoryHandler, CreateCategoryHandler>();
builder.Services.AddScoped<IUpdateCategoryHandler, UpdateCategoryHandler>();
builder.Services.AddScoped<IArchiveCategoryHandler, ArchiveCategoryHandler>();

// Register post handlers
builder.Services.AddScoped<IGetPostsByTopicIdHandler, GetPostsByTopicIdHandler>();
builder.Services.AddScoped<IGetPostByIdHandler, GetPostByIdHandler>();
builder.Services.AddScoped<IGetPostCountByTopicIdHandler, GetPostCountByTopicIdHandler>();
builder.Services.AddScoped<ICreatePostHandler, CreatePostHandler>();
builder.Services.AddScoped<IUpdatePostHandler, UpdatePostHandler>();
builder.Services.AddScoped<IDeletePostHandler, DeletePostHandler>();

// Register decision handlers
builder.Services.AddScoped<IGetDecisionsByPostIdHandler, GetDecisionsByPostIdHandler>();
builder.Services.AddScoped<IGetDecisionByIdHandler, GetDecisionByIdHandler>();
builder.Services.AddScoped<ICreateDecisionHandler, CreateDecisionHandler>();
builder.Services.AddScoped<IUpdateDecisionHandler, UpdateDecisionHandler>();
builder.Services.AddScoped<IDeleteDecisionHandler, DeleteDecisionHandler>();

// Register document handlers
builder.Services.AddScoped<IGetAllDocumentsHandler, GetAllDocumentsHandler>();
builder.Services.AddScoped<IGetDocumentByIdHandler, GetDocumentByIdHandler>();
builder.Services.AddScoped<ICreateDocumentHandler, CreateDocumentHandler>();
builder.Services.AddScoped<IDeleteDocumentHandler, DeleteDocumentHandler>();

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
