using System;
using System.Collections.Generic;
using UnityEngine;

public struct Rectangle
{
    public Vector2 Min;
    public Vector2 Max;
}

public struct Edge
{
    public Rectangle First;
    public Rectangle Second;
    public Vector3 Start;
    public Vector3 End;
}

public class PathFinder : MonoBehaviour, IPathFinder
{
    public IEnumerable<Vector2> GetPath(Vector2 A, Vector2 C, IEnumerable<Edge> edges)
    {
        if (edges == null)
        {
            Debug.LogError("Edges cannot be null.");
            return new List<Vector2>();
        }

        var intersections = FindIntersections(edges);
        var graph = BuildGraph(intersections);
        var start = FindClosestPoint(A, intersections);
        var end = FindClosestPoint(C, intersections);
        var path = FindShortestPath(start, end, graph);

        return path;
    }

    public IEnumerable<Edge> GetEdges(List<Rectangle> rectangles)
    {
        List<Edge> edges = new List<Edge>();

        for (int i = 0; i < rectangles.Count; i++)
        {
            for (int j = i + 1; j < rectangles.Count; j++)
            {
                Edge edge;
                edge.First = rectangles[i];
                edge.Second = rectangles[j];
                edge.Start = new Vector3(rectangles[i].Min.x, rectangles[i].Min.y, 0f);
                edge.End = new Vector3(rectangles[j].Min.x, rectangles[j].Min.y, 0f);
                edges.Add(edge);
            }
        }

        return edges;
    }

    private List<Vector2> FindIntersections(IEnumerable<Edge> edges)
    {
        var intersections = new List<Vector2>();

        foreach (var edge in edges)
        {
            if (edge.First.Max.x < edge.Second.Min.x || edge.Second.Max.x < edge.First.Min.x)
            {
                continue;
            }

            if (edge.First.Max.y < edge.Second.Min.y || edge.Second.Max.y < edge.First.Min.y)
            {
                continue;
            }

            intersections.Add(FindIntersection(edge.First, edge.Second));
        }

        return intersections;
    }

    private Vector2 FindIntersection(Rectangle A, Rectangle B)
    {
        var intersection = new Vector2(
            Mathf.Max(A.Min.x, B.Min.x),
            Mathf.Max(A.Min.y, B.Min.y)
        );

        if (intersection.x > Mathf.Min(A.Max.x, B.Max.x))
        {
            intersection.x = Mathf.Min(A.Max.x, B.Max.x);
        }

        if (intersection.y > Mathf.Min(A.Max.y, B.Max.y))
        {
            intersection.y = Mathf.Min(A.Max.y, B.Max.y);
        }

        return intersection;
    }

    private Dictionary<Vector2, List<Vector2>> BuildGraph(List<Vector2> intersections)
    {
        var graph = new Dictionary<Vector2, List<Vector2>>();

        for (int i = 0; i < intersections.Count; i++)
        {
            for (int j = i + 1; j < intersections.Count; j++)
            {
                if (!IsEdgeBlocked(intersections[i], intersections[j], intersections))
                {
                    AddEdge(graph, intersections[i], intersections[j]);
                    AddEdge(graph, intersections[j], intersections[i]);
                }
            }
        }

        return graph;
    }

    private bool IsEdgeBlocked(Vector2 start, Vector2 end, List<Vector2> intersections)
    {
        var direction = (end - start).normalized;
        var distance = Vector2.Distance(start, end);

        foreach (var intersection in intersections)
        {
            if (Vector2.Distance(start, intersection) < distance && Vector2.Distance(end, intersection) < distance)
                {
                    var edge = intersection - start;
                    if (Vector2.Dot(direction, edge) > 0 && edge.magnitude < distance && edge.magnitude > 0.001f)
                    {
                        return true;
                    }
                }
        }

        return false;
    }

    private void AddEdge(Dictionary<Vector2, List<Vector2>> graph, Vector2 start, Vector2 end)
    {
        if (!graph.ContainsKey(start))
        {
            graph[start] = new List<Vector2>();
        }

        graph[start].Add(end);
    }

    private Vector2 FindClosestPoint(Vector2 point, List<Vector2> intersections)
    {
        Vector2 closestPoint = Vector2.zero;
        float closestDistance = Mathf.Infinity;

        foreach (var intersection in intersections)
        {
            float distance = Vector2.Distance(point, intersection);
            if (distance < closestDistance)
            {
                closestPoint = intersection;
                closestDistance = distance;
            }
        }

        return closestPoint;
    }

    private List<Vector2> FindShortestPath(Vector2 start, Vector2 end, Dictionary<Vector2, List<Vector2>> graph)
    {
        var queue = new Queue<Vector2>();
        var visited = new HashSet<Vector2>();
        var previous = new Dictionary<Vector2, Vector2>();

        queue.Enqueue(start);
        visited.Add(start);

        while (queue.Count > 0)
        {
            var current = queue.Dequeue();

            if (current == end)
            {
                return ReconstructPath(previous, start, end);
            }

            if (graph.ContainsKey(current))
            {
                foreach (var neighbor in graph[current])
                {
                    if (!visited.Contains(neighbor))
                    {
                        queue.Enqueue(neighbor);
                        visited.Add(neighbor);
                        previous[neighbor] = current;
                    }
                }
            }
        }

        return new List<Vector2>();
    }

    private List<Vector2> ReconstructPath(Dictionary<Vector2, Vector2> previous, Vector2 start, Vector2 end)
    {
        var path = new List<Vector2>();
        var current = end;

        while (current != start)
        {
            path.Add(current);
            current = previous[current];
        }

        path.Reverse();
        return path;
    }
}
