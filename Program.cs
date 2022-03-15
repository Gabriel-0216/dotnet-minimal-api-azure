using dotnetApiCode;
using dotnetApiCode.Infra;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<PessoaRepository>();
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

// Configure the HTTP request pipeline.
    app.UseSwagger();
    app.UseSwaggerUI();


app.MapPost("/post", ([FromServices] PessoaRepository repo, Pessoa pessoa) => repo.Add(pessoa));
app.MapPost("/get", ([FromServices] PessoaRepository repo) => repo.Get());



app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
