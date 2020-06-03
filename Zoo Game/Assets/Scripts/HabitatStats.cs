using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class HabitatStats : MonoBehaviour
{
    public List<string> possibleAnimals = new List<string>();
    public int size;
    public int sizeInUnits;
    public int capacity;
    public int spaceLeft;

    public WorldManagement worldScript; //applies to habitat lists
    public int worldScriptIndex;
    public int objectIndex; // applies to object lists

    public StartManagement startScript;

    public bool placed;

    public List<GameObject> animals = new List<GameObject>();

    public int habitatTypeIndex;
    public PrefabsManagement prefabScript;

    // Start is called before the first frame update
    void Start()
    {
        prefabScript = GameObject.Find("PrefabManager").GetComponent<PrefabsManagement>();

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
            worldScriptIndex = worldScript.habitats.IndexOf(gameObject);

            objectIndex = worldScript.objects.IndexOf(gameObject);
            worldScript.habitatObjectIndexes[worldScriptIndex] = objectIndex;

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

        worldScript.habitatPrefabIndexes.RemoveAt(worldScriptIndex);
        worldScript.habitatTypeIndexes.RemoveAt(worldScriptIndex);
        worldScript.habitatObjectIndexes.RemoveAt(worldScriptIndex);

        Destroy(gameObject);
    }
}
