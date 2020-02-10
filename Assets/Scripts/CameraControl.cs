using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CameraControl : MonoBehaviour
{
    public GameObject[] playerPrefab;

    [SerializeField]
    Transform target;
    [SerializeField]
    GameObject map;
    [SerializeField]
    int noOfPlayers = 2;

    public int noOfRows = 9;
    private GameObject crate;

    private Vector3 offset;
    private List<Tuple<int, int>> spawnPositions = new List<Tuple<int, int>>();

    void Start()
    {
        SpawnPlayers();
        //GenerateBlocks();
        offset = transform.position - target.position;
    }

    void CreatePlayerSpawnPositions()
    {
        spawnPositions.Add(new Tuple<int, int>(-noOfRows, -noOfRows));
        spawnPositions.Add(new Tuple<int, int>(noOfRows, noOfRows));
        spawnPositions.Add(new Tuple<int, int>(-noOfRows, noOfRows));
        spawnPositions.Add(new Tuple<int, int>(noOfRows, -noOfRows));
    }

    void SpawnPlayers()
    {
        CreatePlayerSpawnPositions();
        for (int i = 0; i < noOfPlayers; i++)
        {
            GameObject player = playerPrefab[i];
            Debug.Log("i " + i + " // " + spawnPositions.Count);
            Vector3 position = new Vector3(spawnPositions[i].Item1, 0f, spawnPositions[i].Item2);
            Instantiate(player, position, Quaternion.identity, map.transform);
            player.transform.Rotate(0f, 180f, 0f, Space.World);
        }
    }

    void FixedUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, target.position + offset, Time.deltaTime * 5);
    }
}
