
using UnityEngine;
public class PowerUp : MonoBehaviour
{
    private GameObject goldMeter;
    private GameObject myButton;
    void Start()
    {

        myButton = GameObject.Find("Button");
        myButton.SetActive(false);
        goldMeter = GameObject.Find("GoldMeter");
        if (goldMeter == null) {

            Debug.Log("GoldMeter not found");

        }
        
    }

    // Update is called once per frame
    void Update()
    {

         if (Mathf.Approximately(goldMeter.GetComponent<Transform>().localScale.x, 4f)) {

            myButton.SetActive(true);

        } else {

            myButton.SetActive(false);

        }
        
    }

}
