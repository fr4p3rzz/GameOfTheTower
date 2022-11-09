using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RoomFirstDungeonGenerator : PlaygroundGenerator
{
    [SerializeField]
    private int minRoomWidth = 4;
    [SerializeField]
    private int minRoomHeight = 4;
    [SerializeField]
    private int dungeonWidth = 20;
    [SerializeField]
    private int dungeonHeight = 20;
    [SerializeField]
    [Range(0, 4)]
    private int offset = 1;
    [SerializeField]
    private bool randomGeometryRooms = false;
    [SerializeField]
    private bool enemiesSpawnsAtCenter = true;


    

    protected override void RunProceduralGeneration()
    {
        cleanData();
        CreateRooms();
        MapManager.GenerateWorld(tilemapVisualizer, mapInformations, DungeonParameters, enemiesSpawnsAtCenter);
    }

    private void CreateRooms()
    {
        List<Vector2Int> roomCenters = new List<Vector2Int>();
        List<BoundsInt> roomsList = ProceduralGenerationAlgorithms.BinarySpacePartitioning(new BoundsInt((Vector3Int)startPosition, new Vector3Int(dungeonWidth, dungeonHeight, 0)), minRoomWidth, minRoomHeight);
        mapInformations._rooms = roomsList;
        HashSet<Vector2Int> floor = new HashSet<Vector2Int>();        

        if (randomGeometryRooms)
        {
            floor = CreateRoomsRandomly(roomsList);
        }
        else
        {
            floor = createSimpleRooms(roomsList);
        }

        
        foreach (var room in roomsList)
        {
            roomCenters.Add((Vector2Int)Vector3Int.RoundToInt(room.center));
        }

        HashSet<Vector2Int> corridors = ConnectRooms(roomCenters);
        floor.UnionWith(corridors);

        mapInformations._allGrounds = floor.ToList();

        tilemapVisualizer.PaintFloorTiles(floor);
        WallGenerator.CreateWalls(floor, tilemapVisualizer);

    }

    private HashSet<Vector2Int> CreateRoomsRandomly(List<BoundsInt> roomsList)
    {
        HashSet<Vector2Int> floor = new HashSet<Vector2Int>();
        for (int i = 0; i < roomsList.Count; i++)
        {
            BoundsInt roomBounds = roomsList[i];
            Vector2Int roomCenter = new Vector2Int(Mathf.RoundToInt(roomBounds.center.x), Mathf.RoundToInt(roomBounds.center.y));
            HashSet<Vector2Int> roomFloor = createMapLayout(DungeonParameters, roomCenter);
            HashSet<Vector2Int> roomWalkableGround = new HashSet<Vector2Int>();

            foreach (var position in roomFloor)
            {
                if(position.x >= (roomBounds.xMin + offset) && 
                position.x <= (roomBounds.xMax - offset) &&
                position.y >= roomBounds.yMin + offset &&
                position.y <= roomBounds.yMax - offset)
                {
                    floor.Add(position);
                    roomWalkableGround.Add(position);
                }
            }        
           SaveRoomData(roomCenter, roomWalkableGround); 
        }
        
        return floor;
    }

    private HashSet<Vector2Int> createSimpleRooms(List<BoundsInt> roomsList)
    {
        HashSet<Vector2Int> floor = new HashSet<Vector2Int>();
        foreach (var room in roomsList)
        {
            HashSet<Vector2Int> roomWalkableGround = new HashSet<Vector2Int>();
            for (int col = offset; col < room.size.x - offset; col++)         
            {
                for (int row = offset; row < room.size.y - offset; row++)         
                {
                    Vector2Int position = (Vector2Int)room.min + new Vector2Int(col, row);
                    floor.Add(position);
                    roomWalkableGround.Add(position);
                }
            }
            SaveRoomData(new Vector2Int(Mathf.RoundToInt(room.center.x), Mathf.RoundToInt(room.center.y)), roomWalkableGround);
        }

        return floor;
    }

    private HashSet<Vector2Int> ConnectRooms(List<Vector2Int> roomCenters)
    {
        HashSet<Vector2Int> corridors = new HashSet<Vector2Int>();
        Vector2Int currentRoomCenter = roomCenters[Random.Range(0, roomCenters.Count)];
        roomCenters.Remove(currentRoomCenter);

        while(roomCenters.Count > 0)
        {
            Vector2Int closest = FindClosestPointTo(currentRoomCenter, roomCenters);
            roomCenters.Remove(closest);
            HashSet<Vector2Int> newCorridor = CreateCorridor(currentRoomCenter, closest);
            currentRoomCenter = closest;
            corridors.UnionWith(newCorridor);
            mapInformations._corridorsGrounds.UnionWith(newCorridor);
        }

        return corridors;
    }

    private Vector2Int FindClosestPointTo(Vector2Int currentRoomCenter, List<Vector2Int> roomCenters)
    {
        Vector2Int closest = Vector2Int.zero;
        float distance = float.MaxValue;
        foreach(var position in roomCenters)
        {
            float currentDistance = Vector2.Distance(position, currentRoomCenter);
            if(currentDistance < distance)
            {
                distance = currentDistance; 
                closest = position;
            }
        }

        return closest;
    }

    private HashSet<Vector2Int> CreateCorridor(Vector2Int currentRoomCenter, Vector2Int destination)
    {
        HashSet<Vector2Int> corridor = new HashSet<Vector2Int>();
        Vector2Int position = currentRoomCenter;
        corridor.Add(position);

        while(position.y != destination.y)
        {
            if(destination.y > position.y)
            {
                position += Vector2Int.up;
            }
            else
            {
                position += Vector2Int.down;
            }

            corridor.Add(position);
        }

        while(position.x != destination.x)
        {
            if(destination.x > position.x)
            {
                position += Vector2Int.right;
            }
            else
            {
                position += Vector2Int.left;
            }

            corridor.Add(position);
        }

        return corridor;
    }

    private void SaveRoomData(Vector2Int roomCenter, HashSet<Vector2Int> roomFloor)
    {
        if(roomCenter != null)
            mapInformations._roomCenters.Add(roomCenter);
        if(roomFloor != null)
            mapInformations._roomGrounds.Add(roomFloor);
    }

    private void cleanData()
    {
        mapInformations._allGrounds.Clear();
        mapInformations._weightedCells.Clear();
        mapInformations._enemySpawnsPositions.Clear();
        mapInformations._coresPositions.Clear();
        mapInformations._roomGrounds.Clear();
        mapInformations._corridorsGrounds.Clear();
        mapInformations._roomCenters.Clear();
        mapInformations._rooms.Clear();
    }
}
