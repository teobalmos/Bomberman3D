using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SelectionScript : MonoBehaviour
{
    [SerializeField]
    public GameObject warningCanvas;

    [SerializeField] public GameObject redPlayer;
    [SerializeField] public GameObject bluePlayer;
    [SerializeField] public GameObject yellowPlayer;
    [SerializeField] public GameObject blackPlayer;
    
    [HideInInspector] public int noOfPlayers = 0;

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

            switch (buttonName[4])
            {
                case '1':
                    ChangePlayerAnimation(redPlayer);
                    break;
                case '2':
                    ChangePlayerAnimation(bluePlayer);
                    break;
                case '3':
                    ChangePlayerAnimation(yellowPlayer);
                    break;
                case '4':
                    ChangePlayerAnimation(blackPlayer);
                    break;
            }
        }
    }

    void ChangePlayerAnimation(GameObject player)
    {
        Renderer[] characterMaterials = player.GetComponentsInChildren<Renderer>();
        for (int i = 0; i < characterMaterials.Length; i++)
        {
            if (characterMaterials[i].transform.CompareTag("PlayerEyes"))
            {
                characterMaterials[i].material.SetColor("_EmmisionColor", new Color(191,25,25));
                characterMaterials[i].material.SetTextureOffset("_MainTex", new Vector2(.66f,0));
            }
        }
        Animator animator = player.GetComponent<Animator>();
        animator.SetTrigger("angry");
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
            PlayerPrefs.SetInt("PlayerCount", noOfPlayers);
            SceneManager.LoadScene("GameScene");
        }
    }

    void QuitGame()
    {
        Debug.Log("Quit");
        Application.Quit();
    }
}
