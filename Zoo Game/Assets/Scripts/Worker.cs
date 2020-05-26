using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Worker
{
    public List<GameObject> animalsToFeed = new List<GameObject>();
    public int capacity = 5;

    public int dailySalary = 80;

    public void Feed(int index)
    {
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
}
