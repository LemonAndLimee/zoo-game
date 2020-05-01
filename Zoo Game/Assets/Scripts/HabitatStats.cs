using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HabitatStats : MonoBehaviour
{
    public string[] possibleAnimals;
    public int size;
    public int sizeInUnits;
    public int capacity;
    public int spaceLeft;

    public WorldManagement worldScript;
    public int worldScriptIndex;

    public StartManagement startScript;

    public bool placed;

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
            possibleAnimals = FarmEnclosureInfo.animals;
            sizeInUnits = FarmEnclosureInfo.sizes[size];
            capacity = FarmEnclosureInfo.capacity[size];

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
        }
        else
        {
            spaceLeft = capacity;
        }
        
    }
}
