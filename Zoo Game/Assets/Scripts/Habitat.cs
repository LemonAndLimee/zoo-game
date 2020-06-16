using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Habitat : MonoBehaviour
{
    public string[] animals;
    public string[] animalTexts;
    public int[] sizes;
    public int[] capacity; // doubled due to capacity points system
    public int costPerUnit;
}

public class Farm : Habitat
{
    void Awake()
    {
        animals = new string[2] { "Pig", "Llama" };
        animalTexts = new string[2] { "Pig", "Llama" };
        sizes = new int[3] { 1, 4, 9 };
        capacity = new int[] { 2, 6, 12 };
        costPerUnit = 50;
    }
}
public class Savannah : Habitat
{
    void Awake()
    {
        animals = new string[2] { "Zebra", "Lion" };
        animalTexts = new string[2] { "Zebra", "Lion" };
        sizes = new int[3] { 4, 9, 16 };
        capacity = new int[] { 4, 8, 16 };
        costPerUnit = 120;
    }
}
public class Jungle : Habitat
{
    void Awake()
    {
        animals = new string[2] { "Monkey", "Tiger" };
        animalTexts = new string[2] { "Monkey", "Tiger" };
        sizes = new int[3] { 1, 4, 9 };
        capacity = new int[] { 2, 6, 12 };
        costPerUnit = 160;
    }
}
public class Arctic : Habitat
{
    void Awake()
    {
        animals = new string[2] { "Penguin", "PolarBear" };
        animalTexts = new string[2] {"Penguin", "Polar Bear"};
        sizes = new int[3] { 1, 4, 9 };
        capacity = new int[] { 2, 6, 12 };
        costPerUnit = 220;
    }
}
public class Water : Habitat
{
    void Awake()
    {
        animals = new string[2] { "Shark", "Clownfish" };
        animalTexts = new string[2] { "Shark", "Clownfish" };
        sizes = new int[3] { 4, 9, 16 };
        capacity = new int[] { 4, 8, 16 };
        costPerUnit = 250;
    }
}
public class Desert : Habitat
{
    void Awake()
    {
        animals = new string[2] { "Camel", "Lizard" };
        animalTexts = new string[2] { "Camel", "Lizard" };
        sizes = new int[3] { 1, 4, 9 };
        capacity = new int[] { 2, 6, 12 };
        costPerUnit = 120;
    }
}
