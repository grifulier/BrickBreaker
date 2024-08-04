using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public Rigidbody2D rb;
    public bool inPlay;
    public Transform paddle;
    public float speed;
    public Transform explosion;
    public GameManager gm;
    public Transform extraLifePowerup;
    public Transform largePaddlePowerup;
    public Transform fastPaddlePowerup;
    AudioSource sound;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        inPlay = false;
        sound = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (gm.gameOver){
            transform.position = paddle.position;
            rb.velocity = Vector2.zero;
            inPlay = false;
            return;
        }
        if (!inPlay) {
            transform.position = paddle.position;
        }

        if (Input.GetButtonDown("Jump") && !inPlay) {
            inPlay = true;
            rb.AddForce(Vector2.up * speed);
        }
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Bottom")) {
            rb.velocity = Vector2.zero;
            inPlay = false;
            gm.UpdateLives(-1);
        }
    }

    void OnCollisionEnter2D(Collision2D other) {
        if (other.transform.CompareTag("Brick")) {
            BrickScript brickScript = other.gameObject.GetComponent<BrickScript>();
            if (brickScript.hitsToBreak > 1) {
                brickScript.BreakBrick();
            } else {
                int randChance = Random.Range(1, 101);
                if (randChance < 30) {
                    Instantiate(extraLifePowerup, other.transform.position, other.transform.rotation);
                }else if (randChance < 40) {
                    Instantiate(largePaddlePowerup, other.transform.position, other.transform.rotation);
                }else if (randChance < 50) {
                    Instantiate(fastPaddlePowerup, other.transform.position, other.transform.rotation);
                }
                
                Transform newExplotion = Instantiate(explosion, other.transform.position, other.transform.rotation);
                Destroy(newExplotion.gameObject, 2.5f);

                gm.UpdateScore (brickScript.points);
                gm.UpdateNumberOfBricks();
                Destroy(other.gameObject);
            }

            sound.Play();
        }
    }

}
