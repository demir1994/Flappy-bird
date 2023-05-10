using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewindManager : MonoBehaviour
{
    public FlappyController player;

    public void RewindStart()
    {
        player.OnRewindStart();
    }

    public void RewindStop()
    {
        player.OnRewindStop();
    }
}


