using UnityEngine;
using Random = System.Random;
using UnityEngine.SceneManagement;

public class JoustSceneManager : MonoBehaviour
{
    public void GoToNextOpponent() {

        PlayerPrefs.SetInt("firstOppositionIndex", PlayerPrefs.GetInt("secondOppositionIndex"));
        PlayerPrefs.SetInt("Level", 2);
        SceneManager.LoadScene("Lobby2");

    }
    Random random = new Random();

    // Public Variables Manually Filled on Unity
    public Sprite[] characters;
    public Sprite[] moveCharacters;
    public Sprite dust;
    public Sprite[] enemies;
    public Sprite[] moveEnemies;
    public Sprite[] faces;
    public Sprite[] thrustFor;
    public Sprite[] thrustBack;
    public Sprite[] dodgeFor;
    public Sprite[] dodgeBack;
    public Sprite[] player1powerFor;
    public Sprite[] player2powerFor;
    public Sprite[] player1powerBack;
    public Sprite[] player2powerBack;

    // Script Specific Variables
    int selectedCharacterIndex;
    int oppositionIndex;

    // GameObjects Adjusted On this Script
    SpriteRenderer oppositionSpriteRenderer;
    SpriteRenderer spriteRenderer;
    Rigidbody2D playerRigidbody2D;
    Rigidbody2D oppositionRigidbody2D;  
    GameObject playerCharacter;
    GameObject playerFace;
    GameObject oppositionFace;
    GameObject oppositionCharacter;

    [SerializeField] private healthManager healthManager;

    private void Awake() {

        selectedCharacterIndex = PlayerPrefs.GetInt("SelectedCharacterIndex");
        oppositionIndex = PlayerPrefs.GetInt("firstOppositionIndex");
        
    }

    private void Start() {

        Time.fixedDeltaTime = 0.0166f;
        InitializePlayer();
        InitializeOpposition(oppositionIndex);
        InitializeFaces();
        
    }

    // In case a health bar reaches 0:
    private void Update() {

        if (PlayerPrefs.GetInt("oppositionHealth") == 0 && PlayerPrefs.GetInt("Level") == 1) {

            GoToNextOpponent();
            PlayerPrefs.SetInt("Level", 2); // Next level

        } else if (PlayerPrefs.GetInt("oppositionHealth") == 0 && PlayerPrefs.GetInt("Level") == 2) {

            SceneManager.LoadScene("Victory"); // Finished Game

        } else if (PlayerPrefs.GetInt("playerHealth") == 0) {

            SceneManager.LoadScene("LosersLobby"); // Defeated

        } 

    }

    
    // Initialize the faces of the characters which float near health bar
    private void InitializeFaces() {

        float playerYcomp;
        float playerXcomp;
        if (selectedCharacterIndex==0) { // Specifications for exact coords

            playerYcomp = 3.53f;
            playerXcomp = -6.18f;

        } else if (selectedCharacterIndex==1){

            playerYcomp = 3.73f;
            playerXcomp = -5.95f;

        } else {

            playerYcomp = 4.1f;
            playerXcomp = -6f;

        }

        float oppositionYcomp;
        float oppositionXcomp;
        if (oppositionIndex==0) {

            oppositionYcomp = 3.53f;
            oppositionXcomp = 1.85f;

        } else if (oppositionIndex==1) {

            oppositionYcomp = 3.69f;
            oppositionXcomp = 2.13f;

        }else {

            oppositionYcomp = 4.14f;
            oppositionXcomp = 2.07f;

        }
        playerFace = new GameObject("PlayerFace");
        SpriteRenderer playerFaceSpriteRenderer = playerFace.AddComponent<SpriteRenderer>();
        playerFaceSpriteRenderer.sprite = faces[selectedCharacterIndex];
        playerFaceSpriteRenderer.sortingOrder = 13;
        playerFace.transform.position = new Vector3(playerXcomp, playerYcomp, 0);
        playerFace.transform.localScale = new Vector3(2.3f, 2.3f, 0);
        oppositionFace = new GameObject("OppositionFace");
        SpriteRenderer oppositionFace1SpriteRenderer = oppositionFace.AddComponent<SpriteRenderer>();
        oppositionFace1SpriteRenderer.sprite = faces[oppositionIndex];
        oppositionFace1SpriteRenderer.sortingOrder = 13;
        oppositionFace.transform.position = new Vector3(oppositionXcomp, oppositionYcomp, 0);
        oppositionFace.transform.localScale = new Vector3(2.3f, 2.3f, 0);

    }

