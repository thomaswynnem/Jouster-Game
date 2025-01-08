using UnityEngine;

public class OpponentLeftControls : MonoBehaviour
{
    public float speed = 5f;     
    private Rigidbody2D playerRigidbody2D;
    private SpriteRenderer spriteRenderer;
    public Sprite[] anim;
    public GameObject dustBlob;
    private int spriteIndex = 0;
    void Start()
    {

        spriteRenderer = GetComponent<SpriteRenderer>();
        playerRigidbody2D = GetComponent<Rigidbody2D>();
        anim = GetComponent<Anim>().anime;

    }
    void FixedUpdate()
    {
        
        playerRigidbody2D.velocity = new Vector2(-speed, playerRigidbody2D.velocity.y);
        AnimateSprite();

    }
     private void AnimateSprite() {

        if (spriteIndex>=10) {

            spriteRenderer.sprite = anim[0];
            if (spriteIndex == 20) {

                spriteIndex = 0;

            }

        } else { 

            spriteRenderer.sprite = anim[1];

        } spriteIndex++;

     }
        



}
