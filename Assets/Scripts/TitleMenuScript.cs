using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TitleMenuScript : Menu
{
    [SerializeField] public Button startGameBtn;
    [SerializeField] public Button quitBtn;
    
    void Start()
    {
        startGameBtn.onClick.AddListener(StartGame);
        quitBtn.onClick.AddListener(QuitGame);
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
        MenuManager.instance.ChangeMenu<SelectionScript>();
    }

    void QuitGame()
    {
        Debug.Log("Quit");
        Application.Quit();
    }
}
