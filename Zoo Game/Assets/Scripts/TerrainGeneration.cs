using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    public List<GameObject> trees = new List<GameObject>();

    public GameObject[] rockPrefabs;
    public List<GameObject> rocks = new List<GameObject>();

    public GameObject currentObject;

    public int treesNum;

    public int rocksNum;

    public List<Vector3> positions = new List<Vector3>();

    // Start is called before the first frame update
    void Start()
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

            //adds to list of trees
            trees.Add(currentObject);

            //adds position to list storing positions
            positions.Add(currentObject.transform.position);
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

            rocks.Add(currentObject);

            positions.Add(currentObject.transform.position);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
