using System.Collections;
using System.Collections.Generic;
using UnityEngine.Tilemaps;
using UnityEngine;

[CreateAssetMenu(fileName = "Cores", menuName = "Objectives/Core")]
public class CoreSO : ScriptableObject
{
    public TileBase _coreBase { get { return coreBase; } private set {coreBase = value; } }
    public int _health { get { return health; } private set {health = value; } }
    public int _shield { get { return shield; } private set {shield = value; } }
    public BoxCollider2D _collider { get { return collider; } private set {collider = value; } }


    [Header("Visual")]
    [SerializeField]
    private TileBase coreBase;

    [Header("Parameters")]
    [SerializeField]
    private int health;
    [SerializeField]
    private int shield;

    [Header("collider")]
    [SerializeField]
    private BoxCollider2D collider;
}
