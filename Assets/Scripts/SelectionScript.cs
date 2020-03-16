using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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

    private int noOfPlayers = 0;
    
    private void Start()
    {
        startGameBtn.onClick.AddListener(StartGame);
        quitBtn.onClick.AddListener(QuitGame);
        join1.onClick.AddListener(() => JoinPlayer(join1));
        join2.onClick.AddListener(() => JoinPlayer(join2));
        join3.onClick.AddListener(() => JoinPlayer(join3));
        join4.onClick.AddListener(() => JoinPlayer(join4));
    }
    
    private void DisableWarning()
    {
        Debug.Log("Disable warning");
        warningCanvas.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKey("escape"))
        {
            Application.Quit();
        }
    }

    private void JoinPlayer(Button button)
    {
        var buttonName = button.name;
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
                default:
                    Debug.LogError("Button name error on number of players");
                    break;
            }
        }
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
            GameDirector.instance.SetPlayers(noOfPlayers);
            SceneManager.LoadScene("GameScene");
        }
    }

    private static void QuitGame()
    {
        Debug.Log("Quit");
        Application.Quit();
    }
}
