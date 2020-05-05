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

    public GameObject currentHabitat;
    public HabitatStats statsScript;
    public string currentType;
    public string currentSize;

    public bool panelUp;

    public int animalCount;
    public GameObject[] animalPanels;

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

    public void SpawnAnimalZero()
    {
        string[] possibleAnimals = statsScript.possibleAnimals;
        string animal = possibleAnimals[0];

        if (animal == "Pig")
        {
            SpawnAnimal spawnScript = currentHabitat.GetComponent<SpawnAnimal>();
            spawnScript.Spawn(pigPrefab, statsCanvasPrefab);
        }
    }
}
