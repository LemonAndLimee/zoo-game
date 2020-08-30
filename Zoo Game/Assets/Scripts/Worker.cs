using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Worker
{
    public List<GameObject> animalsToFeed = new List<GameObject>();

    public string name;

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
                statsScript.Feed(false);
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

        if (animalsToFeed.Count >= 1)
        {
            for (int i = 0; i < animalsToFeed.Count; i++)
            {
                if (animalsToFeed[i] != null)
                {
                    dailySalary += animalsToFeed[i].GetComponent<AnimalStats>().costToFeed;
                }
                else
                {
                    animalsToFeed.RemoveAt(i);
                }
            }
            dailySalary += 25;
        }

    }
}
