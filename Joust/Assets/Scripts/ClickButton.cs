using UnityEngine;
using UnityEngine.UI;
public class ClickButton : MonoBehaviour
{
    GameObject GoldMeter;
    GameObject PlayerCharacter;
    public Vector3 currentScale;
    private Button button;
    
    void Start()
    {

        GoldMeter = GameObject.Find("GoldMeter");
        button = GetComponent<Button>();
        button.onClick.AddListener(TheClickFunction);
        
    }

    void TheClickFunction () {

        if (PlayerCharacter == null)
        {
            PlayerCharacter = GameObject.Find("PlayerCharacter");
        }
        Debug.Log("Button Clicked");
        currentScale = GoldMeter.GetComponent<Transform>().localScale;
        currentScale.x = 0;
        GoldMeter.GetComponent<Transform>().localScale = currentScale;
        PlayerCharacter.GetComponent<PlayerRightController>().powerMoment = 1;
        PlayerCharacter.GetComponent<PlayerLeftController>().powerMoment = 1;
        gameObject.SetActive(false);

    }
}
