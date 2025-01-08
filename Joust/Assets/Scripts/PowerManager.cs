using UnityEngine;
public class PowerManager : MonoBehaviour
{
    private GameObject goldBar;
    private Vector3 currentScale;
    void Start()
    {

        goldBar = GameObject.Find("GoldMeter");

    }

    void OnTriggerEnter2D() {
        
        currentScale = goldBar.transform.localScale;
        currentScale.x += 0.8f;
        if (currentScale.x > 4f) {

            currentScale.x = 4f;

        }
        goldBar.transform.localScale = currentScale;

    }

}
