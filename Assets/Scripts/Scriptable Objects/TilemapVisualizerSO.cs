using System.Collections;
using System.Collections.Generic;
using UnityEngine.Tilemaps;
using UnityEngine;

[CreateAssetMenu(fileName = "DungeonGeneratorParameters_", menuName = "TilemapVisualizer/TilesToPaint")]

public class TilemapVisualizerSO : ScriptableObject
{
    public TileBase _floorTile { get { return floorTile; } private set {floorTile = value; } }
    public TileBase _wallTop { get { return wallTop; } private set {wallTop = value; } }
    public TileBase _wallSideRight { get { return wallSideRight; } private set {wallSideRight = value; } }
    public TileBase _wallSideLeft { get { return wallSideLeft; } private set {wallSideLeft = value; } }
    public TileBase _wallBottom { get { return wallBottom; } private set {wallBottom = value; } }
    public TileBase _wallFull { get { return wallFull; } private set {wallFull = value; } }
    public TileBase _wallInnerCornerDownLeft { get { return wallInnerCornerDownLeft; } private set {wallInnerCornerDownLeft = value; } }
    public TileBase _wallInnerCornerDownRight { get { return wallInnerCornerDownRight; } private set {wallInnerCornerDownRight = value; } }
    public TileBase _wallDiagonalCornerDownRight { get { return wallDiagonalCornerDownRight; } private set {wallDiagonalCornerDownRight = value; } }
    public TileBase _wallDiagonalCornerDownLeft { get { return wallDiagonalCornerDownLeft; } private set {wallDiagonalCornerDownLeft = value; } }
    public TileBase _wallDiagonalCornerUpRight { get { return wallDiagonalCornerUpRight; } private set {wallDiagonalCornerUpRight = value; } }
    public TileBase _wallDiagonalCornerUpLeft { get { return wallDiagonalCornerUpLeft; } private set {wallDiagonalCornerUpLeft = value; } }
    public TileBase _core { get { return core; } private set {core = value; } }
    public TileBase _enemySpawnPoint { get { return enemySpawnPoint; } private set {enemySpawnPoint = value; } }
    public TileBase _availableCell { get { return availableCell; } private set {availableCell = value; } }


    [Header("Tiles")]
    [SerializeField]
    private TileBase floorTile;
    [SerializeField]
    private TileBase wallTop;
    [SerializeField]
    private TileBase wallSideRight;
    [SerializeField]
    private TileBase wallSideLeft;
    [SerializeField]
    private TileBase wallBottom;
    [SerializeField]
    private TileBase wallFull;
    [SerializeField]
    private TileBase wallInnerCornerDownLeft;
    [SerializeField]
    private TileBase wallInnerCornerDownRight;
    [SerializeField]
    private TileBase wallDiagonalCornerDownRight;
    [SerializeField]
    private TileBase wallDiagonalCornerDownLeft;
    [SerializeField]
    private TileBase wallDiagonalCornerUpRight;
    [SerializeField]
    private TileBase wallDiagonalCornerUpLeft;
    [SerializeField]
    private TileBase core;
    [SerializeField]
    private TileBase enemySpawnPoint;
    [SerializeField]
    private TileBase availableCell;

}
