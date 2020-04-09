using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody2D rb;

    public float movementX;
    public float movementY;
    public float speed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        movementX = Input.GetAxis("Horizontal") * speed;
        movementY = Input.GetAxis("Vertical") * speed;
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + new Vector2(movementX * Time.fixedDeltaTime, movementY * Time.fixedDeltaTime));
    }
}
