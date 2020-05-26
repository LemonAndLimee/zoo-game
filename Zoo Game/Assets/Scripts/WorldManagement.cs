using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

//sorts out lists to be stored - also manages the references to subroutines for loading and saving
public class WorldManagement : MonoBehaviour
{
    public List<GameObject> objects = new List<GameObject>();
    public List<string> names = new List<string>();

    public List<float> x_positions = new List<float>();
    public List<float> y_positions = new List<float>();
    public List<float> sizes = new List<float>();

    public int dayCount;
    public float dayTimer;
    public int yearCount;

    public int balance;

    public bool canSave;

    public List<GameObject> animals = new List<GameObject>(); // not saved or loaded, just used for index assignment
    public List<int> ages = new List<int>();
    public List<bool> hasWorker = new List<bool>();
    public List<GameObject> animalCanvases = new List<GameObject>(); //not saved or loaded, used for linking animals with their canvases
    public List<string> animalNames = new List<string>();
    public List<float> animal_x_positions = new List<float>();
    public List<float> animal_y_positions = new List<float>();
    public List<float> animal_z_rotations = new List<float>();
    public List<int> animalFoodLevels = new List<int>();
    public List<int> animalWaterLevels = new List<int>();

    public List<int> animalZeroCounters = new List<int>();
    public List<bool> isAlive = new List<bool>();

    public List<int> animalHabitatIndexes = new List<int>(); // used for linking animals with their attached habitat, by listing the habitat indexes

    public List<GameObject> habitats; //not saved and loaded, just used for giving a habitat an index
    public List<int> habitatsSpaceLeft;

    public float gameTimer; //not for saving, for tracking game running

    public void Save()
    {
        SaveManagement.SaveWorld(this);
    }

    public void Load()
    {

        WorldData data = SaveManagement.LoadWorld();

        names = data.names.ToList();
        x_positions = data.x_positions.ToList();
        y_positions = data.y_positions.ToList();
        sizes = data.sizes.ToList();

        dayCount = data.dayCount;
        dayTimer = data.dayTimer;
        yearCount = data.yearCount;

        balance = data.balance;

        animalNames = data.animalNames.ToList();
        ages = data.ages.ToList();
        hasWorker = data.hasWorker.ToList();
        animal_x_positions = data.animal_x_positions.ToList();
        animal_y_positions = data.animal_y_positions.ToList();
        animal_z_rotations = data.animal_z_rotations.ToList();
        animalFoodLevels = data.animalFoodLevels.ToList();
        animalWaterLevels = data.animalWaterLevels.ToList();

        animalZeroCounters = data.zeroCounters.ToList();
        isAlive = data.isAlive.ToList();

        animalHabitatIndexes = data.habitatIndexes.ToList();

        habitatsSpaceLeft = data.habitatSpaceLeft.ToList();
    }

    public void Update()
    {
        gameTimer += Time.deltaTime;

        if (canSave == true)
        {
            Save();
        }
    }
}
