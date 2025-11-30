using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CorpsesSpawner : MonoBehaviour
{
    public GameObject prefabToSpawn;
    public Transform objectToMove;
    public Transform targetPosition;

    public void SpawnAndMove()
    {
        if (prefabToSpawn != null)
        {
            Instantiate(prefabToSpawn, objectToMove.position, Quaternion.identity);

            if (objectToMove != null && targetPosition != null)
                objectToMove.position = targetPosition.position;
        }
    }

}
