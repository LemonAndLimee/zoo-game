using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;

public class StaffManagement : MonoBehaviour
{
    public List<Worker> workers = new List<Worker>();

    public MoneyLogic moneyScript;
    public WorldManagement worldScript;
    public StartManagement startScript;

    // Start is called before the first frame update
    void Start()
    {
        startScript = GameObject.FindGameObjectWithTag("StartManager").GetComponent<StartManagement>();

        if (startScript.isLoad == true)
        {
            worldScript.Load();
            for (int i = 0; i < worldScript.hasWorker.Count; i++)
            {
                if (worldScript.hasWorker[i] == true)
                {
                    AddWorker(worldScript.animals[i]);
                }
            }
        }
    }


    public void AddWorker(GameObject animal)
    {
        if (worldScript.hasWorker[animal.GetComponent<AnimalStats>().worldScriptIndex] == false)
        {
            if (workers.Count == 0)
            {
                Worker worker = new Worker();
                worker.animalsToFeed.Add(animal);
                workers.Add(worker);
                Debug.Log("hire new");
            }
            else if (workers[workers.Count - 1].animalsToFeed.Count >= workers[workers.Count - 1].capacity)
            {
                Worker worker = new Worker();
                worker.animalsToFeed.Add(animal);
                workers.Add(worker);
                Debug.Log("hire new");
            }
            else
            {
                workers[workers.Count - 1].animalsToFeed.Add(animal);
                Debug.Log("assign");
            }

            worldScript.hasWorker[animal.GetComponent<AnimalStats>().worldScriptIndex] = true;
        }
        else
        {
            Debug.Log("already fed");
        }
        
    }

    public void Day()
    {
        for (int i = 0; i < workers.Count; i++)
        {
            for (int x = 0; x < workers[i].animalsToFeed.Count; x++)
            {
                workers[i].Feed(x);
            }

            //deduct pay from balance
            moneyScript.balance -= workers[i].dailySalary;
        }
    }
}
