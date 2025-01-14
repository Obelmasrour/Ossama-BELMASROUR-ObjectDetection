using ObjectDetection;

namespace Ossama.BELMASROUR.ObjectDetection;

public record ObjectDetectionResult
{
    public byte[] ImageData { get; set; }
    public YoloOutput Box { get; set; } // Correction ici
}

public class BoundingBox
{
    public float Confidence { get; set; }
    public string Label { get; set; }
    public BoundingBoxDimensions Dimensions { get; set; }
}

public class BoundingBoxDimensions
{
    public float X { get; set; }
    public float Y { get; set; }
    public float Width { get; set; }
    public float Height { get; set; }
}