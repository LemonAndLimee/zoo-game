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
    }

    public void Update()
    {
        if (canSave == true)
        {
            Save();
        }
    }
}
