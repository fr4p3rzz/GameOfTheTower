using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DungeonGeneratorParameters_", menuName = "MapGeneration/DungeonGeneratorData")]

public class DungeonGeneratorSO : ScriptableObject
{
    public int _iterations { get { return iterations; } private set {iterations = value; } }
    public int _walkLength { get { return walkLength; } private set {walkLength = value; } }
    public int _numberOfSpawnPoints { get { return numberOfSpawnPoints; } private set {numberOfSpawnPoints = value; } }
    public int _numberOfCores { get { return numberOfCores; } private set {numberOfCores = value; } }
    public int _minDistanceFromCore { get { return minDistanceFromCore; } private set {minDistanceFromCore = value; } }
    public int _minDistanceFromEnemySpawn { get { return minDistanceFromEnemySpawn; } private set {minDistanceFromEnemySpawn = value; } }
    public bool _startRandomlyEachIteration { get { return startRandomlyEachIteration; } private set { startRandomlyEachIteration = value; } }

    [Header("Dungeon General Settings")]
    [SerializeField]
    private int iterations = 10;
    [SerializeField]
    private int  walkLength = 10;
    [SerializeField]
    private bool startRandomlyEachIteration = true;
    [Header("Points of interests")]
    [SerializeField]
    private int numberOfSpawnPoints = 1;
    [SerializeField]
    private int numberOfCores = 1;
    [SerializeField]
    private int minDistanceFromCore = 5;
    [SerializeField]
    private int minDistanceFromEnemySpawn = 10;

}
