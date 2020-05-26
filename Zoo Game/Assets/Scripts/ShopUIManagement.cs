using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

public class ShopUIManagement : MonoBehaviour
{
    public Animation anim;

    public bool isShopUp;
    public bool isFacilities;
    public bool isHabitats;

    public GameObject facilitiesPanel;
    public GameObject habitatsPanel;

    public GameObject activeSubPanel;

    public GameObject pathsPanel;
    public GameObject farmPanel;

    public PlacingLogic placeScript;

    //different prefabs
    public GameObject[] stonePathPrefabs;

    public GameObject smallFarmPrefab;
    public GameObject mediumFarmPrefab;
    public GameObject largeFarmPrefab;

    public GameObject prefab;

    public void FacilitiesButton()
    {
        ToggleShop("f");
        if (isHabitats == true)
        {
            ToggleHabitats("");
        }
        ToggleFacilities("true");
    }
    public void HabitatsButton()
    {
        ToggleShop("h");
        if (isFacilities == true)
        {
            ToggleFacilities("");
        }
        ToggleHabitats("true");
    }

    public void ToggleShop(string buttonPressed)
    {
        if (isShopUp == false)
        {
            anim.Play("SlideUp");
            isShopUp = true;
        }
        else if (buttonPressed == "f" && isFacilities == true)
        {
            anim.Play("SlideDown");
            isShopUp = false;
            EventSystem.current.SetSelectedGameObject(null);
        }
        else if (buttonPressed == "h" && isHabitats == true)
        {
            anim.Play("SlideDown");
            isShopUp = false;
            EventSystem.current.SetSelectedGameObject(null);
        }
        //if entering place mode
        else if (buttonPressed == "place")
        {
            anim.Play("SlideDown");
            isShopUp = false;
            EventSystem.current.SetSelectedGameObject(null);
        }
    }

    public void ToggleFacilities(string state)
    {
        if (isFacilities == false || state == "true")
        {
            facilitiesPanel.SetActive(true);
            isFacilities = true;
            SwitchToPaths();
        }
        else if (isFacilities == true || state == "false")
        {
            facilitiesPanel.SetActive(false);
            isFacilities = false;
        }
    }

    public void ToggleHabitats(string state)
    {
        if (isHabitats == false || state == "true")
        {
            isHabitats = true;
            habitatsPanel.SetActive(true);
            SwitchToFarms();
        }
        else if (isHabitats == true || state == "false")
        {
            isHabitats = false;
            habitatsPanel.SetActive(false);
        }
    }


    //different facilites categories
    public void SwitchToPaths()
    {
        if (activeSubPanel != pathsPanel)
        {
            activeSubPanel = pathsPanel;
            pathsPanel.SetActive(true);
        }
    }

    public void SwitchToFarms()
    {
        if (activeSubPanel != farmPanel)
        {
            activeSubPanel = farmPanel;
            farmPanel.SetActive(true);
        }
    }


    //different purchase options
    public void StonePath()
    {
        int num = Random.Range(0, stonePathPrefabs.Length);
        prefab = stonePathPrefabs[num];
        placeScript.currentCost = 2;
        placeScript.TogglePlacing(prefab, true, true, true);
        placeScript.continousPrefabs = stonePathPrefabs.ToList();
        ToggleShop("place");
    }

    public void SmallFarmHabitat()
    {
        prefab = smallFarmPrefab;
        placeScript.currentCost = FarmEnclosureInfo.costPerUnit * FarmEnclosureInfo.sizes[0];
        placeScript.TogglePlacing(prefab, false, false, false);
        ToggleShop("place");
    }
    public void MediumFarmHabitat()
    {
        prefab = mediumFarmPrefab;
        placeScript.currentCost = FarmEnclosureInfo.costPerUnit * FarmEnclosureInfo.sizes[1];
        placeScript.TogglePlacing(prefab, false, false, false);
        ToggleShop("place");
    }
    public void LargeFarmHabitat()
    {
        prefab = largeFarmPrefab;
        placeScript.currentCost = FarmEnclosureInfo.costPerUnit * FarmEnclosureInfo.sizes[2];
        placeScript.TogglePlacing(prefab, false, false, false);
        ToggleShop("place");
    }
}
