using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnimalStats : MonoBehaviour
{
    public int foodLevel;
    public int waterLevel;
    public int health;

    public int hungerPerDay;
    public int thirstPerDay;

    public DayLogic dayScript;

    public int day;
    public int previousDay;

    public StartManagement startScript;
    public WorldManagement worldScript;
    public int worldScriptIndex;

    public GameObject childStatsPanel;

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
        }

        startScript = GameObject.FindGameObjectWithTag("StartManager").GetComponent<StartManagement>();
        worldScript = GameObject.Find("GameManager").GetComponent<WorldManagement>();


        //if world is loaded
        if (startScript.isLoad == true && startScript.isNew == false && worldScript.gameTimer == 0f)
        {
            worldScript.Load();
            foodLevel = worldScript.animalFoodLevels[worldScriptIndex];
            waterLevel = worldScript.animalWaterLevels[worldScriptIndex];
        }
    }

    // Update is called once per frame
    void Update()
    {
        UpdateStatsSliders();

        day = dayScript.dayCount;
        if (day > previousDay)
        {
            Day();
            previousDay = day;
        }

        //Debug.Log("index - " + worldScriptIndex);
        //Debug.Log("count - " + worldScript.animalFoodLevels.Count);
        worldScript.animalFoodLevels[worldScriptIndex] = foodLevel;
        worldScript.animalWaterLevels[worldScriptIndex] = waterLevel;
    }

    public void Day()
    {
        Debug.Log("Day");
        foodLevel -= hungerPerDay;
        waterLevel -= thirstPerDay;
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
}
