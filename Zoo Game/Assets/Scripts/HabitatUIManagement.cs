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

    public GameObject pigPrefab;
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

    // Start is called before the first frame update
    void Start()
    {
    }

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

                    GameObject currentAnimal = statsScript.animals[i];
                    foodSlider.value = currentAnimal.GetComponent<AnimalStats>().foodLevel / 100f;
                    waterSlider.value = currentAnimal.GetComponent<AnimalStats>().waterLevel / 100f;
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

    public void TogglePanel()
    {
        if (panelUp == false)
        {
            anim.Play("SlideUp");
            panelUp = true;
        }
        else
        {
            anim.Play("SlideDown");
            panelUp = false;
        }
    }

    public void BabyMode()
    {
        isBabyMode = true;
    }
    public void AdultMode()
    {
        isBabyMode = false;
    }

    public void SpawnAnimalZero()
    {
        string[] possibleAnimals = statsScript.possibleAnimals.ToArray();
        string animal = possibleAnimals[0];

        int age;

        if (animal.Contains("Pig"))
        {
            if (isBabyMode == false)
            {
                age = PigStats.adultThreshold;
            }
            else
            {
                age = 0;
            }

            SpawnAnimal spawnScript = currentHabitat.GetComponent<SpawnAnimal>();
            spawnScript.Spawn(pigPrefab, age, statsCanvasPrefab, warningIconPrefab);
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
        staffScript.AddWorker(animal);
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
