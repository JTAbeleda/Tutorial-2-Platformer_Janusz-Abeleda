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


    private int livesValue = 3;
    private int scoreValue = 0;

        // Start is called before the first frame update
    void Start()
    {
        rd2d = GetComponent<Rigidbody2D>();
        score.text = "Coins: " + scoreValue.ToString();
        lives.text = "Lives: " + livesValue.ToString();
        
        winTextObject.SetActive(false);
        loseTextObject.SetActive(false);
    }
    

    // Update is called once per frame
    void FixedUpdate()
    {
        float hozMovement = Input.GetAxis("Horizontal");
        float vertMovement = Input.GetAxis("Vertical");
        rd2d.AddForce(new Vector2(hozMovement * speed, vertMovement * speed));
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
       if(collision.collider.tag == "Coin")
        {
            scoreValue += 1;
            if(scoreValue == 6)
            {
                transform.position = new Vector3(84.0f, 0.0f, 0.0f);
            }
            if(scoreValue == 12)
            {
                winTextObject.SetActive(true);
            }

            score.text = "Coins: " + scoreValue.ToString();
            Destroy(collision.collider.gameObject);
        }

        if(collision.collider.tag == "Enemy")
        {
            livesValue -= 1;
            if(livesValue == 0)
             {
               loseTextObject.SetActive(true); 
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
