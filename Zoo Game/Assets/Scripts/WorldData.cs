using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

//holds the data to be stored in binary file - gets them from worldmanagement

[System.Serializable]
public class WorldData
{
    public string[] names;
    public float[] x_positions;
    public float[] y_positions;
    public float[] sizes;

    public float dayTimer;
    public int dayCount;
    public int yearCount;

    public int balance;

    public string[] animalNames;
    public float[] animal_x_positions;
    public float[] animal_y_positions;
    public int[] animalFoodLevels;
    public int[] animalWaterLevels;

    public int[] habitatSpaceLeft;

    public WorldData (WorldManagement worldScript)
    {
        names = worldScript.names.ToArray();
        x_positions = worldScript.x_positions.ToArray();
        y_positions = worldScript.y_positions.ToArray();
        sizes = worldScript.sizes.ToArray();

        dayTimer = worldScript.dayTimer;
        dayCount = worldScript.dayCount;
        yearCount = worldScript.yearCount;

        balance = worldScript.balance;

        animalNames = worldScript.animalNames.ToArray();
        animal_x_positions = worldScript.animal_x_positions.ToArray();
        animal_y_positions = worldScript.animal_y_positions.ToArray();
        animalFoodLevels = worldScript.animalFoodLevels.ToArray();
        animalWaterLevels = worldScript.animalWaterLevels.ToArray();

        habitatSpaceLeft = worldScript.habitatsSpaceLeft.ToArray();
    }
}
