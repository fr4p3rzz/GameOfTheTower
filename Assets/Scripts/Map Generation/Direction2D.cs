using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Direction2D
{
    public static List<Vector2Int> cardinalDirectionsList = new List<Vector2Int>
    {
        new Vector2Int(0, 1), // Up
        new Vector2Int(1, 0), // Right
        new Vector2Int(0, -1), // Down
        new Vector2Int(-1, 0), // left
    };

    public static List<Vector2Int> diagonalDirectionsList = new List<Vector2Int>
    {
        new Vector2Int(1, 1), // Up-Right
        new Vector2Int(1, -1), // Right-Down
        new Vector2Int(-1, -1), // Down-Left
        new Vector2Int(-1, 1), // left-Up
    };

    public static List<Vector2Int> eightDirectionsList = new List<Vector2Int>
    {
        // Clockwise direction ordered
        new Vector2Int(0, 1), // Up
        new Vector2Int(1, 1), // Up-Right
        new Vector2Int(1, 0), // Right
        new Vector2Int(1, -1), // Right-Down
        new Vector2Int(0, -1), // Down
        new Vector2Int(-1, -1), // Down-Left
        new Vector2Int(-1, 0), // left
        new Vector2Int(-1, 1), // left-Up
    };

    public static Vector2Int GetRandomCardinalDirection()
    {
        return cardinalDirectionsList[Random.Range(0, cardinalDirectionsList.Count)];
    }
}