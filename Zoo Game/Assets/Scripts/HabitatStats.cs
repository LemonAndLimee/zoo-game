using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class HabitatStats : MonoBehaviour
{
    public List<string> possibleAnimals = new List<string>();
    public List<int> babyCosts = new List<int>();
    public List<int> adultCosts = new List<int>();
    public int size;
    public int sizeInUnits;
    public int capacity;
    public int spaceLeft;

    public WorldManagement worldScript;
    public int worldScriptIndex;
    public int objectIndex;

    public StartManagement startScript;

    public bool placed;

    public List<GameObject> animals = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {

        //assigns integer value to size
        if (gameObject.name.Contains("Small"))
        {
            size = 0;
        }
        else if (gameObject.name.Contains("Medium"))
        {
            size = 1;
        }
        else if (gameObject.name.Contains("Large"))
        {
            size = 2;
        }

        //assigns correct values to habitat stats - sets spaceleft to capacity
        if (gameObject.name.Contains("Farm"))
        {
            possibleAnimals = FarmEnclosureInfo.animals.ToList();
            sizeInUnits = FarmEnclosureInfo.sizes[size];
            capacity = FarmEnclosureInfo.capacity[size];

            spaceLeft = capacity;

        }
        else if (gameObject.name.Contains("Savannah"))
        {
            possibleAnimals = SavannahEnclosureInfo.animals.ToList();
            sizeInUnits = SavannahEnclosureInfo.sizes[size];
            capacity = SavannahEnclosureInfo.capacity[size];
            spaceLeft = capacity;
        }

        startScript = GameObject.FindGameObjectWithTag("StartManager").GetComponent<StartManagement>();
        worldScript = GameObject.Find("GameManager").GetComponent<WorldManagement>();

        //if world is being loaded, loads in correct spaceleft
        if (startScript.isLoad == true && startScript.isNew == false && worldScript.gameTimer == 0f)
        {
            //ERROR - PLACING A HABITAT IN A LOADED WORLD MEANS IT TRIES TO LOAD SPACELEFT
            worldScript.Load();
            spaceLeft = worldScript.habitatsSpaceLeft[worldScriptIndex];
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (placed == true)
        {
            worldScript.habitatsSpaceLeft[worldScriptIndex] = spaceLeft;
            spaceLeft = capacity - animals.Count;
        }
        else
        {
            spaceLeft = capacity;
        }
        
    }

    public void Destroy()
    {
        for (int i = 0; i < animals.Count; i++)
        {
            animals[i].GetComponent<AnimalStats>().Delete();
        } //deletes all animals

        worldScript.habitats.RemoveAt(worldScriptIndex);
        worldScript.habitatsSpaceLeft.RemoveAt(worldScriptIndex);

        worldScript.objects.RemoveAt(objectIndex);
        worldScript.names.RemoveAt(objectIndex);
        worldScript.x_positions.RemoveAt(objectIndex);
        worldScript.y_positions.RemoveAt(objectIndex);
        worldScript.sizes.RemoveAt(objectIndex);

        Destroy(gameObject);
    }
}
