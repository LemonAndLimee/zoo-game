using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class HabitatUIManagement : MonoBehaviour
{
    public Animation anim;

    public Text habitatTypeText;
    public Text habitatSizeText;

    public PrefabsManagement prefabScript;

    public GameObject statsCanvasPrefab;
    public GameObject warningIconPrefab;

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
    public Text[] priceTexts;

    public GameObject infoPanel;

    public Image[] infoPanelStars;
    public Color ratingGreen;
    public Color ratingGrey;

    // Update is called once per frame
    void Update()
    {
        if (currentHabitat != null)
        {
            for (int i = 0; i < prefabScript.habitatTypeNames.Count(); i++)
            {
                if (currentHabitat.name.Contains(prefabScript.habitatTypeNames[i]))
                {
                    currentType = prefabScript.habitatTypeNames[i];
                }
            }

            string lowercase = currentHabitat.name.ToLower();

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
                animalButtonTexts[i].text = prefabScript.habitatScripts[statsScript.habitatTypeIndex].animalTexts[i];

                int currentAnimalIndex = Array.IndexOf(prefabScript.animalNames, statsScript.possibleAnimals[i]);
                int animalSize = 0;
                if (isBabyMode == false)
                {
                    animalSize = 1;
                }
                priceTexts[i].text = "$" + prefabScript.scripts[currentAnimalIndex].costs[animalSize].ToString();
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

                    int animalIndex = currentAnimal.GetComponent<AnimalStats>().animalIndex;
                    animalImage.sprite = prefabScript.animalImages[animalIndex];
                    animalImage.color = prefabScript.animalColours[animalIndex];
                    
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
        animalNamePanel.SetActive(true);
        Animation anim = animalNamePanel.GetComponent<Animation>();
        anim.Play("FadeIn");
        PlayerMovement movementScript = GameObject.Find("Player").GetComponent<PlayerMovement>();
        movementScript.enabled = false;

    }

    public void CheckCapacity()
    {
        string[] possibleAnimals = statsScript.possibleAnimals.ToArray();
        string animal = possibleAnimals[animalNumber];
        int animalIndex = Array.IndexOf(prefabScript.animalNames, animal);

        if (statsScript.spaceLeft >= prefabScript.scripts[animalIndex].capacityPoints)
        {
            ShowAnimalNamePanel();
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
        CheckCapacity();
    }
    public void SpawnAnimalOne()
    {
        animalNumber = 1;
        CheckCapacity();
    }
    public void Animal(string name)
    {
        string[] possibleAnimals = statsScript.possibleAnimals.ToArray();
        string animal = possibleAnimals[animalNumber];

        animal = animal.Replace(" ", string.Empty);

        int animalIndex = Array.IndexOf(prefabScript.animalNames, animal);

        int age;

        if (isBabyMode == true)
        {
            age = 0;
            moneyScript.balance -= prefabScript.scripts[animalIndex].costs[0];
        }
        else
        {
            age = prefabScript.scripts[animalIndex].adultThreshold;
            moneyScript.balance -= prefabScript.scripts[animalIndex].costs[1];
        }

        int capacityPoints = prefabScript.scripts[animalIndex].capacityPoints;

        SpawnAnimal spawnScript = currentHabitat.GetComponent<SpawnAnimal>();
        spawnScript.Spawn(prefabScript.animals[animalIndex], age, name, animalIndex, statsCanvasPrefab, warningIconPrefab, capacityPoints);
    }

    public void Info0()
    {
        ShowInfo(0);
    }
    public void Info1()
    {
        ShowInfo(1);
    }

    public void ShowInfo(int possibleAnimalIndex)
    {
        infoPanel.SetActive(true);

        statsScript = currentHabitat.GetComponent<HabitatStats>();
        int index = Array.IndexOf(prefabScript.animalNames, statsScript.possibleAnimals[possibleAnimalIndex]);
        Animal animalScript = prefabScript.scripts[index];

        Text nameText = infoPanel.transform.Find("Name").GetComponentInChildren<Text>();
        nameText.text = statsScript.possibleAnimals[possibleAnimalIndex];

        Image animalImage = infoPanel.transform.Find("AnimalImage").GetComponent<Image>();
        animalImage.sprite = prefabScript.animalImages[index];
        animalImage.color = prefabScript.animalColours[index];

        Text habitatText = infoPanel.transform.Find("HabitatText").GetComponent<Text>();
        habitatText.text = prefabScript.habitatTypeNames[statsScript.habitatTypeIndex];

        Text priceText = infoPanel.transform.Find("PriceText").GetComponent<Text>();
        priceText.text = "$" + animalScript.costs[1].ToString();

        Text costToFeedText = infoPanel.transform.Find("CostToFeedText").GetComponent<Text>();
        costToFeedText.text = animalScript.costToFeed.ToString();

        Text feedingText = infoPanel.transform.Find("FeedingText").GetComponent<Text>();
        int x = Mathf.RoundToInt(100 / animalScript.thirstPerDay) + 1;
        feedingText.text = "Every " + x + " days";

        for (int i = 0; i < infoPanelStars.Count(); i++)
        {
            if (i < animalScript.interestRating)
            {
                infoPanelStars[i].color = ratingGreen;
            }
            else
            {
                infoPanelStars[i].color = ratingGrey;
            }
        }
    }
    public void HideInfo()
    {
        infoPanel.SetActive(false);
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
    public void Hire4()
    {
        HireWorker(4);
    }
    public void Hire5()
    {
        HireWorker(5);
    }
    public void Hire6()
    {
        HireWorker(6);
    }
    public void Hire7()
    {
        HireWorker(7);
    }
    public void Hire8()
    {
        HireWorker(8);
    }
    public void Hire9()
    {
        HireWorker(9);
    }
    public void Hire10()
    {
        HireWorker(10);
    }
    public void Hire11()
    {
        HireWorker(11);
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
    public void Feed4()
    {
        FeedAnimal(4);
    }
    public void Feed5()
    {
        FeedAnimal(5);
    }
    public void Feed6()
    {
        FeedAnimal(6);
    }
    public void Feed7()
    {
        FeedAnimal(7);
    }
    public void Feed8()
    {
        FeedAnimal(8);
    }
    public void Feed9()
    {
        FeedAnimal(9);
    }
    public void Feed10()
    {
        FeedAnimal(10);
    }
    public void Feed11()
    {
        FeedAnimal(11);
    }

    public void FeedAnimal(int index)
    {
        statsScript = currentHabitat.GetComponent<HabitatStats>();
        GameObject animal = statsScript.animals[index];
        AnimalStats animalScript = animal.GetComponent<AnimalStats>();
        animalScript.Feed(false);
    }

    public void Sell0()
    {
        SellAnimal(0);
    }
    public void Sell1()
    {
        SellAnimal(1);
    }
    public void Sell2()
    {
        SellAnimal(2);
    }
    public void Sell3()
    {
        SellAnimal(3);
    }
    public void Sell4()
    {
        SellAnimal(4);
    }
    public void Sell5()
    {
        SellAnimal(5);
    }
    public void Sell6()
    {
        SellAnimal(6);
    }
    public void Sell7()
    {
        SellAnimal(7);
    }
    public void Sell8()
    {
        SellAnimal(8);
    }
    public void Sell9()
    {
        SellAnimal(9);
    }
    public void Sell10()
    {
        SellAnimal(10);
    }
    public void Sell11()
    {
        SellAnimal(11);
    }

    public void SellAnimal(int index)
    {
        statsScript = currentHabitat.GetComponent<HabitatStats>();
        GameObject animal = statsScript.animals[index];
        AnimalStats animalScript = animal.GetComponent<AnimalStats>();
        if (animalScript.isAdult == true)
        {
            moneyScript.balance += (prefabScript.scripts[animalScript.animalIndex].costs[1]) / 2;
        }
        else
        {
            moneyScript.balance += prefabScript.scripts[animalScript.animalIndex].costs[0] / 2;
        }
        animalScript.Delete();
    }
}
