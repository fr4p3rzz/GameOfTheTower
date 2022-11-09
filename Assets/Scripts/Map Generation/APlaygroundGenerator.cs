using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class APlaygroundGenerator : MonoBehaviour
{
    [SerializeField]
    protected TilemapVisualizer tilemapVisualizer = null;
    [SerializeField]
    protected Vector2Int startPosition = Vector2Int.zero;

    public void GenerateLayout()
    {
        CleanMapData();
        tilemapVisualizer.Clear();
        RunProceduralGeneration();
    }

    protected abstract void RunProceduralGeneration();
    protected abstract void CleanMapData();
}
