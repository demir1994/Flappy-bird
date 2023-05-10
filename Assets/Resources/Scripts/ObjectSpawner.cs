using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    public GameObject objToSpawn;

    public bool isPipe;

    private void Awake()
    {
        if (!isPipe)
        {
            Spawn();
        } else
        {
            SpawnPipes();
        }
    }

    /// <summary>
    /// Spawn pipes
    /// </summary>
    public void SpawnPipes()
    {
        GameObject _SpawningObject = GameObject.Instantiate(objToSpawn, transform);
    }

    /// <summary>
    /// Spawn entity objects
    /// </summary>
    public void Spawn()
    {
        Bounds bounds = GetComponent<Collider>().bounds;
        float offsetX = Random.Range(-bounds.extents.x, bounds.extents.x);
        float offsetY = Random.Range(-bounds.extents.y, bounds.extents.y);
        float offsetZ = Random.Range(-bounds.extents.z, bounds.extents.z);

        GameObject _SpawningObject = GameObject.Instantiate(objToSpawn);
        _SpawningObject.transform.position = bounds.center + new Vector3(offsetX, offsetY, offsetZ);
    }
}
