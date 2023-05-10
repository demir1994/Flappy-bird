using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventHandler : MonoBehaviour
{
    public static EventHandler instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        // transform parenting processing
        gameObject.transform.parent = transform.parent.parent;
    }
}
