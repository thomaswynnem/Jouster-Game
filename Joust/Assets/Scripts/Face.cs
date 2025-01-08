using UnityEngine;
public class Face : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    public Sprite[] sprites;
    private int spriteIndex = 0;
    private void Awake() {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    private void Start() {

        InvokeRepeating(nameof(AnimateSprite), .8f, .8f);

    }
    private void AnimateSprite() {

        spriteIndex++;
        if (spriteIndex>=sprites.Length) {
            spriteIndex = 0;
        }
        spriteRenderer.sprite = sprites[spriteIndex];

    }
}
