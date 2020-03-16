using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartLevel : MonoBehaviour
{
    private void Awake()
    {
        Debug.Log("Spawning players");
        GameDirector.instance.spawnPlayers();
    }
}
