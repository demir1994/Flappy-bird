using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pipe : MonoBehaviour
{
    public GameObject pipeParent;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Scrapper")
        {
            print("junk collecting");
            Destroy(pipeParent);
        }
    }
}
