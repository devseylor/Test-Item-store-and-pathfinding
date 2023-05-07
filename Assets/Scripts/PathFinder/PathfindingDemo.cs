using System.Collections.Generic;
using UnityEngine;

public class PathfindingDemo : MonoBehaviour
{
    [SerializeField] private ShapeRenderer shapeRenderer;
    [SerializeField] private Vector2 start;
    [SerializeField] private Vector2 end;
    [SerializeField] private Vector2[] minRectangle;
    [SerializeField] private Vector2[] maxRectangle;

    private PathFinder pathFinder;

    private List<Rectangle> rectangles = new List<Rectangle>();

    private void Start()
    {
        pathFinder = GetComponent<PathFinder>();
        shapeRenderer = GetComponent<ShapeRenderer>();
        
        rectangles = CreateRectangles(minRectangle, maxRectangle);
        
        IEnumerable<Edge> edges = pathFinder.GetEdges(rectangles); 
        IEnumerable<Vector2> path = pathFinder.GetPath(start, end, edges);
        if (path != null)
        {
            shapeRenderer.DrawRectangle(rectangles);
            shapeRenderer.DrawPath(path);

            foreach (Vector2 point in path)
            {
                Debug.Log(point);
            }
        }
        else
        {
            Debug.Log("Path not found.");
        }
    }

    private List<Rectangle> CreateRectangles(Vector2[] minRectangle, Vector2[] maxRectangle)
    {
        List<Rectangle> rectangles = new List<Rectangle>();

        if (minRectangle.Length != maxRectangle.Length)
        {
            Debug.LogError("The number of minimum points doesn't match the number of maximum points.");
            return rectangles;
        }

        for (int i = 0; i < minRectangle.Length; i++)
        {
            Rectangle rectangle;
            rectangle.Min = minRectangle[i];
            rectangle.Max = maxRectangle[i];
            rectangles.Add(rectangle);
        }

        return rectangles;
    }
}
