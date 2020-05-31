using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody2D rb;

    public float movementX;
    public float movementY;
    public float speed;

    public int[] cameraSizes;
    public int currentindex = 1;
    public Camera cam;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        movementX = Input.GetAxis("Horizontal") * speed;
        movementY = Input.GetAxis("Vertical") * speed;

        if (Input.GetKeyDown(KeyCode.Plus) || Input.GetKeyDown(KeyCode.Equals))
        {
            ZoomIn();
        }
        if (Input.GetKeyDown(KeyCode.Minus))
        {
            ZoomOut();
        }

        cam.orthographicSize = cameraSizes[currentindex];
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + new Vector2(movementX * Time.fixedDeltaTime, movementY * Time.fixedDeltaTime));
    }

    public void ZoomIn()
    {
        if (currentindex > 0)
        {
            currentindex -= 1;
        }
    }
    public void ZoomOut()
    {
        if (currentindex < cameraSizes.Count()-1)
        {
            currentindex += 1;
        }
    }
}
