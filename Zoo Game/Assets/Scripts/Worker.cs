using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Worker
{
    public List<GameObject> animalsToFeed = new List<GameObject>();

    public string name = "Jim";

    public int capacity = 6;

    public int dailySalary;

    public void Feed(int index)
    {
        CalculateSalary();

        if (animalsToFeed[index] != null)
        {
            AnimalStats statsScript = animalsToFeed[index].GetComponent<AnimalStats>();
            if (statsScript.foodLevel <= 30 || statsScript.waterLevel <= 30)
            {
                statsScript.Feed();
            }
        }
        else
        {
            animalsToFeed.RemoveAt(index);
        }
    }

    public void CalculateSalary()
    {
        dailySalary = 0;
        for (int i = 0; i < animalsToFeed.Count; i++)
        {
            dailySalary += animalsToFeed[i].GetComponent<AnimalStats>().costToFeed;
            //Debug.Log(animalsToFeed[i].GetComponent<AnimalStats>().costToFeed);
        }
        dailySalary += 25;
    }
}
