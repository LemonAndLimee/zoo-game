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

    public GameObject[] habitatPanels;

    public int currentHabitatType;

    void Start()
    {
        SwitchToFarms();
        currentHabitatType = 0;
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
                if (staffScript.workers[i].animalsToFeed[x] != null)
                {
                    images[x].SetActive(true);

                    Text animalNameText = images[x].transform.Find("Name").GetComponent<Text>();
                    animalNameText.text = staffScript.workers[i].animalsToFeed[x].GetComponent<AnimalStats>().animalName;

                    int animalIndex = staffScript.workers[i].animalsToFeed[x].GetComponent<AnimalStats>().animalIndex;
                    images[x].GetComponent<Image>().sprite = prefabScript.animalImages[animalIndex];
                    images[x].GetComponent<Image>().color = prefabScript.animalColours[animalIndex];
                }
                else
                {
                    staffScript.workers[i].animalsToFeed.RemoveAt(x);
                }
                
            }

            workerPanels[i].SetActive(true);
            Text nameText = workerPanels[i].transform.Find("Name").GetComponent<Text>();
            nameText.text = staffScript.workers[i].name;
            Text salaryText = workerPanels[i].transform.Find("Salary").GetComponent<Text>();
            salaryText.text = "$" + staffScript.workers[i].dailySalary + "/day";
        }

        for (int i = 0; i < habitatPanels.Count(); i++)
        {
            habitatPanels[i].GetComponent<Button>().image.sprite = prefabScript.habitatImages[currentHabitatType];
            Text nameText = habitatPanels[i].transform.Find("Name").GetComponent<Text>();
            nameText.text = prefabScript.habitatTypeNames[currentHabitatType] + " Habitat";
            Text priceText = habitatPanels[i].transform.Find("Price").GetComponent<Text>();
            int price = prefabScript.habitatScripts[currentHabitatType].costPerUnit * prefabScript.habitatScripts[currentHabitatType].sizes[i];
            priceText.text = "$" + price.ToString();
            Text sizeText = habitatPanels[i].transform.Find("Size").GetComponent<Text>();
            if (i == 0)
            {
                sizeText.text = "Small";
            }
            else if (i == 1)
            {
                sizeText.text = "Medium";
            }
            else if (i == 2)
            {
                sizeText.text = "Large";
            }
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
        currentHabitatType = 0;
    }
    public void SwitchToSavannah()
    {
        currentHabitatType = 1;
    }
    public void SwitchToJungle()
    {
        currentHabitatType = 2;
    }
    public void SwitchToArctic()
    {
        currentHabitatType = 3;
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

    public void SmallHabitat()
    {
        int habitatIndex = System.Array.IndexOf(prefabScript.habitatNames, "small" + prefabScript.habitatTypeNames[currentHabitatType]);
        int typeIndex = currentHabitatType;
        Habitat(habitatIndex, typeIndex, 0);
    }
    public void MediumHabitat()
    {
        int habitatIndex = System.Array.IndexOf(prefabScript.habitatNames, "medium" + prefabScript.habitatTypeNames[currentHabitatType]);
        int typeIndex = currentHabitatType;
        Habitat(habitatIndex, typeIndex, 1);
    }
    public void LargeHabitat()
    {
        int habitatIndex = System.Array.IndexOf(prefabScript.habitatNames, "large" + prefabScript.habitatTypeNames[currentHabitatType]);
        int typeIndex = currentHabitatType;
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
