using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public interface IPathFinder
{
    IEnumerable<Vector2> GetPath(Vector2 A, Vector2 C, IEnumerable<Edge> edges);
}