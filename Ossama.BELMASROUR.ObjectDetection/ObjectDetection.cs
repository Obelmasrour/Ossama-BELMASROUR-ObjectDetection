using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ObjectDetection; // Remplacez par le namespace réel si nécessaire

namespace Ossama.BELMASROUR.ObjectDetection;

// Classe ObjectDetection pour effectuer la détection d'objets
public class ObjectDetection
{
    // Méthode asynchrone pour détecter des objets dans plusieurs images
    public async Task<IList<ObjectDetectionResult>> DetectObjectInScenesAsync(IList<byte[]> imagesSceneData)
    {
        // Liste pour stocker les résultats de la détection
        var results = new List<ObjectDetectionResult>();

        // Initialisation de l'objet Yolo (IA pour la détection)
        var tinyYolo = new Yolo();

        // Traitement de chaque image en parallèle
        var tasks = imagesSceneData.Select(async imageData =>
        {
            // Détecte les objets dans l'image en utilisant le modèle
            var detection = tinyYolo.Detect(imageData);

            // Retourne un résultat pour cette image
            return new ObjectDetectionResult
            {
                ImageData = imageData,
                Box = detection
            };
        });

        // Attendre que toutes les tâches se terminent
        var detectionResults = await Task.WhenAll(tasks);

        // Ajouter tous les résultats à la liste finale
        results.AddRange(detectionResults);

        return results;
    }
}