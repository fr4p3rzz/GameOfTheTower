using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MapInformations_", menuName = "MapGeneration/MapInformations")]

public class MapInformationsSO : ScriptableObject
{
    public List<Vector2Int> _roomCenters { get { return roomCenters; } private set { roomCenters = value; } }
    public List<BoundsInt> _rooms { get { return rooms; } set { rooms = value; } }
    public List<HashSet<Vector2Int>> _roomGrounds { get { return roomGrounds; } private set { roomGrounds = value; } }
    public List<Vector2Int> _allGrounds { get { return allGrounds; } set { allGrounds = value; } }
    public HashSet<Vector2Int> _corridorsGrounds { get { return corridorsGrounds; } set { corridorsGrounds = value; } }
    public List<Vector2Int> _coresPositions { get { return coresPositions; } private set { coresPositions = value; } }
    public List<Vector2Int> _enemySpawnsPositions { get { return enemySpawnsPositions; } private set { enemySpawnsPositions = value; } }
    public Dictionary<Vector2Int, float> _weightedCells { get { return weightedCells; } private set { weightedCells  = value; } }
    public int _dungeonWidth { get { return dungeonWidth; }  set { dungeonWidth  = value; } }
    public int _dungeonHeight { get { return dungeonHeight; }  set { dungeonHeight  = value; } }

    [Header("Planimetry informations")]
    [SerializeField]
    private List<BoundsInt> rooms = new List<BoundsInt>();
    [SerializeField]
    private List<Vector2Int> roomCenters = new List<Vector2Int>();
    [SerializeField]
    private List<HashSet<Vector2Int>> roomGrounds = new List<HashSet<Vector2Int>>();
    [SerializeField]
    private List<Vector2Int> allGrounds = new List<Vector2Int>();
    [SerializeField]
    private HashSet<Vector2Int> corridorsGrounds = new HashSet<Vector2Int>();
    [SerializeField]
    private List<Vector2Int> coresPositions = new List<Vector2Int>();
    [SerializeField]
    private List<Vector2Int> enemySpawnsPositions = new List<Vector2Int>();
    [SerializeField]
    private Dictionary<Vector2Int, float> weightedCells = new Dictionary<Vector2Int, float>();
    [SerializeField]
    private int dungeonWidth;
    [SerializeField]
    private int dungeonHeight;



}
