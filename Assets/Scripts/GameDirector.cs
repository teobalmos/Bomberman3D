using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameDirector : MonoBehaviour
{
    public static GameDirector instance;
    public GameObject[] playerPrefab;
    public int noOfRows = 9;

    private int noOfPlayers = 0;
    private List<Tuple<int, int>> spawnPositions = new List<Tuple<int, int>>();

    private void OnEnable()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(instance);
        }
        else
        {
            Debug.LogError("Duplicate GameDirector instance");
        }
        
    }

    public void SetPlayers(int players)
    {
        noOfPlayers = players;
    }

    private void CreatePlayerSpawnPositions()
    {
        spawnPositions.Add(new Tuple<int, int>(-noOfRows, -noOfRows));
        spawnPositions.Add(new Tuple<int, int>(noOfRows, noOfRows));
        spawnPositions.Add(new Tuple<int, int>(-noOfRows, noOfRows));
        spawnPositions.Add(new Tuple<int, int>(noOfRows, -noOfRows));
    }

    
    public void spawnPlayers()
    {
        CreatePlayerSpawnPositions();
        if (noOfPlayers == 0)
        {
            Debug.Log("0 players");
        }
        for (var i = 0; i < noOfPlayers; i++)
        {
            var player = playerPrefab[i];
            Debug.Log("i " + i + " // " + spawnPositions.Count);
            var position = new Vector3(spawnPositions[i].Item1, 0f, spawnPositions[i].Item2);
            Instantiate(player, position, Quaternion.Euler(new Vector3(0, 180, 0)));
        }
    }
    

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            var unloadResult = SceneManager.UnloadSceneAsync("GameScene");

            SceneManager.LoadScene("MenuScene");
            MenuManager.instance.ChangeMenu<TitleMenuScript>();

        }
    }
}
