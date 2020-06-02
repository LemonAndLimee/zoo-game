﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class HabitatUIManagement : MonoBehaviour
{
    public Animation anim;

    public Text habitatTypeText;
    public Text habitatSizeText;

    public GameObject pigPrefab;
    public GameObject llamaPrefab;
    public GameObject zebraPrefab;
    public GameObject lionPrefab;

    public GameObject statsCanvasPrefab;
    public GameObject warningIconPrefab;

    public Sprite pigImage;
    public Sprite llamaImage;
    public Sprite zebraImage;
    public Sprite lionImage;

    public GameObject currentHabitat;
    public HabitatStats statsScript;
    public string currentType;
    public string currentSize;

    public bool panelUp;

    public int animalCount;
    public GameObject[] animalPanels;

    public StaffManagement staffScript;

    public MoneyLogic moneyScript;

    public bool isBabyMode;
    public GameObject babyButton;
    public GameObject adultButton;

    public Color nonSelected;
    public Color selected;

    public GameObject animalNamePanel;
    public InputField nameInput;
    public string animalName;
    public int animalNumber;

    public WorldManagement worldScript;

    public Text[] animalButtonTexts;

    // Update is called once per frame
    void Update()
    {
        if (currentHabitat != null)
        {
            string lowercase = currentHabitat.name.ToLower();
            if (lowercase.Contains("farm"))
            {
                currentType = "Farm";
            }
            if (lowercase.Contains("savannah"))
            {
                currentType = "Savannah";
            }
            if (lowercase.Contains("small"))
            {
                currentSize = "Small";
            }
            if (lowercase.Contains("medium"))
            {
                currentSize = "Medium";
            }
            if (lowercase.Contains("large"))
            {
                currentSize = "Large";
            }

            statsScript = currentHabitat.GetComponent<HabitatStats>();

            for (int i = 0; i < animalButtonTexts.Count(); i++)
            {
                animalButtonTexts[i].text = statsScript.possibleAnimals[i];
            }

            //controls how many animal panels are enabled, depending on no. of animals in the habitat
            animalCount = statsScript.animals.Count;

            for (int i = 0; i < animalPanels.Count(); i++)
            {
                if (i < animalCount)
                {
                    animalPanels[i].SetActive(true);

                    //assign correct values into sliders
                    Slider foodSlider = animalPanels[i].transform.Find("FoodSlider").GetComponent<Slider>();
                    Slider waterSlider = animalPanels[i].transform.Find("WaterSlider").GetComponent<Slider>();

                    Text nameText = animalPanels[i].transform.Find("Name").GetComponent<Text>();

                    Image animalImage = animalPanels[i].transform.Find("AnimalImage").GetComponent<Image>();

                    GameObject currentAnimal = statsScript.animals[i];
                    foodSlider.value = currentAnimal.GetComponent<AnimalStats>().foodLevel / 100f;
                    waterSlider.value = currentAnimal.GetComponent<AnimalStats>().waterLevel / 100f;

                    nameText.text = currentAnimal.GetComponent<AnimalStats>().animalName;

                    if (currentAnimal.GetComponent<AnimalStats>().animalType == "Pig")
                    {
                        animalImage.sprite = pigImage;
                        Color c = new Color();
                        ColorUtility.TryParseHtmlString("#FACCE1", out c);
                        animalImage.color = c;
                    }
                    else if (currentAnimal.GetComponent<AnimalStats>().animalType == "Llama")
                    {
                        animalImage.sprite = llamaImage;
                        Color c = new Color();
                        ColorUtility.TryParseHtmlString("#8C6C0B", out c);
                        animalImage.color = c;
                    }
                    else if (currentAnimal.GetComponent<AnimalStats>().animalType == "Zebra")
                    {
                        animalImage.sprite = zebraImage;
                        Color c = new Color();
                        ColorUtility.TryParseHtmlString("#FFFFFF", out c);
                        animalImage.color = c;
                    }
                    else if (currentAnimal.GetComponent<AnimalStats>().animalType == "Lion")
                    {
                        animalImage.sprite = lionImage;
                        Color c = new Color();
                        ColorUtility.TryParseHtmlString("#F5EA54", out c);
                        animalImage.color = c;
                    }
                    
                }
                else
                {
                    animalPanels[i].SetActive(false);
                }
            }
        }

        if (isBabyMode == true)
        {
            babyButton.GetComponent<Button>().image.color = selected;
            adultButton.GetComponent<Button>().image.color = nonSelected;
        }
        else
        {
            babyButton.GetComponent<Button>().image.color = nonSelected;
            adultButton.GetComponent<Button>().image.color = selected;
        }

        habitatTypeText.text = currentType;
        habitatSizeText.text = currentSize;
    }

    public void TogglePanel(string state)
    {
        if (panelUp == true && state == "down")
        {
            anim.Play("SlideDown");
            panelUp = false;
            //Debug.Log("1");
        }
        else if (panelUp == true)
        {
            anim.Play("SlideDown");
            panelUp = false;
            //Debug.Log("2");
        }
        else if (panelUp == false && state != "down")
        {
            anim.Play("SlideUp");
            panelUp = true;
            //Debug.Log("3");
        }
    }

    public void DestroyButton()
    {
        TogglePanel("down");
        statsScript.Destroy();
    }

    public void BabyMode()
    {
        isBabyMode = true;
    }
    public void AdultMode()
    {
        isBabyMode = false;
    }

    public void ShowAnimalNamePanel()
    {
        if (statsScript.spaceLeft >= 1)
        {
            animalNamePanel.SetActive(true);
            Animation anim = animalNamePanel.GetComponent<Animation>();
            anim.Play("FadeIn");
            PlayerMovement movementScript = GameObject.Find("Player").GetComponent<PlayerMovement>();
            movementScript.enabled = false;
        }

    }

    public void SubmitName()
    {
        animalName = nameInput.text;
        Animation anim = animalNamePanel.GetComponent<Animation>();
        anim.Play("FadeOut");
        Invoke("DeactivateAnimalNamePanel", 0.5f);
        PlayerMovement movementScript = GameObject.Find("Player").GetComponent<PlayerMovement>();
        movementScript.enabled = true;

        Animal(animalName);
    }
    public void DeactivateAnimalNamePanel()
    {
        animalNamePanel.SetActive(false);
    }

    public void SpawnAnimalZero()
    {
        animalNumber = 0;
        ShowAnimalNamePanel();
    }
    public void SpawnAnimalOne()
    {
        animalNumber = 1;
        ShowAnimalNamePanel();
    }
    public void Animal(string name)
    {
        string[] possibleAnimals = statsScript.possibleAnimals.ToArray();
        string animal = possibleAnimals[animalNumber];

        int age;

        if (animal.Contains("Pig"))
        {
            if (isBabyMode == false)
            {
                age = PigStats.adultThreshold;
                moneyScript.balance -= PigStats.costs[1];
            }
            else
            {
                age = 0;
                moneyScript.balance -= PigStats.costs[0];
            }

            SpawnAnimal spawnScript = currentHabitat.GetComponent<SpawnAnimal>();
            spawnScript.Spawn(pigPrefab, age, name, statsCanvasPrefab, warningIconPrefab);
        }
        else if (animal.Contains("Llama"))
        {
            if (isBabyMode == false)
            {
                age = LlamaStats.adultThreshold;
                moneyScript.balance -= LlamaStats.costs[1];
            }
            else
            {
                age = 0;
                moneyScript.balance -= LlamaStats.costs[0];
            }

            SpawnAnimal spawnScript = currentHabitat.GetComponent<SpawnAnimal>();
            spawnScript.Spawn(llamaPrefab, age, name, statsCanvasPrefab, warningIconPrefab);
        }
        else if (animal.Contains("Zebra"))
        {
            if (isBabyMode == false)
            {
                age = ZebraStats.adultThreshold;
                moneyScript.balance -= ZebraStats.costs[1];
            }
            else
            {
                age = 0;
                moneyScript.balance -= ZebraStats.costs[0];
            }

            SpawnAnimal spawnScript = currentHabitat.GetComponent<SpawnAnimal>();
            spawnScript.Spawn(zebraPrefab, age, name, statsCanvasPrefab, warningIconPrefab);
        }
        else if (animal.Contains("Lion"))
        {
            if (isBabyMode == false)
            {
                age = LionStats.adultThreshold;
                moneyScript.balance -= LionStats.costs[1];
            }
            else
            {
                age = 0;
                moneyScript.balance -= LionStats.costs[0];
            }

            SpawnAnimal spawnScript = currentHabitat.GetComponent<SpawnAnimal>();
            spawnScript.Spawn(lionPrefab, age, name, statsCanvasPrefab, warningIconPrefab);
        }
    }

    public void Hire0()
    {
        HireWorker(0);
    }
    public void Hire1()
    {
        HireWorker(1);
    }
    public void Hire2()
    {
        HireWorker(2);
    }
    public void Hire3()
    {
        HireWorker(3);
    }

    public void HireWorker(int index)
    {
        statsScript = currentHabitat.GetComponent<HabitatStats>();
        GameObject animal = statsScript.animals[index];
        staffScript.AddWorker(animal, false);
    }

    public void Feed0()
    {
        FeedAnimal(0);
    }
    public void Feed1()
    {
        FeedAnimal(1);
    }
    public void Feed2()
    {
        FeedAnimal(2);
    }
    public void Feed3()
    {
        FeedAnimal(3);
    }

    public void FeedAnimal(int index)
    {
        statsScript = currentHabitat.GetComponent<HabitatStats>();
        GameObject animal = statsScript.animals[index];
        AnimalStats animalScript = animal.GetComponent<AnimalStats>();
        animalScript.Feed();
    }
}
