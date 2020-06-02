﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

//overall manager for selecting objects with mouse
public class SelectionLogic : MonoBehaviour
{
    public HabitatUIManagement habitatUIScript;

    public ShopUIManagement shopScript;

    // Update is called once per frame
    void Update()
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
                    //Debug.Log(hit.collider.gameObject.name);
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
