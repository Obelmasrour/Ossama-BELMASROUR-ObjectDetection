using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ossama.BELMASROUR.ObjectDetection
{
    public class ObjectDetection
    {
        public async Task<IList<ObjectDetectionResult>> DetectObjectInScenesAsync(IList<byte[]> imagesSceneData)
        {
            // Initialisation de l'objet Yolo
            var tinyYolo = new Yolo();

            // Traitement parallèle des images
            var tasks = imagesSceneData.Select(async imageData =>
            {
                // Détecte les objets
                var detections = tinyYolo.Detect(imageData);

                // Retourne le résultat sous forme simplifiée
                return new ObjectDetectionResult
                {
                    ImageData = imageData,
                    Box = detections // Directement le retour brut de Yolo si accepté
                };
            });

            // Attendre que tous les traitements soient terminés
            var detectionResults = await Task.WhenAll(tasks);

            // Retourner les résultats
            return detectionResults.ToList();
        }
    }
}