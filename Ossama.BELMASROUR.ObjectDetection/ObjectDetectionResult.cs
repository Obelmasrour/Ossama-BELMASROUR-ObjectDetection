using ObjectDetection;

namespace Ossama.BELMASROUR.ObjectDetection;

// Classe représentant les résultats de la détection d'objets
public record ObjectDetectionResult
{
    public byte[] ImageData { get; set; } = Array.Empty<byte>(); // Assigne une valeur par défaut pour éviter les erreurs de null.
    public IList<BoundingBox> Box { get; set; } = new List<BoundingBox>(); // Assigne une liste vide par défaut.
}

// Classe représentant une boîte englobante pour un objet détecté
public class BoundingBox
{
    public float Confidence { get; set; } = 0f; // Confiance par défaut
    public string Label { get; set; } = string.Empty; // Étiquette par défaut
    public BoundingBoxDimensions Dimensions { get; set; } = new BoundingBoxDimensions(); // Dimensions par défaut
}

// Classe représentant les dimensions d'une boîte englobante
public class BoundingBoxDimensions
{
    public float X { get; set; } = 0f; // Coordonnée X par défaut
    public float Y { get; set; } = 0f; // Coordonnée Y par défaut
    public float Width { get; set; } = 0f; // Largeur par défaut
    public float Height { get; set; } = 0f; // Hauteur par défaut
}
