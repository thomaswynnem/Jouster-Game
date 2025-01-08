using UnityEngine;
using UnityEngine.SceneManagement;


public class ClickDetector : MonoBehaviour
{

    void Update()
    {
        // Check for a left mouse button click
        if (Input.GetMouseButtonDown(0)) {

            SceneManager.LoadScene("FightPit");
            
        }

    }

}
