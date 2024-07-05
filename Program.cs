var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options => options.AddDefaultPolicy(builder => builder.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin()));

var app = builder.Build();

app.MapGet("/stream", () =>
{
    static async IAsyncEnumerable<object> GetItems(int count)
    {
        for (int i = 0; i < count; i++)
        {
            await Task.Delay(500).ConfigureAwait(false);
            yield return new { Id = i };
        }
    }

    return Results.Ok(GetItems(12));
});

app.UseCors();

app.Run();