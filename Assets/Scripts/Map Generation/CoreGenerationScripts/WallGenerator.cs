using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class WallGenerator
{
    public static void CreateWalls(HashSet<Vector2Int> floorTilesPositions, TilemapVisualizer tilemapVisualizer)
    {
        HashSet<Vector2Int> basicWallPositions = FindWallsInDirections(floorTilesPositions, Direction2D.cardinalDirectionsList);
        HashSet<Vector2Int> cornerWallPositions = FindWallsInDirections(floorTilesPositions, Direction2D.diagonalDirectionsList);

        CreateBasicWalls(tilemapVisualizer, basicWallPositions, floorTilesPositions);
        CreateCornerWalls(tilemapVisualizer, cornerWallPositions, floorTilesPositions);
    }

    private static void CreateBasicWalls(TilemapVisualizer tilemapVisualizer, HashSet<Vector2Int> basicWallPositions, HashSet<Vector2Int> floorPositions)
    {
        foreach (var position in basicWallPositions)
        {
            string neighboursBinaryValue = "";
            foreach (var direction in Direction2D.cardinalDirectionsList)
            {
                var neighbourPosition = position + direction;
                if(floorPositions.Contains(neighbourPosition))
                {
                    neighboursBinaryValue += "1";
                }
                else
                {
                    neighboursBinaryValue += "0";
                }
            }
            tilemapVisualizer.PaintSingleBasicWall(position, neighboursBinaryValue);
        }
    }

    private static void CreateCornerWalls(TilemapVisualizer tilemapVisualizer, HashSet<Vector2Int> cornerWallPositions, HashSet<Vector2Int> floorPositions)
    {
        foreach (var position in cornerWallPositions)
        {
            string neighboursBinaryValue = "";
            foreach (var direction in Direction2D.eightDirectionsList)
            {
                var neighbourPosition = position + direction;
                if(floorPositions.Contains(neighbourPosition))
                {
                    neighboursBinaryValue += "1";
                }
                else
                {
                    neighboursBinaryValue += "0";
                }
            }

            tilemapVisualizer.PaintSingleCornerWall(position, neighboursBinaryValue);
        }
    }

    public static HashSet<Vector2Int> FindWallsInDirections(HashSet<Vector2Int> floorTilesPositions, List<Vector2Int> directionList)
    {
        HashSet<Vector2Int> wallPositions = new HashSet<Vector2Int>();
        foreach (var position in floorTilesPositions)
        {
            foreach (var direction in directionList)
            {
                var neighbourPosition = position + direction;
                if(!floorTilesPositions.Contains(neighbourPosition))
                {
                    wallPositions.Add(neighbourPosition);
                }
            }
        }

        return wallPositions;
    }
}
