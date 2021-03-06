﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

    public PrefabsManagement prefabScript;

    public WorldManagement worldScript;
    public int worldScriptIndex;
    public GameObject habitat;

    public NotificationManagement notificationScript;

    public int animalIndex;

    public GameObject target;
    public bool chasingTarget;

    public bool justEaten;

    // Start is called before the first frame update
    void Start()
    {
        prefabScript = GameObject.Find("PrefabManager").GetComponent<PrefabsManagement>();
        worldScript = GameObject.Find("GameManager").GetComponent<WorldManagement>();
        notificationScript = GameObject.Find("GameManager").GetComponent<NotificationManagement>();

        worldScriptIndex = gameObject.GetComponent<AnimalStats>().worldScriptIndex;
        habitat = worldScript.habitats[worldScript.animalHabitatIndexes[worldScriptIndex]];

        rb = gameObject.GetComponent<Rigidbody2D>();
        timeLimit = Random.Range(3, 9);
        targetRotation = Random.Range(0, 361);

        animalIndex = gameObject.GetComponent<AnimalStats>().animalIndex;

        moveChance = prefabScript.scripts[animalIndex].averageSpeed / 4f;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.GetComponent<AnimalStats>().isPredator == true && justEaten == false)
        {
            if (target == null)
            {
                chasingTarget = false;
                HabitatStats habitatScript = habitat.GetComponent<HabitatStats>();
                for (int i = 0; i < habitatScript.animals.Count; i++)
                {
                    AnimalStats animalScript = habitatScript.animals[i].GetComponent<AnimalStats>();
                    if (gameObject.GetComponent<AnimalStats>().foodAnimals.Contains(animalScript.animalType))
                    {
                        //Debug.Log("Food detected");
                        target = habitatScript.animals[i];
                        chasingTarget = true;
                    }
                }
            }
        }

        if (chasingTarget == true && target != null)
        {
            transform.right = Vector3.Lerp(transform.right, (target.transform.position - transform.position), 0.03f);
        }
        else
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
        }

        int num = Random.Range(0, 101);
        if (num <= moveChance)
        {
            speed = Random.Range(prefabScript.scripts[animalIndex].averageSpeed-10, prefabScript.scripts[animalIndex].averageSpeed+11);
            rb.velocity += new Vector2(transform.right.x, transform.right.y) * speed / 10;

        }

    }

    public void EatTarget()
    {
        gameObject.GetComponent<AnimalStats>().Feed(true);
        chasingTarget = false;
        notificationScript.DeathMessage(target.GetComponent<AnimalStats>().animalType, target.GetComponent<AnimalStats>().animalName, " was eaten by the " + gameObject.GetComponent<AnimalStats>().animalType + " " + gameObject.GetComponent<AnimalStats>().animalName);
        target.GetComponent<AnimalStats>().isAlive = false;
        target = null;
        justEaten = true;
        Invoke("UnsetJustEaten", 7f);
    }
    public void UnsetJustEaten()
    {
        justEaten = false;
    }
}
