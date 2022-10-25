using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerScript : MonoBehaviour
{

    private Rigidbody2D rd2d;
    public float speed;
    public TextMeshProUGUI score;
    public TextMeshProUGUI lives;
    public GameObject winTextObject;
     public GameObject loseTextObject;
     public GameObject winScreenObject;
     public GameObject loseScreenObject;
     public AudioClip musicClipOne;
      public AudioClip musicClipTwo;
      public AudioClip musicClipThree;
     public AudioSource musicSource;
    public bool isPaused;
    
    private bool gameOver = true;
    private bool facingRight = true;
    private bool isjumping = true;
    private int livesValue = 4;
    private int scoreValue = 0;

    Animator anim;

    

        // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();

        rd2d = GetComponent<Rigidbody2D>();
        score.text = "Coins: " + scoreValue.ToString();
        lives.text = "Lives: " + livesValue.ToString();
        
        winTextObject.SetActive(false);
        winScreenObject.SetActive(false);
        loseTextObject.SetActive(false);
        loseScreenObject.SetActive(false);
        
            musicSource.clip = musicClipThree;
            musicSource.Play();
            musicSource.loop = true;
            gameOver = false;
    }
    void Flip()
   {
     facingRight = !facingRight;
     Vector2 Scaler = transform.localScale;
     Scaler.x = Scaler.x * -1;
     transform.localScale = Scaler;
   }

    
    // Update is called once per frame
    void FixedUpdate()
    {
        float hozMovement = Input.GetAxis("Horizontal");
        float vertMovement = Input.GetAxis("Vertical");
        rd2d.AddForce(new Vector2(hozMovement * speed, vertMovement * speed));

      if (facingRight == false && hozMovement > 0)
   {
         Flip();
   }
    else if (facingRight == true && hozMovement < 0)
    {
        Flip();
    }
        if (Input.GetKeyDown(KeyCode.D))
            {
                anim.SetInteger("State", 1);
            }
        if (Input.GetKeyUp(KeyCode.D))
             {
                anim.SetInteger("State", 0);
             }
        if (Input.GetKeyDown(KeyCode.A))
            {
                anim.SetInteger("State", 1);
            }
        if (Input.GetKeyUp(KeyCode.A))
            {
                anim.SetInteger("State", 0);
            }
        if (Input.GetKeyDown(KeyCode.W))
            {
                anim.SetInteger("State", 2);
            }
        if (Input.GetKeyUp(KeyCode.W))
            {
            anim.SetInteger("State", 0);
            }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
       
       if(collision.collider.tag == "Coin")
        {
            scoreValue += 1;
            if(scoreValue == 4)
            {
                transform.position = new Vector3(84.0f, 0.0f, 0.0f);
            }
            if(scoreValue == 10)
            {
                musicSource.clip = musicClipOne;
                musicSource.Play();
                musicSource.loop = false;
                winTextObject.SetActive(true);
                winScreenObject.SetActive(true);


                Time.timeScale = 0f;
                isPaused = true;
            }

            score.text = "Coins: " + scoreValue.ToString();
            Destroy(collision.collider.gameObject);
        }

        if(collision.collider.tag == "Boss")
        {
            livesValue -= 2;
            if(livesValue == 0)
             {
                musicSource.clip = musicClipTwo;
                musicSource.Play();
                musicSource.loop = false;
               loseTextObject.SetActive(true);
              
               Time.timeScale = 0f;
                isPaused = true;
                loseScreenObject.SetActive(true);
             }

           lives.text = "Lives: " + livesValue.ToString();
            Destroy(collision.collider.gameObject);
        }
        if(collision.collider.tag == "Enemy")
        {
            livesValue -= 1;
            if(livesValue == 0)
             {
                musicSource.clip = musicClipTwo;
                musicSource.Play();
                musicSource.loop = false;
               loseTextObject.SetActive(true);
              
               Time.timeScale = 0f;
                isPaused = true;
                loseScreenObject.SetActive(true);
             }

           lives.text = "Lives: " + livesValue.ToString();
            Destroy(collision.collider.gameObject);
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.tag == "Ground")
        {
            if (Input.GetKey(KeyCode.W))
            {
                rd2d.AddForce(new Vector2(0, 3), ForceMode2D.Impulse);
            }
        }
        
    }
}
