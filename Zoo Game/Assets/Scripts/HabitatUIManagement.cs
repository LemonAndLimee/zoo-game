using System.Collections;
using System.Collections.Generic;
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
        string[] animals = statsScript.possibleAnimals;
        string animal = animals[0];

        if (animal == "Pig")
        {
            SpawnAnimal spawnScript = currentHabitat.GetComponent<SpawnAnimal>();
            spawnScript.Spawn(pigPrefab, statsCanvasPrefab);
        }
    }
}
