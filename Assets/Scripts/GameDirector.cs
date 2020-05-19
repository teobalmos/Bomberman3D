using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Hardware;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameDirector : MonoBehaviour
{
    public static GameDirector instance;
    public GameObject[] playerPrefab;
    public int noOfRows = 9;

    private bool restarted = false;
    private const int maxPlayers = 4;
    private int noOfPlayers = 0;
    private List<Tuple<int, int>> spawnPositions = new List<Tuple<int, int>>();
    private List<Tuple<InputDevice, InputActionMap>> deviceList = new List<Tuple<InputDevice, InputActionMap>>();

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

    public void AddDevice(InputDevice device, InputActionMap actionMap)
    {
        deviceList.Add(new Tuple<InputDevice, InputActionMap>(device, actionMap));
    }

    public List<Tuple<InputDevice, InputActionMap>> GetDeviceList()
    {
        return deviceList;
    }
    
    public void SetPlayers(int players)
    {
        noOfPlayers = players;
    }

    public void SetRestarted(bool restart)
    {
        restarted = restart;
    }

    public bool GetRestarted()
    {
        return restarted;
    }

    private void CreatePlayerSpawnPositions()
    {
        if (spawnPositions.Count != maxPlayers)
        {
            spawnPositions.Clear();
            spawnPositions.Add(new Tuple<int, int>(-noOfRows, -noOfRows));
            spawnPositions.Add(new Tuple<int, int>(noOfRows, noOfRows));
            spawnPositions.Add(new Tuple<int, int>(-noOfRows, noOfRows));
            spawnPositions.Add(new Tuple<int, int>(noOfRows, -noOfRows));
        }
        
    }

    
    private void spawnPlayers()
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
            var newPlayer = PlayerInput.Instantiate(player, i, null, -1, pairWithDevice: deviceList[i].Item1);
            newPlayer.transform.position = position;
            newPlayer.transform.rotation = Quaternion.Euler(new Vector3(0, 180, 0));
            newPlayer.currentActionMap = deviceList[i].Item2;
            newPlayer.defaultActionMap = deviceList[i].Item2.name;
        }
    }

    public void LoadLevel(string level)
    {
        StartCoroutine(LoadLevelCoroutine(level));
    }

    private IEnumerator LoadLevelCoroutine(string level)
    {
        yield return SceneManager.LoadSceneAsync(level);
        MenuManager.instance.ChangeMenu<InGameMenu>();
        StartCoroutine(LevelGame());
    }

    private IEnumerator LevelGame()
    {
        var inGame = true;
        spawnPlayers();

        while (inGame)
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                inGame = false;
            }
            
            yield return null;
        }
        
        ExitGame(true);
    }

    private void ExitGame(bool restart)
    {
        restarted = restart;
        SceneManager.LoadScene("Background");
        MenuManager.instance.ChangeMenu<TitleMenuScript>();
    }
}
