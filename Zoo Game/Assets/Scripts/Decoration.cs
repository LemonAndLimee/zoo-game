using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Decoration : MonoBehaviour
{
    public string nameText;
    public int cost;
}

public class Path : Decoration
{
    public GameObject[] prefabs;
}
public class StonePath : Path
{
    void Awake()
    {
        nameText = "Stone Path";
        cost = 5;
    }
}

public class Plant : Decoration
{
    public GameObject[] prefabs;
}
public class OakTree : Plant
{
    void Awake()
    {
        nameText = "Oak Tree";
        cost = 20;
    }
}
public class AppleTree : Plant
{
    void Awake()
    {
        nameText = "Apple Tree";
        cost = 25;
    }
}

