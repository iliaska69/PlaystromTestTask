using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class LevelSpawner : MonoBehaviour
{
    [SerializeField] private List<GameObject> objectsList;
    [SerializeField] private Vector3 spawnPoint;
    [SerializeField] private Transform parent;

    private void OnTriggerExit(Collider other)
    {
        SpawnObject(GetRandomGameObject());
    }

    private GameObject GetRandomGameObject()
    {
        var randomId = Random.Range(0, objectsList.Count);
        return objectsList[randomId];
    }

    private void SpawnObject(GameObject objectToSpawn)
    {
        Instantiate(objectToSpawn, spawnPoint, Quaternion.identity, parent);
    }
}
