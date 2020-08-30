using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

//overall manager for selecting objects with mouse
public class SelectionLogic : MonoBehaviour
{
    public HabitatUIManagement habitatUIScript;

    public ShopUIManagement shopScript;

    public bool isDeleting;
    public GameObject hoverObject;
    public GameObject previousObject;

    public WorldManagement worldScript;

    // Update is called once per frame
    void Update()
    {
        if (isDeleting == true)
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            if (hit.collider.gameObject != null)
            {
                //Debug.Log(hit.collider.gameObject.name);
                hoverObject = hit.collider.gameObject;
                if (hoverObject != previousObject)
                {
                    Debug.Log(hit.collider.gameObject.name);
                    if (previousObject != null && (previousObject.tag == "Facility" || previousObject.tag == "Habitat"))
                    {
                        previousObject.GetComponent<SpriteRenderer>().enabled = false;
                    }
                    if (hoverObject.tag == "Facility" || hoverObject.tag == "Habitat")
                    {
                        hoverObject.GetComponent<SpriteRenderer>().enabled = true;
                    }
                    previousObject = hoverObject;
                }
                if (Input.GetMouseButton(0))
                {
                    if (hoverObject.tag == "Facility")
                    {
                        int worldScriptIndex = worldScript.objects.IndexOf(hoverObject);

                        worldScript.objects.RemoveAt(worldScriptIndex);
                        worldScript.names.RemoveAt(worldScriptIndex);
                        worldScript.x_positions.RemoveAt(worldScriptIndex);
                        worldScript.y_positions.RemoveAt(worldScriptIndex);
                        worldScript.sizes.RemoveAt(worldScriptIndex);

                        Destroy(hoverObject);
                        isDeleting = false;
                    }
                    else if (hoverObject.tag == "Habitat")
                    {
                        hoverObject.GetComponent<HabitatStats>().Destroy();
                        isDeleting = false;
                    }
                }
            }
        }
        else //if deleting is false
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (EventSystem.current.IsPointerOverGameObject() == false) //if mouse not over UI
                {
                    //remove active UI panels
                    shopScript.ToggleShop("off");

                    RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

                    //if something is hit by raycast
                    if (hit.collider.gameObject != null)
                    {
                        Debug.DrawRay(Camera.main.ScreenToWorldPoint(Input.mousePosition), hit.point, Color.red, 10.0f);
                        Debug.Log(hit.collider.gameObject.tag);
                        if (hit.collider.gameObject.tag == "Habitat")
                        {
                            //SpawnAnimal spawnScript = hit.collider.gameObject.GetComponent<SpawnAnimal>();
                            //spawnScript.Spawn(pigPrefab, statsCanvasPrefab);
                            habitatUIScript.currentHabitat = hit.collider.gameObject;
                            habitatUIScript.TogglePanel("");
                        }
                        else
                        {
                            habitatUIScript.TogglePanel("down");
                        }

                        if (hit.collider.gameObject.tag == "Animal")
                        {
                            AnimalStats animalScript = hit.collider.gameObject.GetComponent<AnimalStats>();
                            animalScript.ToggleStatsPanel();
                        }
                    }
                }
                else
                {
                    Debug.Log("raycast blocked");
                }
            }
        }
        
    }

    public void ToggleDelete()
    {
        if (isDeleting == true)
        {
            isDeleting = false;
        }
        else
        {
            isDeleting = true;
        }
    }
}
