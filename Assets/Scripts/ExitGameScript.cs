using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ExitGameScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Button StartGameButton = GameObject.Find("StartGameButton").GetComponent<Button>();
        StartGameButton.onClick.AddListener(StartGame);

        Button QuitButton = GameObject.Find("QuitButton").GetComponent<Button>();
        QuitButton.onClick.AddListener(QuitGame);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey("escape"))
        {
            Application.Quit();
        }
    }

    void StartGame()
    {
        Debug.Log("Start Game");
        SceneManager.LoadScene("PlayerSelection");
    }

    void QuitGame()
    {
        Debug.Log("Quit");
        Application.Quit();
    }
}
