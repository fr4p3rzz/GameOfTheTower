using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapVisualizer : MonoBehaviour
{

    [Header("Tilemaps")]
    [SerializeField]
    private Tilemap floorTilemap;
    [SerializeField]
    private Tilemap wallTilemap;
    [SerializeField]  
    private Tilemap coresTilemap;
    [SerializeField]
    private Tilemap spawnsTilemap;
    [SerializeField]
    private Tilemap interactablesTilemap;

    public Tilemap heatmap;

    [SerializeField]
    private TilemapVisualizerSO tilemapVisualizer;

    public void PaintFloorTiles(HashSet<Vector2Int> floorPositions)
    {
       PaintTiles(floorPositions, floorTilemap, tilemapVisualizer._floorTile); 
    }

    private void PaintSingleTile(Tilemap tilemap, TileBase tile, Vector2Int position)
    {
        var tilePosition = tilemap.WorldToCell((Vector3Int)position);
        tilemap.SetTile(tilePosition, tile);
    }

    private void PaintTiles(HashSet<Vector2Int> positions, Tilemap tilemap, TileBase tile)
    {
        foreach (var position in positions)
        {
            PaintSingleTile(tilemap, tile, position);
        }
    }

    internal void PaintAvailableTiles(HashSet<Vector2Int> positions)
    {
        foreach (var position in positions)
        {
            PaintSingleAvailableCellTile(position);
        } 
    }

    internal void PaintSingleGenericTile(TileBase tile, Vector2Int position)
    {
        PaintSingleTile(interactablesTilemap, tile, position);
    }

    internal void PaintSingleAvailableCellTile(Vector2Int position)
    {
        PaintSingleTile(interactablesTilemap, tilemapVisualizer._availableCell, position);
    }

    internal void PaintSingleEnemySpawnTile(Vector2Int position)
    {
        PaintSingleTile(spawnsTilemap, tilemapVisualizer._enemySpawnPoint, position);
    }

    internal void PaintSingleCoreTile(Vector2Int position)
    {
        PaintSingleTile(coresTilemap, tilemapVisualizer._core, position);
    }

    internal void PaintSingleBasicWall(Vector2Int position, string binaryType)
    {
        int typeAsInt = Convert.ToInt32(binaryType, 2);
        TileBase tile = null;

        if(WallTypesHelper.wallTop.Contains(typeAsInt))
        {
            tile = tilemapVisualizer._wallTop;
        }
        else if (WallTypesHelper.wallSideRight.Contains(typeAsInt))
        {
            tile = tilemapVisualizer._wallSideRight;
        }
        else if (WallTypesHelper.wallSideLeft.Contains(typeAsInt))
        {
            tile = tilemapVisualizer._wallSideLeft;
        }
        if (WallTypesHelper.wallBottom.Contains(typeAsInt))
        {
            tile = tilemapVisualizer._wallBottom;
        }
        if (WallTypesHelper.wallFull.Contains(typeAsInt))
        {
            tile = tilemapVisualizer._wallFull;
        }

        if (tile != null)
        {
            PaintSingleTile(wallTilemap, tile, position);
        }
    }

    internal void PaintSingleCornerWall(Vector2Int position, string binaryType)
    {
        int typeAsInt = Convert.ToInt32(binaryType, 2);
        TileBase tile = null;

        if (WallTypesHelper.wallInnerCornerDownLeft.Contains(typeAsInt))
        {
            tile = tilemapVisualizer._wallInnerCornerDownLeft;
        }
        else if (WallTypesHelper.wallInnerCornerDownRight.Contains(typeAsInt))
        {
            tile = tilemapVisualizer._wallInnerCornerDownRight;
        }
        else if (WallTypesHelper.wallInnerCornerDownRight.Contains(typeAsInt))
        {
            tile = tilemapVisualizer._wallInnerCornerDownRight;
        }
        else if (WallTypesHelper.wallDiagonalCornerDownRight.Contains(typeAsInt))
        {
            tile = tilemapVisualizer._wallDiagonalCornerDownRight;
        }
        else if (WallTypesHelper.wallDiagonalCornerDownLeft.Contains(typeAsInt))
        {
            tile = tilemapVisualizer._wallDiagonalCornerDownLeft;
        }
        else if (WallTypesHelper.wallDiagonalCornerUpRight.Contains(typeAsInt))
        {
            tile = tilemapVisualizer._wallDiagonalCornerUpRight;
        }
        else if (WallTypesHelper.wallDiagonalCornerUpLeft.Contains(typeAsInt))
        {
            tile = tilemapVisualizer._wallDiagonalCornerUpLeft;
        }
        else if (WallTypesHelper.wallBottomEightDirections.Contains(typeAsInt))
        {
            tile = tilemapVisualizer._wallBottom;
        }
        else if (WallTypesHelper.wallFullEightDirections.Contains(typeAsInt))
        {
            tile = tilemapVisualizer._wallFull;
        }

        if (tile != null)
        {
            PaintSingleTile(wallTilemap, tile, position);
        }
    }

    public void Clear()
    {
        floorTilemap.ClearAllTiles();
        wallTilemap.ClearAllTiles();
        coresTilemap.ClearAllTiles();
        spawnsTilemap.ClearAllTiles();
        interactablesTilemap.ClearAllTiles();
    }
}