    private void InitializePlayer() { // The player game object is manually created and adjusted through this function

        playerCharacter = new GameObject("PlayerCharacter");
        spriteRenderer = playerCharacter.AddComponent<SpriteRenderer>();
        playerRigidbody2D = playerCharacter.AddComponent<Rigidbody2D>();
        Anim anim = playerCharacter.AddComponent<Anim>();
        playerCharacter.AddComponent<PlayerRightController>();
        playerCharacter.AddComponent<PlayerLeftController>();
        playerCharacter.GetComponent<PlayerLeftController>().enabled = false;
        anim.anime[0] = characters[selectedCharacterIndex];
        anim.anime[1] = moveCharacters[selectedCharacterIndex];
        anim.anime[2] = thrustFor[selectedCharacterIndex];
        anim.anime[3] = dodgeFor[selectedCharacterIndex];
        if (selectedCharacterIndex == 1) {

            anim.anime[4] = player1powerFor[0];
            anim.anime[5] = player1powerFor[1];
            anim.anime[6] = player1powerFor[2];

        } else if (selectedCharacterIndex == 2) {

            anim.anime[4] = player2powerFor[0];
            anim.anime[5] = player2powerFor[1];
            anim.anime[6] = player2powerFor[2];

        }
        playerRigidbody2D.gravityScale = 0;
        spriteRenderer.sortingOrder = 4;
        spriteRenderer.sprite = characters[selectedCharacterIndex];
        playerCharacter.AddComponent<BoxCollider2D>();
        playerCharacter.transform.position = new Vector3(-6, -3, 0);
        playerCharacter.transform.localScale = new Vector3(2, 2, 0);
        PlayerPrefs.SetInt("playerHealth", 100);

    }

    private void InitializeOpposition(int index) { // The opposition game object is manually created and adjusted through this function

        if (oppositionCharacter == null) {

            oppositionCharacter = new GameObject($"OppositionCharacter");
            oppositionSpriteRenderer = oppositionCharacter.AddComponent<SpriteRenderer>();
            oppositionRigidbody2D = oppositionCharacter.AddComponent<Rigidbody2D>();
            oppositionCharacter.AddComponent<OpponentLeftControls>();
            oppositionCharacter.AddComponent<OpponentRightControls>();
            oppositionCharacter.GetComponent<OpponentRightControls>().enabled = false;
            Anim enemy1Anim = oppositionCharacter.AddComponent<Anim>();
            enemy1Anim.anime[0] = enemies[index];
            enemy1Anim.anime[1] = moveEnemies[index];
            enemy1Anim.anime[2] = thrustBack[index];
            enemy1Anim.anime[3] = dodgeBack[index];
            oppositionRigidbody2D.gravityScale = 0;
            oppositionSpriteRenderer.sortingOrder = 1;
            oppositionSpriteRenderer.sprite = enemies[index];
            oppositionCharacter.transform.position = new Vector3(6, -3, 0);
            oppositionCharacter.transform.localScale = new Vector3(2, 2, 0);
            PlayerPrefs.SetInt("oppositionHealth", 100);

        }

    }

