using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using Ossama.BELMASROUR.ObjectDetection;

namespace Ossama.BELMASROUR.ObjectDetection.Console
{
    class Program
    {
        static async Task Main(string[] args)
        {
            // Vérifier qu'un argument est fourni (le chemin du dossier)
            if (args.Length < 1)
            {
                System.Console.WriteLine("Usage: dotnet run <PathToScenesDirectory>");
                return;
            }

            string scenesDirectory = args[0];

            // Vérifier que le dossier existe
            if (!Directory.Exists(scenesDirectory))
            {
                System.Console.WriteLine($"Directory not found: {scenesDirectory}");
                return;
            }

            // Charger toutes les images du dossier
            var imagesSceneData = new List<byte[]>();
            foreach (var imagePath in Directory.GetFiles(scenesDirectory))
            {
                var imageBytes = await File.ReadAllBytesAsync(imagePath);
                imagesSceneData.Add(imageBytes);
            }

            // Initialiser la détection d'objet
            var objectDetection = new ObjectDetection();
            var detectObjectInScenesResults = await objectDetection.DetectObjectInScenesAsync(imagesSceneData);

            // Afficher les résultats dans la console
            foreach (var objectDetectionResult in detectObjectInScenesResults)
            {
                System.Console.WriteLine($"Box: {JsonSerializer.Serialize(objectDetectionResult.Box)}");
            }
        }
    }
}