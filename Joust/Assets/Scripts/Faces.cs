using UnityEngine;

public class Faces : MonoBehaviour
{
    public Sprite[] faces = new Sprite[10];
    private void Start()
    {

        int faceIndex = PlayerPrefs.GetInt("SelectedCharacterIndex");
        GetComponent<SpriteRenderer>().sprite = faces[faceIndex];

    }

}
