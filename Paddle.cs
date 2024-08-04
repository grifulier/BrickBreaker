using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Paddle : MonoBehaviour
{

    public float speed;
    public float rightScreenEdge;
    public float leftScreenEdge;
    public GameManager gm;
    public AudioSource powerupAudio;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (gm.gameOver) {
            return;
        }
        float horizontal = Input.GetAxis("Horizontal");

        transform.Translate (Vector2.right * horizontal * Time.deltaTime * speed);
        if (transform.position.x < leftScreenEdge) {
            transform.position = new Vector2(leftScreenEdge, transform.position.y);
        }
        if (transform.position.x > rightScreenEdge) {
            transform.position = new Vector2(rightScreenEdge, transform.position.y);
        }
    }

    void OnTriggerEnter2D(Collider2D other) {
        powerupAudio.Play();
        if(other.CompareTag("Extra Life")) {
            gm.UpdateLives(1);
        }
        if(other.CompareTag("Longer Paddle")) {
            transform.localScale = new Vector2(20, transform.localScale.y);
            Invoke("ResetSize", 20f);
        }
        if(other.CompareTag("Faster Paddle")) {
            speed = 12;
            Invoke("ResetSpeed", 20f);
        }
        Destroy(other.gameObject);
    }

    void ResetSize() {
        transform.localScale = new Vector2(15, transform.localScale.y);
    }

    void ResetSpeed() {
        speed = 7;
    }
}
