using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//given to each placeable object, to check if overlapping with another object
public class TriggerDetection : MonoBehaviour
{
    public bool isTouching;

    public SpriteRenderer sr;

    public GameObject collisionObject;

    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        //if triggers with something other than background
        if (collision.gameObject.name != "Background")
        {
            //Debug.Log(gameObject.name + " trig " + collision.gameObject.name);
            Debug.Log(collision.gameObject.name + " " + collision.gameObject.tag);
            if (collision.gameObject.tag != "Terrain" && collision.gameObject.tag != "Animal")
            {
                //set touching to true, turns red
                isTouching = true;
                Red();
            }
            else
            {
                collisionObject = collision.gameObject;
            }
        }
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        isTouching = false;
        RemoveRed();
    }


    public void Red()
    {
        sr.enabled = true;
    }
    public void RemoveRed()
    {
        sr.enabled = false;
    }
}
