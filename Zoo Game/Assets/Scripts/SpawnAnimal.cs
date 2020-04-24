using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
        HabitatStats statsScript = gameObject.GetComponent<HabitatStats>();
        if (statsScript.spaceLeft >= 1)
        {
            if (statsScript.possibleAnimals.Contains(animal.name))
            {
                currentObject = Instantiate(animal);
                currentObject.transform.position = transform.position;

                worldScript.animals.Add(currentObject);
                worldScript.animalNames.Add(animal.name);
                worldScript.animal_x_positions.Add(currentObject.transform.position.x);
                worldScript.animal_y_positions.Add(currentObject.transform.position.y);

                AnimalStats animalStatsScript = currentObject.GetComponent<AnimalStats>();
                Debug.Log(worldScript.animalFoodLevels);
                worldScript.animalFoodLevels.Add(animalStatsScript.foodLevel);
                Debug.Log(worldScript.animalFoodLevels);
                worldScript.animalWaterLevels.Add(animalStatsScript.waterLevel);

                animalStatsScript.worldScriptIndex = worldScript.animals.IndexOf(currentObject);

                statsScript.spaceLeft -= 1;
            }
        }
        
    }
}
