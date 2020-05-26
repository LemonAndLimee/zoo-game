using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SpawnAnimal : MonoBehaviour
{
    public GameObject currentObject;
    public GameObject currentCanvas;
    public GameObject currentIcon;

    public WorldManagement worldScript;

    void Start()
    {
        worldScript = GameObject.Find("GameManager").GetComponent<WorldManagement>();
    }

    public void Spawn(GameObject animal, int age, GameObject canvas, GameObject warningIcon)
    {
        HabitatStats statsScript = gameObject.GetComponent<HabitatStats>();
        if (statsScript.spaceLeft >= 1)
        {
            if (statsScript.possibleAnimals.Contains(animal.name))
            {
                currentObject = Instantiate(animal);
                currentObject.transform.position = new Vector3(transform.position.x, transform.position.y, -2f);
                currentObject.transform.Rotate(0f, 0f, Random.Range(0, 361));

                worldScript.animals.Add(currentObject);
                worldScript.animalNames.Add(animal.name);
                worldScript.hasWorker.Add(false);
                worldScript.animal_x_positions.Add(currentObject.transform.position.x);
                worldScript.animal_y_positions.Add(currentObject.transform.position.y);
                worldScript.animal_z_rotations.Add(currentObject.transform.eulerAngles.z);

                worldScript.animalZeroCounters.Add(0);
                worldScript.isAlive.Add(true);

                worldScript.animalHabitatIndexes.Add(statsScript.worldScriptIndex);

                statsScript.animals.Add(currentObject);

                AnimalStats animalStatsScript = currentObject.GetComponent<AnimalStats>();

                animalStatsScript.age = age;
                worldScript.ages.Add(animalStatsScript.age);

                //Debug.Log(worldScript.animalFoodLevels);
                worldScript.animalFoodLevels.Add(animalStatsScript.foodLevel);
                //Debug.Log(worldScript.animalFoodLevels);
                worldScript.animalWaterLevels.Add(animalStatsScript.waterLevel);

                animalStatsScript.worldScriptIndex = worldScript.animals.IndexOf(currentObject);

                statsScript.spaceLeft -= 1;

                //spawn stats canvas
                currentCanvas = Instantiate(canvas);
                FollowAnimal followScript = currentCanvas.GetComponent<FollowAnimal>();
                followScript.animal = currentObject;
                followScript.offset = new Vector3(0f, 1.5f, 0f);

                currentIcon = Instantiate(warningIcon);
                followScript = currentIcon.GetComponent<FollowAnimal>();
                followScript.animal = currentObject;
                followScript.offset = new Vector3(0f, 0f, 0f);

                animalStatsScript.childStatsPanel = currentCanvas.transform.GetChild(0).gameObject;
                animalStatsScript.warningIcon = currentIcon;
            }
        }
        
    }
}
