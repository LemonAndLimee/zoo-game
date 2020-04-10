using System;
using System.Collections;
using System.Collections.Generic;
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
            LoadWorld();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
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

        //allows world to be saved
        worldScript.canSave = true;
    }

    public void LoadWorld()
    {
        worldScript.Load();

        for (int i = 0; i < worldScript.names.Count; i++)
        {
            int pos = Array.IndexOf(treeNames, worldScript.names[i]);
            if (pos > -1)
            {
                currentObject = Instantiate(treePrefabs[pos]);
                currentObject.transform.position = new Vector3(worldScript.x_positions[i], worldScript.y_positions[i], 0f);

                currentObject.transform.localScale = new Vector3(worldScript.sizes[i], worldScript.sizes[i], 1f);
            }

            pos = Array.IndexOf(rockNames, worldScript.names[i]);
            if (pos > -1)
            {
                currentObject = Instantiate(rockPrefabs[pos]);
                currentObject.transform.position = new Vector3(worldScript.x_positions[i], worldScript.y_positions[i], 0f);

                currentObject.transform.localScale = new Vector3(worldScript.sizes[i], worldScript.sizes[i], 1f);
            }
        }
    }
}
