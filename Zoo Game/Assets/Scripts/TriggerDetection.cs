using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerDetection : MonoBehaviour
{
    public bool isTouching;

    public SpriteRenderer sr;

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
            //set touching to true, turns red
            Debug.Log(gameObject.name + " trig " + collision.gameObject.name);
            isTouching = true;
            Red();
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
