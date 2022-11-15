using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlaygroundGenerator : APlaygroundGenerator
{
    [SerializeField]
    protected DungeonGeneratorSO DungeonParameters;
    [SerializeField]
    protected MapInformationsSO mapInformations;


    void Start()
    {
        GenerateLayout();  
    }
    
    protected override void RunProceduralGeneration()
    {
        HashSet<Vector2Int> floorTilesPositions = createMapLayout(DungeonParameters, startPosition);   
        tilemapVisualizer.PaintFloorTiles(floorTilesPositions);
        WallGenerator.CreateWalls(floorTilesPositions, tilemapVisualizer);
    }

    protected HashSet<Vector2Int> createMapLayout(DungeonGeneratorSO DungeonParameters, Vector2Int position)
    {
        Vector2Int currentPosition = position;
        HashSet<Vector2Int> floorTilesPositions = new HashSet<Vector2Int>();

        for (int i = 0; i < DungeonParameters._iterations; i++)
        {
            var path = ProceduralGenerationAlgorithms.SimpleRandomWalk(currentPosition, DungeonParameters._walkLength);
            floorTilesPositions.UnionWith(path);

            if(DungeonParameters._startRandomlyEachIteration)
            {
                currentPosition = floorTilesPositions.ElementAt(Random.Range(0, floorTilesPositions.Count));
            }
        }

        return floorTilesPositions;
    }

    protected override void CleanMapData()
    {
        mapInformations._allGrounds.Clear();
        mapInformations._roomCenters.Clear();
        mapInformations._rooms.Clear();
        mapInformations._roomGrounds.Clear();
        mapInformations._coresPositions.Clear();
    }
}
