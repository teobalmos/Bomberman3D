using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SelectionScript : MonoBehaviour
{
    [SerializeField]
    public GameObject warningCanvas;

    [HideInInspector]
    public int noOfPlayers = 0;
    
    void Start()
    {
        Button startGameButton = GameObject.Find("StartGameButton").GetComponent<Button>();
        startGameButton.onClick.AddListener(StartGame);

        Button quitButton = GameObject.Find("QuitButton").GetComponent<Button>();
        quitButton.onClick.AddListener(QuitGame);

        Button join1 = GameObject.Find("Join1").GetComponent<Button>();
        join1.onClick.AddListener(() => JoinPlayer(join1));

        Button join2 = GameObject.Find("Join2").GetComponent<Button>();
        join2.onClick.AddListener(() => JoinPlayer(join2));

        Button join3 = GameObject.Find("Join3").GetComponent<Button>();
        join3.onClick.AddListener(() => JoinPlayer(join3));

        Button join4 = GameObject.Find("Join4").GetComponent<Button>();
        join4.onClick.AddListener(() => JoinPlayer(join4));

        GameObject playerRed = GameObject.Find("PlayerRed");
        playerRed.GetComponent<Animator>().SetTrigger("happy");
        //playerRed.GetComponent<Animation>().Play();
        //ChangeEyeOffset(EyePosition.happy);
        //ChangeAnimatorIdle("happy");

    }
    
    private void DisableWarning()
    {
        Debug.Log("Disable warning");
        warningCanvas.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKey("escape"))
        {
            Application.Quit();
        }
    }

    void JoinPlayer(Button button)
    {
        string buttonName = button.name;
        if(buttonName[4] - noOfPlayers == 49)
        {
            Debug.Log("pressed button: " + button);
            button.interactable = false;
            button.GetComponentInChildren<Text>().text = "Joined";
            noOfPlayers++;
        }
    }

    void StartGame()
    {
        Debug.Log("Start Game");
        if (noOfPlayers < 2)
        {
            warningCanvas.SetActive(true);
            Button WarningOK = GameObject.Find("WarningButton").GetComponent<Button>();
            WarningOK.onClick.AddListener(DisableWarning);
        }
        else
        {
            SceneManager.LoadScene("GameScene");
        }
    }

    void QuitGame()
    {
        Debug.Log("Quit");
        Application.Quit();
    }
}
