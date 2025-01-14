using System.Text.Json;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Ossama.BELMASROUR.ObjectDetection;

var builder = WebApplication.CreateBuilder(args);

// Ajouter Swagger pour la documentation
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Activer Swagger uniquement en développement
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Activer HTTPS
app.UseHttpsRedirection();

// Ajouter une route POST pour ObjectDetection
app.MapPost("/ObjectDetection", async ([FromForm] IFormFileCollection files) =>
    {
        // Vérifier si au moins un fichier est fourni
        if (files.Count < 1)
        {
            return Results.BadRequest("No file provided.");
        }

        // Charger l'image à partir du fichier
        using var sceneSourceStream = files[0].OpenReadStream();
        using var sceneMemoryStream = new MemoryStream();
        sceneSourceStream.CopyTo(sceneMemoryStream);
        var imageSceneData = sceneMemoryStream.ToArray();

        // Initialiser la détection d'objet
        var objectDetection = new Ossama.BELMASROUR.ObjectDetection.ObjectDetection();
        var detectionResults = await objectDetection.DetectObjectInScenesAsync(new List<byte[]> { imageSceneData });

        // Retourner les résultats au format JSON
        return Results.Json(detectionResults);
    })
    .WithName("ObjectDetection")
    .WithOpenApi();

app.Run();