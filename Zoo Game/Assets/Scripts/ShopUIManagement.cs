using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ShopUIManagement : MonoBehaviour
{
    public Animation anim;

    public bool isShopUp;
    public bool isFacilities;
    public bool isHabitats;
    public bool isWorkers;

    public GameObject facilitiesPanel;
    public GameObject habitatsPanel;
    public GameObject workersPanel;
    public GameObject[] workerPanels;

    public StaffManagement staffScript;

    public GameObject activeSubPanel;

    public GameObject pathsPanel;

    public GameObject farmPanel;
    public GameObject savannahPanel;
    public GameObject junglePanel;
    public GameObject arcticPanel;

    public PlacingLogic placeScript;

    //different prefabs
    public GameObject[] stonePathPrefabs;

    public GameObject prefab;

    public PrefabsManagement prefabScript;

    void Start()
    {
        farmPanel.SetActive(true);
        activeSubPanel = farmPanel;
    }

    void Update()
    {
        for (int i = 0; i < workerPanels.Count(); i++)
        {
            workerPanels[i].SetActive(false);
        }

        for (int i = 0; i < staffScript.workers.Count; i++)
        {
            //creates list of images
            List<GameObject> images = new List<GameObject>();
            foreach (Transform img in workerPanels[i].transform)
            {
                if (img.gameObject.name.Contains("Image"))
                {
                    images.Add(img.gameObject);
                }
            }

            for (int x = 0; x < images.Count; x++)
            {
                images[x].SetActive(false);
            }
            for (int x = 0; x < staffScript.workers[i].animalsToFeed.Count; x++)
            {
                images[x].SetActive(true);

                Text animalNameText = images[x].transform.Find("Name").GetComponent<Text>();
                animalNameText.text = staffScript.workers[i].animalsToFeed[x].GetComponent<AnimalStats>().animalName;

                int animalIndex = staffScript.workers[i].animalsToFeed[x].GetComponent<AnimalStats>().animalIndex;
                images[x].GetComponent<Image>().sprite = prefabScript.animalImages[animalIndex];
                images[x].GetComponent<Image>().color = prefabScript.animalColours[animalIndex];
                
            }

            workerPanels[i].SetActive(true);
            Text nameText = workerPanels[i].transform.Find("Name").GetComponent<Text>();
            nameText.text = staffScript.workers[i].name;
            Text salaryText = workerPanels[i].transform.Find("Salary").GetComponent<Text>();
            salaryText.text = "$" + staffScript.workers[i].dailySalary + "/day";
        }
    }

    public void FacilitiesButton()
    {
        ToggleShop("f");
        if (isHabitats == true)
        {
            ToggleHabitats("");
        }
        else if (isWorkers == true)
        {
            ToggleWorkers("");
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
        else if (isWorkers == true)
        {
            ToggleWorkers("");
        }
        ToggleHabitats("true");
    }
    public void WorkersButton()
    {
        ToggleShop("w");
        if (isHabitats == true)
        {
            ToggleHabitats("");
        }
        else if (isFacilities == true)
        {
            ToggleFacilities("");
        }
        ToggleWorkers("true");
    }

    public void ToggleShop(string buttonPressed)
    {
        if (isShopUp == false && buttonPressed != "off")
        {
            //Debug.Log("up");
            anim.Play("SlideUp");
            isShopUp = true;
        }
        else if (buttonPressed == "off" && isShopUp == true)
        {
            anim.Play("SlideDown");
            isShopUp = false;
            EventSystem.current.SetSelectedGameObject(null);
        }
        else if (buttonPressed == "place")
        {
            anim.Play("SlideDown");
            isShopUp = false;
            EventSystem.current.SetSelectedGameObject(null);
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
        else if (buttonPressed == "w" && isWorkers == true)
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
    public void ToggleWorkers(string state)
    {
        if (isWorkers == false || state == "true")
        {
            isWorkers = true;
            workersPanel.SetActive(true);
        }
        else if (isWorkers == true || state == "true")
        {
            isWorkers = false;
            workersPanel.SetActive(false);
        }
    }


    //different facilites categories
    public void SwitchToPaths()
    {
        if (activeSubPanel != pathsPanel)
        {
            activeSubPanel.SetActive(false);
            activeSubPanel = pathsPanel;
            pathsPanel.SetActive(true);
        }
    }

    public void SwitchToFarms()
    {
        if (activeSubPanel != farmPanel)
        {
            activeSubPanel.SetActive(false);
            activeSubPanel = farmPanel;
            farmPanel.SetActive(true);
        }
    }
    public void SwitchToSavannah()
    {
        if (activeSubPanel != savannahPanel)
        {
            activeSubPanel.SetActive(false);
            activeSubPanel = savannahPanel;
            savannahPanel.SetActive(true);
        }
    }
    public void SwitchToJungle()
    {
        if (activeSubPanel != junglePanel)
        {
            activeSubPanel.SetActive(false);
            activeSubPanel = junglePanel;
            junglePanel.SetActive(true);
        }
    }
    public void SwitchToArctic()
    {
        if (activeSubPanel != arcticPanel)
        {
            activeSubPanel.SetActive(false);
            activeSubPanel = arcticPanel;
            arcticPanel.SetActive(true);
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
        int habitatIndex = System.Array.IndexOf(prefabScript.habitatNames, "smallFarm");
        int typeIndex = System.Array.IndexOf(prefabScript.habitatTypeNames, "Farm");
        Habitat(habitatIndex, typeIndex, 0);
    }
    public void MediumFarmHabitat()
    {
        int habitatIndex = System.Array.IndexOf(prefabScript.habitatNames, "mediumFarm");
        int typeIndex = System.Array.IndexOf(prefabScript.habitatTypeNames, "Farm");
        Habitat(habitatIndex, typeIndex, 1);
    }
    public void LargeFarmHabitat()
    {
        int habitatIndex = System.Array.IndexOf(prefabScript.habitatNames, "largeFarm");
        int typeIndex = System.Array.IndexOf(prefabScript.habitatTypeNames, "Farm");
        Habitat(habitatIndex, typeIndex, 2);
    }

    public void SmallSavannahHabitat()
    {
        int habitatIndex = System.Array.IndexOf(prefabScript.habitatNames, "smallSavannah");
        int typeIndex = System.Array.IndexOf(prefabScript.habitatTypeNames, "Savannah");
        Habitat(habitatIndex, typeIndex, 0);
    }
    public void MediumSavannahHabitat()
    {
        int habitatIndex = System.Array.IndexOf(prefabScript.habitatNames, "mediumSavannah");
        int typeIndex = System.Array.IndexOf(prefabScript.habitatTypeNames, "Savannah");
        Habitat(habitatIndex, typeIndex, 1);
    }
    public void LargeSavannahHabitat()
    {
        int habitatIndex = System.Array.IndexOf(prefabScript.habitatNames, "largeSavannah");
        int typeIndex = System.Array.IndexOf(prefabScript.habitatTypeNames, "Savannah");
        Habitat(habitatIndex, typeIndex, 2);
    }

    public void SmallJungleHabitat()
    {
        int habitatIndex = System.Array.IndexOf(prefabScript.habitatNames, "smallJungle");
        int typeIndex = System.Array.IndexOf(prefabScript.habitatTypeNames, "Jungle");
        Debug.Log(typeIndex);
        Habitat(habitatIndex, typeIndex, 0);
    }
    public void MediumJungleHabitat()
    {
        int habitatIndex = System.Array.IndexOf(prefabScript.habitatNames, "mediumJungle");
        int typeIndex = System.Array.IndexOf(prefabScript.habitatTypeNames, "Jungle");
        Habitat(habitatIndex, typeIndex, 1);
    }
    public void LargeJungleHabitat()
    {
        int habitatIndex = System.Array.IndexOf(prefabScript.habitatNames, "largeJungle");
        int typeIndex = System.Array.IndexOf(prefabScript.habitatTypeNames, "Jungle");
        Habitat(habitatIndex, typeIndex, 2);
    }

    public void SmallArcticHabitat()
    {
        int habitatIndex = System.Array.IndexOf(prefabScript.habitatNames, "smallArctic");
        int typeIndex = System.Array.IndexOf(prefabScript.habitatTypeNames, "Arctic");
        Habitat(habitatIndex, typeIndex, 0);
    }
    public void MediumArcticHabitat()
    {
        int habitatIndex = System.Array.IndexOf(prefabScript.habitatNames, "mediumArctic");
        int typeIndex = System.Array.IndexOf(prefabScript.habitatTypeNames, "Arctic");
        Habitat(habitatIndex, typeIndex, 1);
    }
    public void LargeArcticHabitat()
    {
        int habitatIndex = System.Array.IndexOf(prefabScript.habitatNames, "largeArctic");
        int typeIndex = System.Array.IndexOf(prefabScript.habitatTypeNames, "Arctic");
        Habitat(habitatIndex, typeIndex, 2);
    }

    public void Habitat(int habitatIndex, int typeIndex, int size)
    {
        prefab = prefabScript.habitats[habitatIndex];
        placeScript.currentHabitatIndex = habitatIndex;
        placeScript.currentHabitatTypeIndex = typeIndex;
        placeScript.currentCost = prefabScript.habitatScripts[typeIndex].costPerUnit * prefabScript.habitatScripts[typeIndex].sizes[size];
        placeScript.TogglePlacing(prefab, false, false, false);
        ToggleShop("place");
    }
}
