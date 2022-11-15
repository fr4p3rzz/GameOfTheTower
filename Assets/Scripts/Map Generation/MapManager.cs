using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.Tilemaps;
using UnityEngine;

public static class MapManager
{

    public static void GenerateWorld(TilemapVisualizer tilemapVisualizer, MapInformationsSO mapInformations, DungeonGeneratorSO DungeonParameters, bool enemiesSpawnsAtCenter)
    {
        SpawnCoresAtCenter(tilemapVisualizer, mapInformations, DungeonParameters._numberOfCores);
        WeightFloorTiles(tilemapVisualizer, mapInformations);

        if(enemiesSpawnsAtCenter)
        {
            SpawnEnemiesAtCenter(tilemapVisualizer, mapInformations, DungeonParameters._numberOfSpawnPoints);
        }
        else
        {
            SpawnEnemiesOnWeight(tilemapVisualizer, mapInformations, DungeonParameters._numberOfSpawnPoints, DungeonParameters._minDistanceFromCore, DungeonParameters._minDistanceFromEnemySpawn);
        }

        FillEntireRoom(tilemapVisualizer, mapInformations, mapInformations._roomGrounds[Random.Range(0, mapInformations._roomGrounds.Count)]);
    }

    // Randomly choose as many rooms as the cores of the level and spawn a core in it
    public static void SpawnCoresAtCenter(TilemapVisualizer tilemapVisualizer, MapInformationsSO mapInformations, int numberOfCores)
    {
        List<Vector2Int> availableCenters = new List<Vector2Int>(mapInformations._roomCenters);

        if(numberOfCores < mapInformations._roomCenters.Count)
        {
            for (int i = 0; i < numberOfCores; i++)
            {           
                int randomValue = Random.Range(0, availableCenters.Count);
                Vector2Int choosenCenter = availableCenters[randomValue];

                tilemapVisualizer.PaintSingleCoreTile(choosenCenter);
                mapInformations._coresPositions.Add(choosenCenter);

                availableCenters.Remove(choosenCenter);
            }
        }
        else
        {
            int randomValue = Random.Range(0, availableCenters.Count);
            Vector2Int choosenCenter = availableCenters[randomValue];

            tilemapVisualizer.PaintSingleCoreTile(choosenCenter);
            mapInformations._coresPositions.Add(choosenCenter);

            availableCenters.Remove(choosenCenter);
        }

    }

    // Randomly choose as many rooms as the enemy spawners of the level and spawn a core in it
    public static void SpawnEnemiesAtCenter(TilemapVisualizer tilemapVisualizer, MapInformationsSO mapInformations, int numberOfEnemySpawns)
    {
        List<Vector2Int> availableCenters = new List<Vector2Int>(mapInformations._roomCenters);
        foreach (var center in mapInformations._roomCenters)
        {
            if (mapInformations._coresPositions.Contains(center))
            {
                availableCenters.Remove(center);
            }
        }

        if(numberOfEnemySpawns < availableCenters.Count)
        {
            for (int i = 0; i < numberOfEnemySpawns; i++)
            {           
                int randomValue = Random.Range(0, availableCenters.Count);
                Vector2Int choosenCenter = availableCenters[randomValue];

                tilemapVisualizer.PaintSingleEnemySpawnTile(choosenCenter);
                mapInformations._enemySpawnsPositions.Add(choosenCenter);

                availableCenters.Remove(choosenCenter);
            }
        }
        else
        {
            int randomValue = Random.Range(0, availableCenters.Count);
            Vector2Int choosenCenter = availableCenters[randomValue];

            tilemapVisualizer.PaintSingleEnemySpawnTile(choosenCenter);
            mapInformations._enemySpawnsPositions.Add(choosenCenter);

            availableCenters.Remove(choosenCenter);
        }

    }

    // Create a spawn point based on the weight of the cells
    public static void SpawnEnemiesOnWeight(TilemapVisualizer tilemapVisualizer, MapInformationsSO mapInformations, int numberOfEnemySpawns, int minDistanceFromCore, int minDistanceFromEnemySpawn)
    {
        List<Vector2Int> availableGround = mapInformations._allGrounds;
        bool foundCompliantTile = false;

        // We cycle until we have the desired amount of spawners in the field
        while(mapInformations._enemySpawnsPositions.Count < numberOfEnemySpawns)
        {  
            int randomIndex = Random.Range(0, availableGround.Count);
            Vector2Int tile = availableGround[randomIndex]; 

            //Check if the selected tile: is not where's already a core, is not in a corridor and has enough weight (distance to core) to spawn
            if(!mapInformations._coresPositions.Contains(tile) && !mapInformations._corridorsGrounds.Contains(tile) && mapInformations._weightedCells[tile] >= minDistanceFromCore)
            {          
                // If yes, check if there are other enemy spawners in the field 
                if(mapInformations._enemySpawnsPositions.Count > 0)  
                {
                    // If yes, ensure that we spawn the next far enough from other spawners (BUG: we are currently NOT checking ALL the spawned enemySpawners)
                    foreach (var spawn in mapInformations._enemySpawnsPositions)
                    {      
                                      
                        if(Vector2.Distance(spawn, tile) < minDistanceFromEnemySpawn)
                        {                         
                            availableGround.Remove(tile);
                        } 
                        else
                        {
                            foundCompliantTile = true;  
                        }
                    } 
                    if(foundCompliantTile)
                    {
                        tilemapVisualizer.PaintSingleEnemySpawnTile(tile);
                        mapInformations._enemySpawnsPositions.Add(tile);
                        availableGround.Remove(tile);
                        foundCompliantTile = false;
                    }


                }  
                else
                {
                    tilemapVisualizer.PaintSingleEnemySpawnTile(tile);
                    mapInformations._enemySpawnsPositions.Add(tile);    
                                    
                }                 
            }
            else
            {
                availableGround.Remove(tile);
            }
            
            // If we weren't able to find enough compliant tiles, spawn at the center of available rooms
            if(availableGround.Count < 1 && mapInformations._enemySpawnsPositions.Count < numberOfEnemySpawns)
            {
                SpawnEnemiesAtCenter(tilemapVisualizer, mapInformations, numberOfEnemySpawns - mapInformations._enemySpawnsPositions.Count);
            } 
        }
  
    }

    // Fill one room with a tile
    public static void FillEntireRoom(TilemapVisualizer tilemapVisualizer, MapInformationsSO mapInformations, HashSet<Vector2Int> room)
    {
        HashSet<Vector2Int> selectedRoom = new HashSet<Vector2Int>(room);
        foreach (var core in mapInformations._coresPositions)
        {
            if(selectedRoom.Contains(core))
            {
                selectedRoom.Remove(core);
            }
        }  
        tilemapVisualizer.PaintAvailableTiles(selectedRoom);
    }

    // Create weights for every cell of the level (only walkables)
    public static void WeightFloorTiles(TilemapVisualizer tilemapVisualizer, MapInformationsSO mapInformations)
    {
        Vector2Int closestCore = mapInformations._coresPositions[0];

        foreach (var tile in mapInformations._allGrounds)
        {
            if(!mapInformations._coresPositions.Contains(tile))
            {
                foreach (var core in  mapInformations._coresPositions)
                {
                    if(Vector2.Distance(tile, core) < Vector2.Distance(tile, closestCore))
                    {
                        closestCore = core;
                    }
                }
                mapInformations._weightedCells[tile] = Vector2.Distance(tile, closestCore);
            }
            else
            {
                mapInformations._weightedCells[tile] = 0;
            } 
        }
    }


}
