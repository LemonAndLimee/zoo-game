﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalGeneration : MonoBehaviour
{
    public StartManagement startScript;
    public WorldManagement worldScript;

    public GameObject currentObject;
    public GameObject prefab;

    public GameObject pigPrefab;

    // Start is called before the first frame update
    void Start()
    {
        startScript = GameObject.FindGameObjectWithTag("StartManager").GetComponent<StartManagement>();

        //if world is being loaded
        if (startScript.isLoad == true && startScript.isNew == false)
        {
            LoadAnimals();
        }
    }

    public void LoadAnimals()
    {
        worldScript.Load();

        for (int i = 0; i < worldScript.animalNames.Count; i++)
        {
            if (worldScript.animalNames[i] == "Pig")
            {
                prefab = pigPrefab;
            }

            SpawnAnimal(prefab, worldScript.animal_x_positions[i], worldScript.animal_y_positions[i]);
        }
    }

    public void SpawnAnimal(GameObject pref, float x, float y)
    {
        currentObject = Instantiate(pref);
        currentObject.transform.position = new Vector3(x, y, 0f);
    }
}
