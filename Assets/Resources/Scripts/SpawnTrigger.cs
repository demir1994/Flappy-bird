using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnTrigger : MonoBehaviour
{
    public int nextSpawnDistanceValue;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + nextSpawnDistanceValue);
            GameManager.instance.GenerateObjects();
            transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + nextSpawnDistanceValue);
            print("new world generated");
        }
    }
}
