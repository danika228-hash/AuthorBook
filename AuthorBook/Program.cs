using AuthorBook.Data;
using AuthorBook.IRepository;
using AuthorBook.Repository;
using Microsoft.EntityFrameworkCore;
using AuthorBook.AutoMapperProfile;
using AuthorBook.Filters;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddDbContext<AuthorBookDataContext>(optioins => 
optioins.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IAuthorRepository, AuthorsRepository>();
builder.Services.AddScoped<IBookRepository, BooksRepository>();

builder.Services.AddAutoMapper(typeof(AutoMapperProfile));
builder.Services.AddControllers(op =>
{
    op.Filters.Add<CustomExceptionFilter>();
});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();
app.Run();