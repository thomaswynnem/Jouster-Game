using UnityEngine;
using UnityEngine.SceneManagement;

public class LossClickDetector : MonoBehaviour
{

    void Update()
    {
        
        if (Input.GetMouseButtonDown(0)) {

            SceneManager.LoadScene("Player");
            
        }

    }

}


