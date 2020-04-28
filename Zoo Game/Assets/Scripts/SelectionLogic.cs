using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//overall manager for selecting objects with mouse
public class SelectionLogic : MonoBehaviour
{
    public GameObject pigPrefab;
    public GameObject statsCanvasPrefab;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("e"))
        {
            Debug.Log("mouse");
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

            //if something is hit by raycast
            if (hit.collider.gameObject != null)
            {
                Debug.Log(hit.collider.gameObject.name);
                if (hit.collider.gameObject.tag == "Habitat")
                {
                    SpawnAnimal spawnScript = hit.collider.gameObject.GetComponent<SpawnAnimal>();
                    spawnScript.Spawn(pigPrefab, statsCanvasPrefab);
                    Debug.Log("spawn");
                }
                else if (hit.collider.gameObject.tag == "Animal")
                {
                    AnimalStats animalScript = hit.collider.gameObject.GetComponent<AnimalStats>();
                    animalScript.ToggleStatsPanel();
                }
            }
        }
    }
}
