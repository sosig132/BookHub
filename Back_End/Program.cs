using Back_End.Data;
using Back_End.Helper;
using Back_End.Helper.JwtUtils;
using Back_End.Helper.Middleware;
using Back_End.Repositories.BookDetailsRepository;
using Back_End.Repositories.BookRepository;
using Back_End.Repositories.CategoryRepository;
using Back_End.Repositories.ReviewRepository;
using Back_End.Repositories.UserRepository;
using Back_End.Services.BookDetailsService;
using Back_End.Services.BookService;
using Back_End.Services.CategoryService;
using Back_End.Services.ReviewService;
using Back_End.Services.UserService;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddJsonOptions(options =>
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve
); ;
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

builder.Services.AddDbContext<Context>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Default")));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"));

builder.Services.AddTransient<IBookRepository, BookRepository>();
builder.Services.AddTransient<IBookService, BookService>();
builder.Services.AddTransient<IJwtUtils, JwtUtils>();
builder.Services.AddTransient<IUserRepository, UserRepository>();
builder.Services.AddTransient<IUserService, UserService>();
builder.Services.AddTransient<ICategoryRepository, CategoryRepository>();
builder.Services.AddTransient<ICategoryService, CategoryService>();
builder.Services.AddTransient<IBookDetailsRepository, BookDetailsRepository>();
builder.Services.AddTransient<IBookDetailsService, BookDetailsService>();
builder.Services.AddTransient<IReviewService, ReviewService>();
builder.Services.AddTransient<IReviewRepository, ReviewRepository>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors(policy=>policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader()); 

app.UseAuthorization();
app.UseMiddleware<JwtMiddleware>();

app.MapControllers();

app.Run();
