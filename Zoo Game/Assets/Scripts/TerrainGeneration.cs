using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class TerrainGeneration : MonoBehaviour
{
    //actual min and max / 2, to get an even number
    public int minX;
    public int maxX;
    public int minY;
    public int maxY;

    public int minTreeSize;
    public int maxTreeSize;

    public int minRockSize;
    public int maxRockSize;

    public GameObject[] treePrefabs;
    public string[] treeNames;

    public GameObject[] rockPrefabs;
    public string[] rockNames;

    public GameObject currentObject;

    public int treesNum;

    public int rocksNum;

    public List<Vector3> positions = new List<Vector3>();

    public WorldManagement worldScript;

    public StartManagement startScript;


    public GameObject[] pathPrefabs;
    public string[] pathNames;

    public string[] hygieneNames;
    public string[] shopNames;

    public CustomerManagement customerScript;

    public List<GameObject> habitats = new List<GameObject>();


    public PrefabsManagement prefabScript;

    // Start is called before the first frame update
    void Start()
    {
        
        startScript = GameObject.FindGameObjectWithTag("StartManager").GetComponent<StartManagement>();

        if (startScript.isNew == true && startScript.isLoad == false)
        {
            GenerateNew();
        }
        else if (startScript.isLoad == true && startScript.isNew == false)
        {
            Invoke("LoadWorld", 0.1f);
        }
        
    }

    public void GenerateNew()
    {
        for (int i = 0; i < treesNum; i++)
        {
            //picks random tree prefab, instantiates it
            int num = Random.Range(0, treePrefabs.Length);
            currentObject = Instantiate(treePrefabs[num]);

            //picks random position, then x2 to get only even numbers
            int x = Random.Range(minX, maxX) * 2;
            int y = Random.Range(minY, maxY) * 2;

            //makes sure there are no overlaps in position
            while (positions.Contains(new Vector3(x, y, 0f)) || (x >= -4 && x <= 4 && y <= 2))
            {
                x = Random.Range(minX, maxX) * 2;
                y = Random.Range(minY, maxY) * 2;
            }

            currentObject.transform.position = new Vector3(x, y, 0f);

            //picks random size
            float size = Random.Range(minTreeSize, maxTreeSize) / 100f;

            currentObject.transform.localScale = new Vector3(size, size, 1f);


            //adds position to list storing positions
            positions.Add(currentObject.transform.position);

            //adds data to worldmanagement script lists
            worldScript.objects.Add(currentObject);

            worldScript.x_positions.Add(currentObject.transform.position.x);
            worldScript.y_positions.Add(currentObject.transform.position.y);
            worldScript.sizes.Add(currentObject.transform.localScale.x);

            worldScript.names.Add(treePrefabs[num].name);

        }

        for (int i = 0; i < rocksNum; i++)
        {
            int num = Random.Range(0, rockPrefabs.Length);
            currentObject = Instantiate(rockPrefabs[num]);

            int x = Random.Range(minX, maxX) * 2;
            int y = Random.Range(minY, maxY) * 2;

            while (positions.Contains(new Vector3(x, y, 0f)) || (x >= -2 && x <= 2 && y <= 0))
            {
                x = Random.Range(minX, maxX) * 2;
                y = Random.Range(minY, maxY) * 2;
            }

            currentObject.transform.position = new Vector3(x, y, 0f);

            float size = Random.Range(minRockSize, maxRockSize) / 100f;

            currentObject.transform.localScale = new Vector3(size, size, 1f);

            positions.Add(currentObject.transform.position);


            worldScript.objects.Add(currentObject);

            worldScript.x_positions.Add(currentObject.transform.position.x);
            worldScript.y_positions.Add(currentObject.transform.position.y);
            worldScript.sizes.Add(currentObject.transform.localScale.x);

            worldScript.names.Add(rockPrefabs[num].name);
        }

        //generates paths
        GenerateStartPaths();

        //allows world to be saved
        worldScript.canSave = true;
    }

    public void LoadWorld()
    {
        worldScript.Load();

        //for every object in the list of names
        for (int i = 0; i < worldScript.names.Count; i++)
        {
            //checks if in list of tree names
            int pos = Array.IndexOf(treeNames, worldScript.names[i]);
            if (pos > -1)
            {
                //generates new tree, assigns it to correct position and size
                currentObject = Instantiate(treePrefabs[pos]);
                currentObject.transform.position = new Vector3(worldScript.x_positions[i], worldScript.y_positions[i], 0f);

                currentObject.transform.localScale = new Vector3(worldScript.sizes[i], worldScript.sizes[i], 1f);
                worldScript.objects.Add(currentObject);
            }

            //checks if in list of rock names
            pos = Array.IndexOf(rockNames, worldScript.names[i]);
            if (pos > -1)
            {
                currentObject = Instantiate(rockPrefabs[pos]);
                currentObject.transform.position = new Vector3(worldScript.x_positions[i], worldScript.y_positions[i], 0f);

                currentObject.transform.localScale = new Vector3(worldScript.sizes[i], worldScript.sizes[i], 1f);
                worldScript.objects.Add(currentObject);
            }

            //checks if in list of paths names
            pos = Array.IndexOf(pathNames, worldScript.names[i]);
            if (pos > -1)
            {
                currentObject = Instantiate(pathPrefabs[pos]);
                currentObject.transform.position = new Vector3(worldScript.x_positions[i], worldScript.y_positions[i], 0f);

                currentObject.transform.localScale = new Vector3(worldScript.sizes[i], worldScript.sizes[i], 1f);
                worldScript.objects.Add(currentObject);
            }

            pos = Array.IndexOf(hygieneNames, worldScript.names[i]);
            if (pos > -1)
            {
                customerScript.comfortPoints += prefabScript.hygieneScripts[pos].comfortPoints;

                currentObject = Instantiate(prefabScript.hygieneFacilities[pos]);
                currentObject.transform.position = new Vector3(worldScript.x_positions[i], worldScript.y_positions[i], 0f);

                currentObject.transform.localScale = new Vector3(worldScript.sizes[i], worldScript.sizes[i], 1f);
                worldScript.objects.Add(currentObject);
            }

            pos = Array.IndexOf(shopNames, worldScript.names[i]);
            if (pos > -1)
            {
                customerScript.comfortPoints += prefabScript.shopScripts[pos].comfortPoints;

                currentObject = Instantiate(prefabScript.shops[pos]);
                currentObject.transform.position = new Vector3(worldScript.x_positions[i], worldScript.y_positions[i], 0f);

                currentObject.transform.localScale = new Vector3(worldScript.sizes[i], worldScript.sizes[i], 1f);
                worldScript.objects.Add(currentObject);

            }

            //SpawnHabitat(i);
        }

        for (int i = 0; i < worldScript.habitatPrefabIndexes.Count; i++)
        {
            SpawnHabitat(i);
        }


        worldScript.canSave = true;
        
    }

    public void GenerateStartPaths()
    {
        for (int i = -2; i >= -8; i--)
        {
            Path(new Vector3(0f, i*2, 0f));
        }

    }

    public void Path(Vector3 pos)
    {
        int n = Random.Range(0, pathPrefabs.Length);
        currentObject = Instantiate(pathPrefabs[n]);
        currentObject.transform.position = pos;
        worldScript.objects.Add(currentObject);
        worldScript.names.Add(pathPrefabs[n].name);
        worldScript.x_positions.Add(currentObject.transform.position.x);
        worldScript.y_positions.Add(currentObject.transform.position.y);
        worldScript.sizes.Add(currentObject.transform.localScale.x);
    }

    public void SpawnHabitat(int i)
    {
        currentObject = Instantiate(prefabScript.habitats[worldScript.habitatPrefabIndexes[i]]);
        Debug.Log(currentObject.GetComponent<BoxCollider2D>());
        int objectIndex = worldScript.habitatObjectIndexes[i];
        currentObject.transform.position = new Vector3(worldScript.x_positions[objectIndex], worldScript.y_positions[objectIndex], -1f);
        currentObject.transform.localScale = new Vector3(worldScript.sizes[objectIndex], worldScript.sizes[objectIndex], 1f);
        worldScript.habitats.Add(currentObject);
        worldScript.objects.Add(currentObject);
        //sends array index of current object to its script
        HabitatStats statsScript = currentObject.GetComponent<HabitatStats>();

        statsScript.worldScriptIndex = worldScript.habitats.IndexOf(currentObject);
        statsScript.objectIndex = objectIndex;
        statsScript.placed = true;

        int typeIndex = worldScript.habitatTypeIndexes[i];
        statsScript.habitatTypeIndex = typeIndex;
        statsScript.possibleAnimals = prefabScript.habitatScripts[typeIndex].animals.ToList();
        int size = 0;
        if (currentObject.name.Contains("Medium"))
        {
            size = 1;
        }
        else if (currentObject.name.Contains("Large"))
        {
            size = 2;
        }
        statsScript.sizeInUnits = prefabScript.habitatScripts[typeIndex].sizes[size];
        statsScript.capacity = prefabScript.habitatScripts[typeIndex].capacity[size];
        statsScript.spaceLeft = worldScript.habitatsSpaceLeft[i];
    }

}
