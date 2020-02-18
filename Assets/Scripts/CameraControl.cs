using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class CameraControl : MonoBehaviour
{
    public GameObject[] playerPrefab;

//    [SerializeField]
//    Transform target;
    [SerializeField]
    GameObject map;
    
    private int noOfPlayers;

    public int noOfRows = 9;
    private GameObject crate;

    private Vector3 offset;
    private List<Tuple<int, int>> spawnPositions = new List<Tuple<int, int>>();
    private Vector3 finalPosition;
    private GameObject[] players;
    private Camera camera;
    private Vector3 moveVelocity;
    private float zoomSpeed;
    
    private void Awake()
    {
        camera = GetComponentInChildren<Camera>();
    }

    private void Start()
    {
        SpawnPlayers();
        players = GameObject.FindGameObjectsWithTag("Player");
//        offset = transform.position - target.position;
    }

    private void CreatePlayerSpawnPositions()
    {
        spawnPositions.Add(new Tuple<int, int>(-noOfRows, -noOfRows));
        spawnPositions.Add(new Tuple<int, int>(noOfRows, noOfRows));
        spawnPositions.Add(new Tuple<int, int>(-noOfRows, noOfRows));
        spawnPositions.Add(new Tuple<int, int>(noOfRows, -noOfRows));
    }

    private void SpawnPlayers()
    {
        noOfPlayers = PlayerPrefs.GetInt("PlayerCount");
        CreatePlayerSpawnPositions();
        for (var i = 0; i < noOfPlayers; i++)
        {
            GameObject player = playerPrefab[i];
            Debug.Log("i " + i + " // " + spawnPositions.Count);
            Vector3 position = new Vector3(spawnPositions[i].Item1, 0f, spawnPositions[i].Item2);
            Instantiate(player, position, Quaternion.Euler(new Vector3(0, 180, 0)), map.transform);
        }
    }

    private void FindAveragePosition()
    {
        var averagePos = new Vector3();
        var noOfTargets = 0;


        foreach (var player in players)
        {
            if(!player.gameObject.activeSelf)
                continue;

            averagePos += player.transform.position;
            noOfTargets++;
        }

        if (noOfTargets > 0)
            averagePos /= noOfTargets;

        averagePos.y = transform.position.y;

        finalPosition = averagePos;
    }

    private float FindRequiredSize()
    {
        var desiredLocalPos = transform.InverseTransformPoint(finalPosition);
        var size = 0f;

        foreach (var player in players)
        {
            if(!player.gameObject.activeSelf)
                continue;
            var targetLocalPos = transform.InverseTransformPoint(player.transform.position);
            var posToTarget = targetLocalPos - desiredLocalPos;

            size = Mathf.Max(size, Mathf.Abs(posToTarget.y));
            size = Mathf.Max(size, Mathf.Abs(posToTarget.x / camera.aspect));
            // edge buffer
            size += 0.5f;
            // size not too small
            size = Mathf.Max(size, 6f);
        }

        return size;
    }

    private void Move()
    {
        FindAveragePosition();

        transform.position = Vector3.SmoothDamp(transform.position, finalPosition, ref moveVelocity, 0.2f);
    }

    private void Zoom()
    {
        var requiredSize = FindRequiredSize();
        camera.orthographicSize = Mathf.SmoothDamp(camera.orthographicSize, requiredSize, ref zoomSpeed, 0.2f);
    }

    private void FixedUpdate()
    {
        Move();
        Zoom();
//        transform.position = Vector3.Lerp(transform.position, target.position + offset, Time.deltaTime * 5);
    }
}
