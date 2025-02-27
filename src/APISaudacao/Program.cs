using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapOpenApi("/openapi/{documentName}.yaml");
}

app.MapGet("/saudacao", () =>
{
    var currentDate = DateTime.Now;
    string mensagem = (currentDate.DayOfWeek == DayOfWeek.Saturday ||
        currentDate.DayOfWeek == DayOfWeek.Sunday) ? "Bom final de semana!" : "Seguimos na luta!";
    var saudacao = new Saudacao(currentDate, mensagem);
    app.Logger.LogInformation($"Gerado objeto: {JsonSerializer.Serialize(saudacao)}");
    return saudacao;
})
.WithName("GetSaudacao");

app.Run();

internal record Saudacao(DateTime DataAtual, string Mensagem);