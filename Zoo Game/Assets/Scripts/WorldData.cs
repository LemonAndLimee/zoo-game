using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using JetBrains.Annotations;

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
    public string[] animalGivenNames;
    public int[] ages;
    public bool[] hasWorker;
    public float[] animal_x_positions;
    public float[] animal_y_positions;
    public float[] animal_z_rotations;
    public int[] animalFoodLevels;
    public int[] animalWaterLevels;

    public int[] zeroCounters;
    public bool[] isAlive;

    public int[] habitatIndexes;
    public int[] habitatSpaceLeft;

    public int[] habitatPrefabIndexes;
    public int[] habitatTypeIndexes;
    public int[] habitatObjectIndexes;

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
        
        ages = worldScript.ages.ToArray();
        animalGivenNames = worldScript.animalGivenNames.ToArray();
        hasWorker = worldScript.hasWorker.ToArray();
        animal_x_positions = worldScript.animal_x_positions.ToArray();
        animal_y_positions = worldScript.animal_y_positions.ToArray();
        animal_z_rotations = worldScript.animal_z_rotations.ToArray();
        animalFoodLevels = worldScript.animalFoodLevels.ToArray();
        animalWaterLevels = worldScript.animalWaterLevels.ToArray();

        zeroCounters = worldScript.animalZeroCounters.ToArray();
        isAlive = worldScript.isAlive.ToArray();

        habitatIndexes = worldScript.animalHabitatIndexes.ToArray();
        habitatSpaceLeft = worldScript.habitatsSpaceLeft.ToArray();

        habitatPrefabIndexes = worldScript.habitatPrefabIndexes.ToArray();
        habitatTypeIndexes = worldScript.habitatTypeIndexes.ToArray();
        habitatObjectIndexes = worldScript.habitatObjectIndexes.ToArray();
    }
}
