using UnityEngine;

public class Follower : MonoBehaviour
{
    GameObject player;
    void Update()
    {
        if (this.tag == "GoodTrack") {
            player = GameObject.Find("PlayerCharacter");
        } else {
            player = GameObject.Find("OppositionCharacter");
        }
        if (player != null)
        {
            float locale = player.transform.position.x;

            if (locale > 5.8) {
                locale = 5.8f;
            } else if (locale < -5.8) {
                locale = -5.8f;
            }
            transform.position = new Vector3(locale, transform.position.y, transform.position.z);
        }
    }

}
