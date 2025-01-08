using TMPro;
using UnityEngine;

public class TextLite : MonoBehaviour
{
    private TMP_Text textmesh;
    private int textIndex = 0;
    private void Awake() {
        textmesh = GetComponentInChildren<TMP_Text>();
    }
    private void Start() {
        InvokeRepeating(nameof(AnimateText), .8f, .8f);
    }
    private void AnimateText() {

        textIndex++;
        if (textIndex == 2) {

            textmesh.text = "";
            textIndex = 0;
            
        } else {

            textmesh.text = "Choose Your Fighter!";

        }

    }

}