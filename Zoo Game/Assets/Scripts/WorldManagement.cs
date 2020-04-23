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

    public List<string> animalNames = new List<string>();
    public List<float> animal_x_positions = new List<float>();
    public List<float> animal_y_positions = new List<float>();

    public List<int> habitatsSpaceLeft;

    public void Save()
    {
        SaveManagement.SaveWorld(this);
    }

    public void Load()
    {

        WorldData data = SaveManagement.LoadWorld();

        names = data.names.ToList<string>();
        x_positions = data.x_positions.ToList<float>();
        y_positions = data.y_positions.ToList<float>();
        sizes = data.sizes.ToList<float>();

        dayCount = data.dayCount;
        dayTimer = data.dayTimer;
        yearCount = data.yearCount;

        balance = data.balance;

        animalNames = data.animalNames.ToList<string>();
        animal_x_positions = data.animal_x_positions.ToList<float>();
        animal_y_positions = data.animal_y_positions.ToList<float>();

        habitatsSpaceLeft = data.habitatSpaceLeft.ToList<int>();
    }

    public void Update()
    {
        if (canSave == true)
        {
            Save();
        }
    }
}
