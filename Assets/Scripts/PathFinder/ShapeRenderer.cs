using System.Collections.Generic;
using UnityEngine;

public class ShapeRenderer : MonoBehaviour
{
    [SerializeField] private LineRenderer lineRenderer;
    [SerializeField] private Material rectangleMaterial;
    [SerializeField] private Material pathMaterial;

    public void DrawRectangle(List<Rectangle> rectangles)
    {
        lineRenderer.material = rectangleMaterial;
        lineRenderer.positionCount = 5 * rectangles.Count;
        lineRenderer.loop = false;

        List<Vector3> rectanglePoints = new List<Vector3>();

        foreach (Rectangle rectangle in rectangles)
        {
            rectanglePoints.Add(new Vector3(rectangle.Min.x, rectangle.Min.y, 0f));
            rectanglePoints.Add(new Vector3(rectangle.Min.x, rectangle.Max.y, 0f));
            rectanglePoints.Add(new Vector3(rectangle.Max.x, rectangle.Max.y, 0f));
            rectanglePoints.Add(new Vector3(rectangle.Max.x, rectangle.Min.y, 0f));
            rectanglePoints.Add(new Vector3(rectangle.Min.x, rectangle.Min.y, 0f));
            lineRenderer.SetPositions(rectanglePoints.ToArray());
            lineRenderer.positionCount = 5;
            Instantiate(lineRenderer);
            rectanglePoints.Clear();
        }
    }

    public void DrawPath(IEnumerable<Vector2> path)
    {
        lineRenderer.material = pathMaterial;
        lineRenderer.positionCount = 1;

        List<Vector3> pathPoints = new List<Vector3>();

        foreach (Vector2 point in path)
        {
            pathPoints.Add(new Vector3(point.x, point.y, 0f));
        }
        lineRenderer.positionCount = pathPoints.Count;
        lineRenderer.SetPositions(pathPoints.ToArray());
        Instantiate(lineRenderer);
    }
}
