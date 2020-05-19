using System;
using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using UnityEngine.UI;

public class SelectionScript : Menu
{
    [SerializeField]
    public GameObject warningCanvas;

    [SerializeField] public GameObject redPlayer;
    [SerializeField] public GameObject bluePlayer;
    [SerializeField] public GameObject yellowPlayer;
    [SerializeField] public GameObject blackPlayer;

    [SerializeField] public Button startGameBtn;
    [SerializeField] public Button quitBtn;
    [SerializeField] public Button join1;
    [SerializeField] public Button join2;
    [SerializeField] public Button join3;
    [SerializeField] public Button join4;
    [SerializeField] public Button warningBtn;
    [SerializeField] public InputActionAsset actionAsset;

    private int noOfPlayers = 0;
    private bool inSelection = true;
    private bool WASDPresent;
    private bool arrowsPresent;
    
    private void Start()
    {
        WASDPresent = false;
        arrowsPresent = false;
        
        startGameBtn.onClick.AddListener(StartGame);
        quitBtn.onClick.AddListener(QuitGame);

        StartCoroutine(InputDeviceCheck());
    }

    private IEnumerator InputDeviceCheck()
    {

        while (inSelection && noOfPlayers < 4)
        {
            if (!WASDPresent && (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.A) || 
                                 Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.D)))
            {
                GameDirector.instance.AddDevice(Keyboard.current, actionAsset.FindActionMap("WASD"));
                WASDPresent = true;
                JoinPlayer();
            }

            if (!arrowsPresent && (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.LeftArrow) ||
                                   Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.RightArrow)))
            {
                GameDirector.instance.AddDevice(Keyboard.current, actionAsset.FindActionMap("Arrows"));
                arrowsPresent = true;
                JoinPlayer();
            }
            
            var controllers = Gamepad.all.Where(x =>
                x.wasUpdatedThisFrame && x.allControls.OfType<ButtonControl>().Any(b => b.wasPressedThisFrame));
            
            foreach (var device in controllers)
            {
                Debug.Log($"Pressed on controller: {device.device}");
                if(!GameDirector.instance.GetDeviceList().Exists(x => x.Item1.Equals(device.device)))
                {
                    GameDirector.instance.AddDevice(device, actionAsset.FindActionMap("Controller"));
                    JoinPlayer();
                }
            }

            yield return null;
        }
        
        
    }

    private void DisableWarning()
    {
        Debug.Log("Disable warning");
        warningCanvas.SetActive(false);
    }

    private void Update()
    {
        if (GameDirector.instance.GetRestarted())
        {
            join1.interactable = true;
            join1.GetComponentInChildren<Text>().text = "Join Player 1";
            join2.interactable = true;
            join2.GetComponentInChildren<Text>().text = "Join Player 2";
            join3.interactable = true;
            join3.GetComponentInChildren<Text>().text = "Join Player 3";
            join4.interactable = true;
            join4.GetComponentInChildren<Text>().text = "Join Player 4";
            
            noOfPlayers = 0;
            
            GameDirector.instance.SetRestarted(false);
        }

        InputDeviceCheck();
        
        if (Input.GetKey("escape"))
        {
            Application.Quit();
        }
    }

    private void JoinPlayer()
    {
        noOfPlayers++;
        Debug.Log($"Player {noOfPlayers} joined");

        switch (noOfPlayers)
        {
            case 1:
                ChangePlayerAnimation(redPlayer);
                ChangeButton(join1);
                break;
            case 2:
                ChangePlayerAnimation(bluePlayer);
                ChangeButton(join2);
                break;
            case 3:
                ChangePlayerAnimation(yellowPlayer);
                ChangeButton(join3);
                break;
            case 4:
                ChangePlayerAnimation(blackPlayer);
                ChangeButton(join4);
                break;
            default:
                Debug.LogError("Button name error on number of players");
                break;
        }
    }

    private void ChangeButton(Button button)
    {
        button.interactable = false;
        button.GetComponentInChildren<Text>().text = "Joined";
    }

    private static void ChangePlayerAnimation(GameObject player)
    {
        var characterMaterials = player.GetComponentsInChildren<Renderer>();
        foreach (var material in characterMaterials)
        {
            if (material.transform.CompareTag("PlayerEyes"))
            {
                material.material.SetColor("_EmmisionColor", new Color(191,25,25));
                material.material.SetTextureOffset("_MainTex", new Vector2(.66f,0));
            }
        }

        var animator = player.GetComponent<Animator>();
        animator.SetTrigger("angry");
    }

    private void StartGame()
    {
        Debug.Log("Start Game");
        if (noOfPlayers < 2)
        {
            warningCanvas.SetActive(true);
            warningBtn.onClick.AddListener(DisableWarning);
        }
        else
        {
            inSelection = false;
            GameDirector.instance.SetPlayers(noOfPlayers);
            GameDirector.instance.LoadLevel("GameScene");
        }
    }

    private static void QuitGame()
    {
        Debug.Log("Quit");
        Application.Quit();
    }
}
