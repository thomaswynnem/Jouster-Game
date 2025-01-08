using TMPro;
using UnityEngine;

public class Quoter : MonoBehaviour
{
    
    void Start()
    {

        string[] quote = new string[5];
        quote[0] = "Many a man has finally succeeded only because he has failed after repeated efforts. If he had never met defeat he would never have known any great victory. - Orison Swett Marden";
        quote[1] = "Back of every mistaken venture and defeat is the laughter of wisdom, if you listen. - Carl Sandburg";
        quote[2] = "It's easy to do anything in victory. It's in defeat that a man reveals himself. - Floyd Patterson";
        quote[3] = "Victory has a thousand fathers, but defeat is an orphan. - John F. Kennedy";
        quote[4] = "Victory is sweetest when you've known defeat. - Malcolm S. Forbes";
        int randomIndex = Random.Range(0, quote.Length);
        gameObject.GetComponent<TextMeshProUGUI>().text = quote[randomIndex];
   
    }

}
