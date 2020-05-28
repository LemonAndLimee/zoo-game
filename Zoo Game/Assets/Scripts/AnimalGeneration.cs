using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalGeneration : MonoBehaviour
{
    public StartManagement startScript;
    public WorldManagement worldScript;

    public GameObject currentObject;
    public GameObject currentCanvas;
    public GameObject canvasPrefab;
    public GameObject warningIconPrefab;
    public GameObject currentIcon;
    public GameObject prefab;

    public GameObject pigPrefab;
    public GameObject llamaPrefab;


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
            else if (worldScript.animalNames[i] == "Llama")
            {
                prefab = llamaPrefab;
            }

            SpawnAnimal(prefab, worldScript.animal_x_positions[i], worldScript.animal_y_positions[i], i);
        }
    }

    public void SpawnAnimal(GameObject pref, float x, float y, int i)
    {
        currentObject = Instantiate(pref);
        currentObject.transform.position = new Vector3(x, y, -2f);
        currentObject.transform.Rotate(0f, 0f, worldScript.animal_z_rotations[i]);

        GameObject currentHabitat = worldScript.habitats[worldScript.animalHabitatIndexes[i]];

        HabitatStats habitatStatsScript = currentHabitat.GetComponent<HabitatStats>();
        habitatStatsScript.animals.Add(currentObject);

        worldScript.animals.Add(currentObject);

        AnimalStats statsScript = currentObject.GetComponent<AnimalStats>();
        statsScript.worldScriptIndex = i;

        statsScript.age = worldScript.ages[i];
        statsScript.animalName = worldScript.animalGivenNames[i];

        //spawn stats canvas
        currentCanvas = Instantiate(canvasPrefab);
        FollowAnimal followScript = currentCanvas.GetComponent<FollowAnimal>();
        followScript.animal = currentObject;
        followScript.offset = new Vector3(0f, 1.5f, 0f);

        currentIcon = Instantiate(warningIconPrefab);
        followScript = currentIcon.GetComponent<FollowAnimal>();
        followScript.animal = currentObject;
        followScript.offset = new Vector3(0f, 0f, 0f);

        statsScript.childStatsPanel = currentCanvas.transform.GetChild(0).gameObject;
        statsScript.warningIcon = currentIcon;

        statsScript.zeroHealthCounter = worldScript.animalZeroCounters[i];
        statsScript.isAlive = worldScript.isAlive[i];

    }
}
