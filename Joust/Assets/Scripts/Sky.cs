using UnityEngine;
using Random = System.Random;

public class Sky : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    public Sprite[] sprites;
    private Random random = new Random();
    private void Awake() {

        spriteRenderer = GetComponent<SpriteRenderer>();

    }
    private void Start() {

        InvokeRepeating(nameof(AnimateSprite), .5f, .5f);

    }
    private void AnimateSprite() {

        int ans = random.Next(0,100);
        int next;
        if (ans < 90) {

            next = 0;

        } else {

            next = random.Next(1, 4);

        }
        spriteRenderer.sprite = sprites[next];
    }

}

