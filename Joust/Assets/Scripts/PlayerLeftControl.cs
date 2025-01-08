using UnityEngine;

public class PlayerLeftController : MonoBehaviour
{
    public bool opened = false;
    public int times = 0;
    bool duringthrust = false;
    public bool thrust = false;
    private float thrustDuration = 0.5f;
    private float thrustTimer = 0f;
    public float speed;   
    private Rigidbody2D playerRigidbody2D;
    private SpriteRenderer spriteRenderer;
    public Sprite[] anim;
    public GameObject dustBlob;
    public GameObject GoldMeter;
    private int spriteIndex = 0;
    public float held = 0f;
    private int power;
    public int powerMoment = 0;
    // Speeds
    [SerializeField] private float samSpeed = -4.5f;
    [SerializeField] private float ravenSpeed = -4f;
    [SerializeField] private float barnyardSpeed = -3.5f;

    // Classic Controls
    [SerializeField] private float classicY = -3f;
    [SerializeField] private float classicScale = 2f;

    // Power Controls
    [SerializeField] private float flyingY = -2f;
    [SerializeField] private float buffedScale = 3f;

    void Start()
    {

        if (PlayerPrefs.GetInt("SelectedCharacterIndex") == 0) {

            speed = samSpeed;  
            power = 1;

        } else if (PlayerPrefs.GetInt("SelectedCharacterIndex") == 1) {

            speed = ravenSpeed;
            power = 2;

        } else  {

            speed = barnyardSpeed;
            power = 3;

        }     
        GameObject.Find("OppositionCharacter").GetComponent<OpponentRightControls>().enabled = false;
        spriteRenderer = GetComponent<SpriteRenderer>();
        playerRigidbody2D = GetComponent<Rigidbody2D>();
        playerRigidbody2D.velocity = new Vector2(0, playerRigidbody2D.velocity.y);
        GameObject.Find("OppositionCharacter").GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        anim = GetComponent<Anim>().anime;
        GoldMeter = GameObject.Find("GoldMeter");

    }

    private void Update()
    {

        HandleInput();
        if (powerMoment == 1 && power == 2) {

            Vector3 pos = gameObject.transform.position;
            pos.y = flyingY;
            gameObject.transform.position = pos;

        } else if (powerMoment == 1 && power == 1) {

            Vector3 sz = gameObject.transform.localScale;
            sz.x = buffedScale;
            sz.y = buffedScale;
            gameObject.transform.localScale = sz;

        } else {

            if (gameObject.transform.position.y != -3) {

                Vector3 pos = gameObject.transform.position;
                pos.y = classicY;
                gameObject.transform.position = pos;

            }
            if (gameObject.transform.localScale.y != 2) {

                Vector3 sz = gameObject.transform.localScale;
                sz.x = classicScale;
                sz.y = classicScale;
                gameObject.transform.localScale = sz;

            }
        
        }

    }

    void FixedUpdate()
    {

        float horizantalInput = Input.GetAxis("Horizontal");
        if (opened) {

            if (horizantalInput<0){

                playerRigidbody2D.velocity = new Vector2(speed + horizantalInput*held, playerRigidbody2D.velocity.y);
            
            } else {

                playerRigidbody2D.velocity = new Vector2(speed, playerRigidbody2D.velocity.y);
            
            } 
            if (!duringthrust && powerMoment ==0) {

                AnimateSprite(held);
            
            } else if (!duringthrust && powerMoment == 1 && (power == 2|| power == 3)) {

                PowerAnimation();

            } else {

                thrustTimer -= Time.fixedDeltaTime;
                if (thrustTimer <= 0) {

                    duringthrust = false;

                }

            }
  
        }
        if (horizantalInput < 0) {

            held += Time.fixedDeltaTime;
            opened = true;
            GameObject.Find("OppositionCharacter").GetComponent<OpponentRightControls>().enabled = true;
        
        } else {

            held = 0f;

        }
        
    }

    private void HandleInput()
    {
        int renderSprite;
        if (powerMoment == 1 && (power == 2 || power == 3)) {

            renderSprite = 6;

        } else {
            
            renderSprite = 2;

        }

        if (opened && Input.GetKeyDown(KeyCode.Space) && times == 0) {

            times = 1;
            spriteRenderer.sprite = anim[renderSprite];
            duringthrust = true;
            times++;
            thrustTimer = thrustDuration; 

        } else if (opened && Input.GetKeyDown(KeyCode.F) && times == 0) {

            times = 1;
            spriteRenderer.sprite = anim[3];
            duringthrust = true;
            times++;
            thrustTimer = thrustDuration; 

        }
        
    }

     private void AnimateSprite(float held) {

        if (held == 0) {

            if (spriteIndex>=10) {

                spriteRenderer.sprite = anim[0];
                if (spriteIndex == 20) {

                    spriteIndex = 0;

                }

            } else { 

                spriteRenderer.sprite = anim[1];

            }

        }
        else {

            if (spriteIndex>=5) {

                spriteRenderer.sprite = anim[0];
                if (spriteIndex == 10) {

                    spriteIndex = 0;

                }
            } else { 

                spriteRenderer.sprite = anim[1];

            }
        } spriteIndex++;

    }

    private void PowerAnimation() {

        if (held == 0) {

            if (spriteIndex>=10) {

                spriteRenderer.sprite = anim[4];
                if (spriteIndex == 20) {

                    spriteIndex = 0;

                }

            } else { 

                spriteRenderer.sprite = anim[5];

            }

        }
        else {

            if (spriteIndex>=5) {

                spriteRenderer.sprite = anim[4];
                if (spriteIndex == 10) {

                    spriteIndex = 0;

                }

            } else { 

                spriteRenderer.sprite = anim[5];

            }
        } spriteIndex++;

    }


    private void OnTriggerEnter2D(Collider2D other) {
        
        Debug.Log("Triggered");
        if (other.CompareTag("Border1")) {

            GameObject.Find("SceneManager").GetComponent<JoustSceneManager>().Right();

        }

    }

}


