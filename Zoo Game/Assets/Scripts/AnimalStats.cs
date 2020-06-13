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
    public GameObject warningIcon;

    public int daysTilDeath;
    public bool isAlive = true;
    public bool isDanger;
    public int zeroHealthCounter;

    public NotificationManagement notificationScript;

    public PrefabsManagement prefabScript;

    public string animalType;

    public int age; //days
    public bool isAdult;
    public int adultThreshold;

    public float babySize;
    public float adultSize;

    public string animalName;

    public int costToFeed;

    public int capacityPoints;

    public int animalIndex; //used for inheriting values from prefab script

    public bool isPredator;
    public string[] foodAnimals;

    // Start is called before the first frame update
    void Start()
    {
        prefabScript = GameObject.Find("PrefabManager").GetComponent<PrefabsManagement>();

        notificationScript = GameObject.Find("GameManager").GetComponent<NotificationManagement>();

        childStatsPanel.SetActive(false);
        Text nameText = childStatsPanel.transform.Find("Name").GetComponent<Text>();
        nameText.text = animalName;

        warningIcon.SetActive(false);

        foodLevel = 100;
        waterLevel = 100;

        dayScript = GameObject.Find("GameManager").GetComponent<DayLogic>();
        day = dayScript.dayCount;
        previousDay = dayScript.dayCount;

        //inherits correct values
        animalType = prefabScript.animalNames[animalIndex];
        hungerPerDay = prefabScript.scripts[animalIndex].hungerPerDay;
        thirstPerDay = prefabScript.scripts[animalIndex].thirstPerDay;
        interestRating = prefabScript.scripts[animalIndex].interestRating;
        daysTilDeath = prefabScript.scripts[animalIndex].daysTilDeath;
        adultThreshold = prefabScript.scripts[animalIndex].adultThreshold;
        adultSize = prefabScript.scripts[animalIndex].adultSize;
        babySize = prefabScript.scripts[animalIndex].babySize;
        costToFeed = prefabScript.scripts[animalIndex].costToFeed;
        capacityPoints = prefabScript.scripts[animalIndex].capacityPoints;
        isPredator = prefabScript.scripts[animalIndex].isPredator;
        if (isPredator == true)
        {
            foodAnimals = prefabScript.scripts[animalIndex].foodAnimals;
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
        if (isAlive == false) //if dead
        {
            isDanger = false;
            warningIcon.SetActive(false);
            AnimalMovement movementScript = gameObject.GetComponent<AnimalMovement>();
            movementScript.enabled = false;
            Rigidbody2D rb = gameObject.GetComponent<Rigidbody2D>();
            rb.freezeRotation = true;
            gameObject.GetComponent<BoxCollider2D>().enabled = false;

            Animation anim = gameObject.GetComponent<Animation>();
            anim.Play();
            Invoke("Delete", 6f);

            CustomerManagement customerScript = GameObject.Find("GameManager").GetComponent<CustomerManagement>();
            customerScript.reputation -= 10;
        }
        else
        {
            worldScriptIndex = worldScript.animals.IndexOf(gameObject);

            if (age >= adultThreshold)
            {
                isAdult = true;
                gameObject.transform.localScale = new Vector3(adultSize, adultSize, 1f);
            }
            else
            {
                isAdult = false;
                gameObject.transform.localScale = new Vector3(babySize, babySize, 1f);

            }

            UpdateStatsSliders();

            worldScript.ages[worldScriptIndex] = age;
            //Debug.Log("index - " + worldScriptIndex);
            //Debug.Log("count - " + worldScript.animalFoodLevels.Count);
            worldScript.animal_x_positions[worldScriptIndex] = transform.position.x;
            worldScript.animal_y_positions[worldScriptIndex] = transform.position.y;
            worldScript.animal_z_rotations[worldScriptIndex] = transform.eulerAngles.z;

            worldScript.animalFoodLevels[worldScriptIndex] = foodLevel;
            worldScript.animalWaterLevels[worldScriptIndex] = waterLevel;

            worldScript.animalZeroCounters[worldScriptIndex] = zeroHealthCounter;
            worldScript.isAlive[worldScriptIndex] = isAlive;

            if (foodLevel <= 0 || waterLevel <= 0) //elif in danger zone
            {
                isDanger = true;
                if (warningIcon.activeInHierarchy == false)
                {
                    notificationScript.target = gameObject;
                    notificationScript.AddNotification("Warning");
                    notificationScript.animals.Add(gameObject);
                }
                warningIcon.SetActive(true);
            }
            else //if healthy
            {
                isDanger = false;
                warningIcon.SetActive(false);
            }
        }

    }

    public void Day()
    {
        Debug.Log("Day");
        foodLevel -= hungerPerDay;
        waterLevel -= thirstPerDay;

        age += 1;

        if (foodLevel <= 0 || waterLevel <= 0)
        {
            Debug.Log("danger");
            zeroHealthCounter += 1;

            if (zeroHealthCounter > daysTilDeath)
            {
                Debug.Log(gameObject.name + " died"); //death notification
                isAlive = false;

                notificationScript.DeathMessage(animalType, animalName, "died of starvation");

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

    public void Feed(bool eaten)
    {
        if (isAlive == true)
        {
            foodLevel = 100;
            waterLevel = 100;

            Debug.Log("feed");
            if (eaten == false)
            {
                MoneyLogic moneyScript = GameObject.Find("GameManager").GetComponent<MoneyLogic>();
                moneyScript.balance -= costToFeed;
            }
        }
    }

    public void Delete()
    {
        worldScriptIndex = worldScript.animals.IndexOf(gameObject);

        Debug.Log("delete");
        worldScript.animals.RemoveAt(worldScriptIndex);
        worldScript.animalGivenNames.RemoveAt(worldScriptIndex);
        worldScript.ages.RemoveAt(worldScriptIndex);
        worldScript.hasWorker.RemoveAt(worldScriptIndex);
        worldScript.animalNames.RemoveAt(worldScriptIndex);
        worldScript.animal_x_positions.RemoveAt(worldScriptIndex);
        worldScript.animal_y_positions.RemoveAt(worldScriptIndex);
        worldScript.animal_z_rotations.RemoveAt(worldScriptIndex);
        worldScript.animalFoodLevels.RemoveAt(worldScriptIndex);
        worldScript.animalWaterLevels.RemoveAt(worldScriptIndex);
        worldScript.animalZeroCounters.RemoveAt(worldScriptIndex);
        worldScript.isAlive.RemoveAt(worldScriptIndex);

        HabitatStats habitatStats = worldScript.habitats[worldScript.animalHabitatIndexes[worldScriptIndex]].GetComponent<HabitatStats>();
        habitatStats.animals.Remove(gameObject);
        worldScript.animalHabitatIndexes.RemoveAt(worldScriptIndex);

        Destroy(childStatsPanel.transform.parent.gameObject);
        Destroy(warningIcon);

        Destroy(gameObject);
    }

}
