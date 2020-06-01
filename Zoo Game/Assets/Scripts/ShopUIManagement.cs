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

    public PlacingLogic placeScript;

    //different prefabs
    public GameObject[] stonePathPrefabs;

    public GameObject smallFarmPrefab;
    public GameObject mediumFarmPrefab;
    public GameObject largeFarmPrefab;

    public GameObject smallSavannahPrefab;

    public GameObject prefab;

    public Sprite pigImage;
    public Sprite llamaImage;

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

                if (staffScript.workers[i].animalsToFeed[x].gameObject.name.Contains("Pig"))
                {
                    images[x].GetComponent<Image>().sprite = pigImage;
                    Color c = new Color();
                    ColorUtility.TryParseHtmlString("#FACCE1", out c);
                    images[x].GetComponent<Image>().color = c;
                }
                else if (staffScript.workers[i].animalsToFeed[x].gameObject.name.Contains("Llama"))
                {
                    images[x].GetComponent<Image>().sprite = llamaImage;
                    Color c = new Color();
                    ColorUtility.TryParseHtmlString("#8C6C0B", out c);
                    images[x].GetComponent<Image>().color = c;
                }
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
        else if (buttonPressed == "w" && isWorkers == true)
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

    public void SmallSavannahHabitat()
    {
        prefab = smallSavannahPrefab;
        placeScript.currentCost = SavannahEnclosureInfo.costPerUnit * SavannahEnclosureInfo.sizes[0];
        placeScript.TogglePlacing(prefab, false, false, false);
    }
}