    public void Left() { // When the player finishes the run right, it hits a trigger which calls this function to switch the player to the left side

        playerCharacter.transform.position = new Vector3(6, -3, 0);
        oppositionCharacter.transform.position = new Vector3(-6, -3, 0);
        playerCharacter.GetComponent<PlayerRightController>().enabled = false;
        playerCharacter.GetComponent<PlayerLeftController>().enabled = true;
        playerCharacter.GetComponent<PlayerLeftController>().held = 0f;
        playerCharacter.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        playerCharacter.GetComponent<PlayerLeftController>().opened = false;
        oppositionCharacter.GetComponent<OpponentRightControls>().enabled = false;
        oppositionCharacter.GetComponent<OpponentLeftControls>().enabled = false;
        oppositionCharacter.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        Anim anim = playerCharacter.GetComponent<Anim>();
        anim.anime[0] = enemies[selectedCharacterIndex];
        playerCharacter.GetComponent<SpriteRenderer>().sprite = enemies[selectedCharacterIndex];
        anim.anime[1] = moveEnemies[selectedCharacterIndex];
        anim.anime[2] = thrustBack[selectedCharacterIndex];
        anim.anime[3] = dodgeBack[selectedCharacterIndex];
        if (selectedCharacterIndex == 1) {

            anim.anime[4] = player1powerBack[0];
            anim.anime[5] = player1powerBack[1];
            anim.anime[6] = player1powerBack[2];

        } else if (selectedCharacterIndex == 2) {

            anim.anime[4] = player2powerBack[0];
            anim.anime[5] = player2powerBack[1];
            anim.anime[6] = player2powerBack[2];

        }
        playerCharacter.GetComponent<PlayerLeftController>().times = 0;
        playerCharacter.GetComponent<PlayerLeftController>().powerMoment = 0;
        playerCharacter.GetComponent<PlayerRightController>().powerMoment = 0;
        Anim enemy1Anim = oppositionCharacter.GetComponent<Anim>();
        enemy1Anim.anime[0] = characters[oppositionIndex];
        enemy1Anim.GetComponent<SpriteRenderer>().sprite = characters[oppositionIndex];
        enemy1Anim.anime[1] = moveCharacters[oppositionIndex];
        enemy1Anim.anime[2] = thrustFor[oppositionIndex];
        enemy1Anim.anime[3] = dodgeFor[oppositionIndex];
        healthManager.GetComponent<healthManager>().generatedJoust = 0;
        healthManager.GetComponent<healthManager>().contact = 0;

    }

    public void Right() { // When the player finishes the run left, it hits a trigger which calls this function to switch the player to the right side

        playerCharacter.transform.position = new Vector3(-6, -3, 0);
        oppositionCharacter.transform.position = new Vector3(6, -3, 0);
        playerCharacter.GetComponent<PlayerRightController>().enabled = true;
        playerCharacter.GetComponent<PlayerRightController>().held = 0f;
        playerCharacter.GetComponent<PlayerRightController>().opened = false;
        playerCharacter.GetComponent<PlayerLeftController>().enabled = false;
        playerCharacter.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        oppositionCharacter.GetComponent<OpponentRightControls>().enabled = false;
        oppositionCharacter.GetComponent<OpponentLeftControls>().enabled = false;
        oppositionCharacter.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        Anim anim = playerCharacter.GetComponent<Anim>();
        anim.anime[0] = characters[selectedCharacterIndex];
        playerCharacter.GetComponent<SpriteRenderer>().sprite = characters[selectedCharacterIndex];
        anim.anime[1] = moveCharacters[selectedCharacterIndex];
        anim.anime[2] = thrustFor[selectedCharacterIndex];
        anim.anime[3] = dodgeFor[selectedCharacterIndex];
        if (selectedCharacterIndex == 1) {

            anim.anime[4] = player1powerFor[0];
            anim.anime[5] = player1powerFor[1];
            anim.anime[6] = player1powerFor[2];

        } else if (selectedCharacterIndex == 2) {

            anim.anime[4] = player2powerFor[0];
            anim.anime[5] = player2powerFor[1];
            anim.anime[6] = player2powerFor[2];

        }
        playerCharacter.GetComponent<PlayerRightController>().times = 0;
        playerCharacter.GetComponent<PlayerLeftController>().powerMoment = 0;
        playerCharacter.GetComponent<PlayerRightController>().powerMoment = 0;
        Anim enemy1Anim = oppositionCharacter.GetComponent<Anim>();
        enemy1Anim.anime[0] = enemies[oppositionIndex];
        enemy1Anim.GetComponent<SpriteRenderer>().sprite = enemies[oppositionIndex];
        enemy1Anim.anime[1] = moveEnemies[oppositionIndex];
        enemy1Anim.anime[2] = thrustBack[oppositionIndex];
        enemy1Anim.anime[3] = dodgeBack[oppositionIndex];
        healthManager.GetComponent<healthManager>().generatedJoust = 0;
        healthManager.GetComponent<healthManager>().contact = 0;

    }

}
