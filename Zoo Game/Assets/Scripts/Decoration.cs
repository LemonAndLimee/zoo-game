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



