using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnAnimal : MonoBehaviour
{
    public GameObject currentObject;

    public WorldManagement worldScript;

    void Start()
    {
        worldScript = GameObject.Find("GameManager").GetComponent<WorldManagement>();
    }

    public void Spawn(GameObject animal)
    {
        currentObject = Instantiate(animal);
        currentObject.transform.position = transform.position;

        worldScript.animalNames.Add(animal.name);
        worldScript.animal_x_positions.Add(currentObject.transform.position.x);
        worldScript.animal_y_positions.Add(currentObject.transform.position.y);

        HabitatStats statsScript = gameObject.GetComponent<HabitatStats>();
        statsScript.spaceLeft -= 1;
    }
}
