using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text.Json;
using System.Threading.Tasks;
using Xunit;
using Assert = Xunit.Assert;

namespace Ossama.BELMASROUR.ObjectDetection.Tests
{
    public class ObjectDetectionUnitTest
    {
        [Fact]
        public async Task ObjectShouldBeDetectedCorrectly()
        {
            var executingPath = GetExecutingPath();
            var imageScenesData = new List<byte[]>();

            // Charger toutes les images du dossier "Scenes"
            foreach (var imagePath in Directory.EnumerateFiles(Path.Combine(executingPath, "Scenes")))
            {
                var imageBytes = await File.ReadAllBytesAsync(imagePath);
                imageScenesData.Add(imageBytes);
            }

            // Appel de la méthode à tester
            var detectObjectInScenesResults = await new ObjectDetection().DetectObjectInScenesAsync(imageScenesData);

            // Logs des résultats réels pour inspection
            Console.WriteLine("Résultats réels :");
            Console.WriteLine(JsonSerializer.Serialize(detectObjectInScenesResults));

            // Vérification des résultats pour chaque image
            Assert.Equal(
                "[{\"Dimensions\":{\"X\":0,\"Y\":0,\"Height\":2,\"Width\":2},\"Label\":\"Car\",\"Confidence\":0.5}]",
                JsonSerializer.Serialize(detectObjectInScenesResults[0].Box)
            );

            Assert.Equal(
                "[{\"Dimensions\":{\"X\":0,\"Y\":0,\"Height\":2,\"Width\":2},\"Label\":\"Car\",\"Confidence\":0.5}]",
                JsonSerializer.Serialize(detectObjectInScenesResults[1].Box)
            );
        }

        // Méthode pour récupérer le chemin d'exécution
        private static string GetExecutingPath()
        {
            var executingAssemblyPath = Assembly.GetExecutingAssembly().Location;
            var executingPath = Path.GetDirectoryName(executingAssemblyPath);
            return executingPath;
        }
    }
}