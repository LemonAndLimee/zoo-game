using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class AnimalMovement : MonoBehaviour
{
    public Rigidbody2D rb;

    public float rotSpeed;
    public float speed;

    public bool isRotating;

    public float timer;
    public float timeLimit;

    public int targetRotation;

    public float moveChance;

    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        timeLimit = Random.Range(3, 9);
        targetRotation = Random.Range(0, 361);
    }

    // Update is called once per frame
    void Update()
    {


        timer += Time.deltaTime;
        if (timer >= timeLimit)
        {
            isRotating = true; 
        }

        if (isRotating == true)
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0, 0, targetRotation), rotSpeed * Time.deltaTime);
            int rotationZ = (int)Mathf.Round(transform.rotation.eulerAngles.z);
            if (rotationZ == targetRotation)
            {
                isRotating = false;
                timer = 0;
                timeLimit = Random.Range(3, 9);
                targetRotation = Random.Range(0, 361);
            }
        }


        int num = Random.Range(0, 101);
        if (num <= moveChance)
        {
            speed = Random.Range(5, 25);
            rb.velocity += new Vector2(-transform.right.x, -transform.right.y) * speed / 10;

        }

    }
}
