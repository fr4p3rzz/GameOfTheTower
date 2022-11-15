using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    void SpawnObject(GameObject objectToSpawn, Vector3 position)
    {
        objectToSpawn.transform.position = position;
        Instantiate(objectToSpawn);
    }
}
