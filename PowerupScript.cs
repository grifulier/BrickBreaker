using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class PowerupScript : MonoBehaviour
{
    public float speed;
    AudioSource sound;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(new Vector2(0f, -1f) * Time.deltaTime * speed, Space.World);
        transform.Rotate(new Vector3(0f, 0f, 0.1f));

        if (transform.position.y < -6f) {
            Destroy(gameObject);
        }
    }
}
