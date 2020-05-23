using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class AnimalStats : MonoBehaviour
{
    public int foodLevel;
    public int waterLevel;
    public int interestRating;

    public int hungerPerDay;
    public int thirstPerDay;

    public DayLogic dayScript;

    public int day;
    public int previousDay;

    public StartManagement startScript;
    public WorldManagement worldScript;
    public int worldScriptIndex;

    public GameObject childStatsPanel;

    public int daysTilDeath;
    public bool isAlive = true;
    public int zeroHealthCounter;

    public Color healthyColour;
    public Color dangerColour;
    public Color deadColour;

    // Start is called before the first frame update
    void Start()
    {
        childStatsPanel.SetActive(false);

        foodLevel = 100;
        waterLevel = 100;

        dayScript = GameObject.Find("GameManager").GetComponent<DayLogic>();
        day = dayScript.dayCount;
        previousDay = dayScript.dayCount;

        //inherits correct values
        if (gameObject.name.Contains("Pig"))
        {
            hungerPerDay = PigStats.hungerPerDay;
            thirstPerDay = PigStats.thirstPerDay;

            interestRating = PigStats.interestRating;

            daysTilDeath = PigStats.daysTilDeath;

            healthyColour = PigStats.healthyColour;
            dangerColour = PigStats.dangerColour;
            deadColour = PigStats.deadColour;
        }

        startScript = GameObject.FindGameObjectWithTag("StartManager").GetComponent<StartManagement>();
        worldScript = GameObject.Find("GameManager").GetComponent<WorldManagement>();


        //if world is loaded
        if (startScript.isLoad == true && startScript.isNew == false && worldScript.gameTimer == 0f)
        {
            Debug.Log("load");
            worldScript.Load();
            foodLevel = worldScript.animalFoodLevels[worldScriptIndex];
            waterLevel = worldScript.animalWaterLevels[worldScriptIndex];
        }
    }

    // Update is called once per frame
    void Update()
    {
        UpdateStatsSliders();

        //Debug.Log("index - " + worldScriptIndex);
        //Debug.Log("count - " + worldScript.animalFoodLevels.Count);
        worldScript.animal_x_positions[worldScriptIndex] = transform.position.x;
        worldScript.animal_y_positions[worldScriptIndex] = transform.position.y;
        worldScript.animal_z_rotations[worldScriptIndex] = transform.eulerAngles.z;

        worldScript.animalFoodLevels[worldScriptIndex] = foodLevel;
        worldScript.animalWaterLevels[worldScriptIndex] = waterLevel;

        worldScript.animalZeroCounters[worldScriptIndex] = zeroHealthCounter;
        worldScript.isAlive[worldScriptIndex] = isAlive;

        if (isAlive == false) //if dead
        {
            AnimalMovement movementScript = gameObject.GetComponent<AnimalMovement>();
            movementScript.enabled = false;
            Rigidbody2D rb = gameObject.GetComponent<Rigidbody2D>();
            rb.freezeRotation = true;

            foreach (Transform child in transform)
            {
                SpriteRenderer sr = child.GetComponent<SpriteRenderer>();
                sr.color = deadColour;
            }
            SpriteRenderer spriter = gameObject.GetComponent<SpriteRenderer>();
            spriter.color = deadColour;
        }
        else if (foodLevel <= 0 || waterLevel <= 0) //elif in danger zone
        {
            foreach (Transform child in transform)
            {
                SpriteRenderer sr = child.GetComponent<SpriteRenderer>();
                sr.color = dangerColour;
            }
            SpriteRenderer spriter = gameObject.GetComponent<SpriteRenderer>();
            spriter.color = dangerColour;
        }
        else
        {
            foreach (Transform child in transform)
            {
                SpriteRenderer sr = child.GetComponent<SpriteRenderer>();
                sr.color = healthyColour;
            }
            SpriteRenderer spriter = gameObject.GetComponent<SpriteRenderer>();
            spriter.color = healthyColour;
        }

    }

    public void Day()
    {
        Debug.Log("Day");
        foodLevel -= hungerPerDay;
        waterLevel -= thirstPerDay;

        if (foodLevel <= 0 || waterLevel <= 0)
        {
            Debug.Log("danger");
            zeroHealthCounter += 1;

            if (zeroHealthCounter > daysTilDeath)
            {
                Debug.Log(gameObject.name + " died");
                isAlive = false;
            }
        }
        else
        {
            zeroHealthCounter = 0;
        }
    }

    public void ToggleStatsPanel()
    {
        if (childStatsPanel.activeInHierarchy == true)
        {
            childStatsPanel.SetActive(false);
        }
        else
        {
            childStatsPanel.SetActive(true);
        }
    }

    public void UpdateStatsSliders()
    {
        Slider foodSlider = childStatsPanel.transform.Find("FoodSlider").GetComponent<Slider>();
        Slider waterSlider = childStatsPanel.transform.Find("WaterSlider").GetComponent<Slider>();

        foodSlider.value = foodLevel / 100f;
        waterSlider.value = waterLevel / 100f;
    }

    public void Feed()
    {
        if (isAlive == true)
        {
            foodLevel = 100;
            waterLevel = 100;
        }
    }
}
