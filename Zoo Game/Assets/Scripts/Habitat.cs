using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Habitat : MonoBehaviour
{
    public string[] animals;
    public int[] sizes;
    public int[] capacity;
    public int costPerUnit;
}

public class Farm : Habitat
{
    void Awake()
    {
        animals = new string[2] { "Pig", "Llama" };
        sizes = new int[3] { 1, 4, 9 };
        capacity = new int[] { 1, 3, 6 };
        costPerUnit = 50;
    }
}
public class Savannah : Habitat
{
    void Awake()
    {
        animals = new string[2] { "Zebra", "Lion" };
        sizes = new int[3] { 4, 9, 16 };
        capacity = new int[] { 2, 4, 8 };
        costPerUnit = 100;
    }
}
public class Jungle : Habitat
{
    void Awake()
    {
        animals = new string[2] { "Monkey", "Tiger" };
        sizes = new int[3] { 1, 4, 9 };
        capacity = new int[] { 1, 3, 6 };
        costPerUnit = 150;
    }
}
public class Arctic : Habitat
{
    void Awake()
    {
        animals = new string[2] { "Penguin", "Polar Bear" };
        sizes = new int[3] { 1, 4, 9 };
        capacity = new int[] { 1, 3, 6 };
        costPerUnit = 200;
    }
}
