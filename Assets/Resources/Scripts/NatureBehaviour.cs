using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NatureBehaviour : MonoBehaviour
{
    public float minRot = 0;
    public float maxRot = 360;

    private void Awake()
    {
        float rotatingValue = Random.Range(minRot, maxRot);

        transform.eulerAngles = new Vector3(0, rotatingValue, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Scrapper")
        {
            print("junk cleaning...");
            Destroy(gameObject);
        }
    }
}
