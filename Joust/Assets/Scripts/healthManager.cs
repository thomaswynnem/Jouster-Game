using TMPro;
using UnityEngine;
using Random = System.Random;   

public class healthManager : MonoBehaviour
{

    Random random = new Random();
    GameObject Player;
    GameObject Opposition;
    GameObject BadGreenBar;
    GameObject GoodGreenBar;
    GameObject TMP;
    GameObject Jouster;
    public float generatedJoust = 0;
    public float avoidance = 0;
    public int contact = 0;
    private void Start() {

        Player = GameObject.Find("PlayerLoc");
        Opposition = GameObject.Find("EnemyLoc");
        BadGreenBar = GameObject.Find("BadGreenBar"); 
        GoodGreenBar = GameObject.Find("GoodGreenBar");
        TMP = GameObject.Find("TMP");

    }

   private void Update() {

    if (Jouster == null) {
        Jouster = GameObject.Find("PlayerCharacter");
    }

    float PlayerCords = Player.transform.position.x;
    float OppositionCords = Opposition.transform.position.x;
    float distance = PlayerCords - OppositionCords;

    if (Mathf.Abs(distance) <= .5 && generatedJoust == 0) {

        generatedJoust = random.Next(1,11) / 20f;
        int ifneg = random.Next(1, 3);
        if (ifneg == 1) {

            generatedJoust *= -1;

        }
        Debug.Log("Generated Joust: " + generatedJoust);

    }
    float epsilon = 0.05f;
    if (Mathf.Abs(distance - generatedJoust) < epsilon && contact == 0 && Jouster.transform.position.y < -2) {

        contact = Hit(Mathf.Abs(generatedJoust));

    }

    if (Input.GetKeyDown(KeyCode.Space) && contact == 0) {

        Debug.Log("Distance: " + distance);
        contact = Attack(Mathf.Abs(distance));

    }

    if (Input.GetKeyDown(KeyCode.F) && contact == 0) {

        avoidance = 3;

    }

    if (avoidance > 0) {

        avoidance -= Time.deltaTime;

    }

   }


   private float CalculateWidth(float health) {

       return (health / 100) * 4;

   }

   private float CalculateX(float width, int player) {

    if (player == 1) {

        return (-5.2f + 0.5f * width);

    } else {

        return (3 + 0.5f * width);

    }

}

   private int Attack(float distance) {

    if (distance <= .55f || (Jouster.GetComponent<PlayerRightController>().power == 3 && Jouster.GetComponent<PlayerRightController>().powerMoment == 1 && distance <= 1)) {

            int evilHealth = PlayerPrefs.GetInt("oppositionHealth");
            float perc = random.Next(1,5) * (1f/4f);
            if (Jouster.transform.localScale.y == 3) {
                perc *= 2;
            }
            if (distance > .6f) {
                distance = .6f;
            }
            evilHealth -= (int)(distance * 100 * perc);
            if (evilHealth <= 0) {

                evilHealth = 0;
                TMP.GetComponent<TextMeshPro>().color = new Color(0, 255, 0);
                TMP.GetComponent<TextMeshPro>().text = "Victor!";

            } 
            float width = CalculateWidth(evilHealth);
            float x = CalculateX(width, 0);
            BadGreenBar.transform.localScale = new Vector3(width, .2f, 1);
            BadGreenBar.transform.position = new Vector3(x, BadGreenBar.transform.position.y, BadGreenBar.transform.position.z);
            PlayerPrefs.SetInt("oppositionHealth", evilHealth);
            Debug.Log("Opposition Health: " + evilHealth);

            return 1;

        }

        return 0;
   }

   private int Hit(float distance)  {

    int playerHealth = PlayerPrefs.GetInt("playerHealth");
    float perc = random.Next(1,5) * (1f/4f);
    int avoided;
    random = new Random();
    if (avoidance > 0) { 

        avoided = random.Next(1,3);
        if (avoided == 1) {

            return 1;

        }

    }
    playerHealth -= (int)(distance * 100 * perc);
    if (playerHealth <= 0) {

        playerHealth = 0;
        TMP.GetComponent<TextMeshPro>().text = "Defeat!";
        TMP.GetComponent<TextMeshPro>().color = new Color(255, 0, 0);

    }
    float width = CalculateWidth(playerHealth);
    float x = CalculateX(width, 1);
    GoodGreenBar.transform.localScale = new Vector3(width, .2f, 1);
    GoodGreenBar.transform.position = new Vector3(x, GoodGreenBar.transform.position.y, GoodGreenBar.transform.position.z);
    PlayerPrefs.SetInt("playerHealth", playerHealth);
    Debug.Log("Player Health: " + playerHealth);

    return 1;

    }
    
}
