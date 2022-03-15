using dotnet_minimal_api_azure.Models;
using dotnetApiCode;
using dotnetApiCode.Infra;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

ConfigureServices(builder);

var app = builder.Build();

ConfigureApplication(app);

app.Run();





void ConfigureServices(WebApplicationBuilder builder){

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<PessoaRepository>();
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

}
void ConfigureApplication(WebApplication app){
// Configure the HTTP request pipeline.
    app.UseSwagger();
    app.UseSwaggerUI();

app.MapPost("/post", async ([FromServices] PessoaRepository repo, PessoaDto pessoaDto) => {
    var pessoaModel = new Pessoa(){
        Id = 0,
        Name = pessoaDto.Name,
    };
    var inserted = await repo.Add(pessoaModel);
    return inserted is true ? Results.Ok() : Results.BadRequest();

});
app.MapGet("/getAll/skip/{skip:int}/take/{take:int}", async ([FromServices] PessoaRepository repo, [FromRoute] int skip, [FromRoute] int take) => await repo.GetAll(skip, take));
app.MapPut("/update/id/{id:int}", async ([FromServices] PessoaRepository repo, [FromRoute] int id, [FromBody] PessoaDto pessoa) => {
    var responseDto = new ResponseDto();
    var get = await repo.GetById(id);
    if(get is null) return Results.NotFound();

    get.Name = pessoa.Name;
    var updated = await repo.Update(get);
    if(updated){
        return Results.Ok();
    }
    return Results.BadRequest();
});

app.MapDelete("delete", async ([FromServices] PessoaRepository repo, [FromRoute] int id) => {
        var responseDto = new ResponseDto();
    var get = await repo.GetById(id);
        if(get is null) return Results.NotFound();

    var deleted = await repo.Delete(get);
    if(deleted) return Results.Ok();

    return Results.BadRequest();
});


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
}