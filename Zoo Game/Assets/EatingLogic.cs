using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EatingLogic : MonoBehaviour
{
    public float timer = 0f;
    public AnimalMovement movementScript;

    void Start()
    {
        movementScript = gameObject.GetComponentInParent<AnimalMovement>();
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject == movementScript.target)
        {
            timer += Time.deltaTime;
            if (timer >= 4f)
            {
                movementScript.EatTarget();
            }
        }
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        timer = 0f;
    }
}
