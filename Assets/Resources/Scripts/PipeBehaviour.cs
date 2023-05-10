using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeBehaviour : MonoBehaviour
{
    public Transform pipeDown;
    public Transform pipeUp;

    public float minU;
    public float maxU;

    public float minD;
    public float maxD;

    public float minHeight;
    public float maxHeight;

    private void Awake()
    {
        transform.parent = transform.parent.parent.parent.parent.parent;

        float pipeUpValue = Random.Range(minU, maxU);
        float pipeDownValue = Random.Range(minD, maxD);
        float height = Random.Range(minHeight, maxHeight);

        pipeDown.position = new Vector3(transform.position.x, pipeDownValue, transform.position.z);
        pipeUp.position = new Vector3(transform.position.x, pipeUpValue, transform.position.z);
        transform.position = new Vector3(transform.position.x, height, transform.position.z);
    }



}
