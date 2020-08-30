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
            Invoke("LoadWorkers", 0.12f);
        }
    }

    void Update()
    {
        for (int i = 0; i < workers.Count; i++)
        {
            if (workers[i].dailySalary >= 1)
            {
                workers[i].CalculateSalary();
            }
            else 
            {
                workers.RemoveAt(i);
            }
        }
    }
    public void LoadWorkers()
    {
        worldScript.Load();
        for (int i = 0; i < worldScript.hasWorker.Count; i++)
        {
            if (worldScript.hasWorker[i] == true)
            {
                Debug.Log("animals length: " + worldScript.animals.Count.ToString());
                AddWorker(worldScript.animals[i], true);
            }
        }
    }

    public void AddWorker(GameObject animal, bool isLoaded)
    {
        if (worldScript.hasWorker[animal.GetComponent<AnimalStats>().worldScriptIndex] == false || isLoaded == true)
        {
            if (workers.Count == 0)
            {
                Worker worker = new Worker();
                worker.animalsToFeed.Add(animal);
                worker.name = "Worker " + (workers.Count + 1).ToString();
                worker.CalculateSalary();
                workers.Add(worker);
                Debug.Log("hire new");
            }
            else
            {
                bool foundSpace = false;
                for (int i = 0; i < workers.Count; i++)
                {
                    if (foundSpace == false)
                    {
                        if (workers[i].animalsToFeed.Count < workers[i].capacity)
                        {
                            workers[i].animalsToFeed.Add(animal);
                            workers[i].CalculateSalary();
                            Debug.Log("assign");
                            foundSpace = true;
                        }
                    }
                    
                }
                if (foundSpace == false)
                {
                    Worker worker = new Worker();
                    worker.animalsToFeed.Add(animal);
                    worker.name = "Worker " + (workers.Count + 1).ToString();
                    worker.CalculateSalary();
                    workers.Add(worker);
                    Debug.Log("hire new");
                }
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
