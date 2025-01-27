using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Static instance to allow access to this manager
    public static GameManager Instance;

    // Global variables

    public GameObject player;
    public DialogueManager dialogueManager;
    public int playerScore = 0;
    public int maxFuel = 100;
    public string playerName = "Player";

    //by adding a "_" to the string it separates them to create paragrphs
    public string text = "hello this is a test for dialogue";
    public string text2 = "hello this is a test for dialogue";

    // Game states
    public enum GameState { Menu, Playing, Paused, GameOver }
    public GameState currentState = GameState.Menu;

    private void Awake()
    {
        // Ensure only one instance exists (Singleton Pattern)
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Persist across scenes
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E)) 
        {
            Dialogue(text, "");
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            Dialogue(text2, "subDisplayText");
        }
        if (currentState == GameState.Playing)
        {
            Time.timeScale = 1;
            // playing logic
        }
        else if(currentState == GameState.Menu)
        {
            //show menu
        }
        else if (currentState == GameState.Paused)
        {
            Time.timeScale = 0;
            // show pause menu
        }
        else if (currentState == GameState.GameOver)
        {
            // do game over stuff
        }

        if (Input.GetKey(KeyCode.Escape))
        {
            if (currentState != GameState.Paused)
            {
                currentState = GameState.Paused;
            }
            else 
            { 
                currentState = GameState.Playing; 
            }

        }
    }



    public void Dialogue(string Text, string textBubble)
    {
        if (dialogueManager.NewDialogue(Text, textBubble) == true)
        {
            print("gave new dialogue");
        }
        else
        {
            print("Already a dialogue going on");
        }
    }
}