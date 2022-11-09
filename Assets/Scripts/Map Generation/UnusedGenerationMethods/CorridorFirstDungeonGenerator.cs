using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CorridorFirstDungeonGenerator : PlaygroundGenerator
{
   [SerializeField]
   private int corridorLength = 14;
   [SerializeField]
   private int corridorCount = 5;
   [SerializeField]
   [Range(0.1f, 1)]
   private float roomPercent = 0.8f;

   protected override void RunProceduralGeneration()
   {
       corridorFirstGeneration();
   }

   private void corridorFirstGeneration()
   {
       HashSet<Vector2Int> floorPositions = new HashSet<Vector2Int>();
       HashSet<Vector2Int> potentialRoomPositions = new HashSet<Vector2Int>();

       CreateCorridors(floorPositions, potentialRoomPositions);

       HashSet<Vector2Int> roomPositions = CreateRooms(potentialRoomPositions);
       List<Vector2Int> deadEnds = FindAllDeadEnds(floorPositions);

       CreateRoomsAtDeadEnds(deadEnds, roomPositions);

       floorPositions.UnionWith(roomPositions); 

       tilemapVisualizer.PaintFloorTiles(floorPositions);
       WallGenerator.CreateWalls(floorPositions, tilemapVisualizer);
   }

   private List<Vector2Int> FindAllDeadEnds(HashSet<Vector2Int> floorPositions)
   {
       List<Vector2Int> deadEnds = new List<Vector2Int>();
       foreach (var position in floorPositions)
       {
           int neighboursCount = 0;
           foreach (var direction in Direction2D.cardinalDirectionsList)
           {
               if(floorPositions.Contains(position + direction))
               {
                   neighboursCount++;
               }
           }

           if(neighboursCount == 1)
           {
               deadEnds.Add(position);
           }
       }

       return deadEnds;
   }

   private void CreateRoomsAtDeadEnds(List<Vector2Int> deadEnds, HashSet<Vector2Int> roomPositions)
   {
       foreach (var position in deadEnds)
       {
          if(!roomPositions.Contains(position)) 
          {
               HashSet<Vector2Int> newRoom =  createMapLayout(DungeonParameters, position);
               roomPositions.UnionWith(newRoom);
          }
       }
   }

   private void SpawnCoresAtDeadEnds(List<Vector2Int> deadEnds, HashSet<Vector2Int> roomPositions)
   {
       foreach (var position in deadEnds)
       {
           if (!roomPositions.Contains(position))
           {
               HashSet<Vector2Int> newRoom = createMapLayout(DungeonParameters, position);
               roomPositions.UnionWith(newRoom);
           }
       }
   }

   private HashSet<Vector2Int> CreateRooms(HashSet<Vector2Int> potentialRoomPositions)
   {
       HashSet<Vector2Int> roomPositions = new HashSet<Vector2Int>();
       int roomToCreateCount = Mathf.RoundToInt(potentialRoomPositions.Count * roomPercent);

       List<Vector2Int> roomsToCreate = potentialRoomPositions.OrderBy(x => Guid.NewGuid()).Take(roomToCreateCount).ToList();

       foreach (var roomPosition in roomsToCreate)
       {
           HashSet<Vector2Int> roomFloor =  createMapLayout(DungeonParameters, roomPosition);
           roomPositions.UnionWith(roomFloor);
       }

       return roomPositions;
   }

   private void CreateCorridors(HashSet<Vector2Int> floorPositions,  HashSet<Vector2Int> potentialRoomPositions)
   {
       Vector2Int currentPosition = startPosition;
       potentialRoomPositions.Add(currentPosition);

       for (int i = 0; i < corridorCount; i++)
       {
           List<Vector2Int>  corridor = ProceduralGenerationAlgorithms.RandomWalkCorridor(currentPosition, corridorLength);
           currentPosition = corridor[corridor.Count - 1];
           potentialRoomPositions.Add(currentPosition);
           floorPositions.UnionWith(corridor);
       }
   }
}
