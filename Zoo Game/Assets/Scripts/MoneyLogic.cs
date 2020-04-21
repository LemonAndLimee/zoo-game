using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//manager for money
public class MoneyLogic : MonoBehaviour
{
    public int startBalance;
    public int balance;

    public Text moneyText;

    public WorldManagement worldScript;
    public StartManagement startScript;

    // Start is called before the first frame update
    void Start()
    {
        startScript = GameObject.FindGameObjectWithTag("StartManager").GetComponent<StartManagement>();
        if (startScript.isNew == true && startScript.isLoad == false)
        {
            balance = startBalance;
        }
        else if (startScript.isLoad == true && startScript.isNew == false)
        {
            worldScript.Load();
            balance = worldScript.balance;
        }
    }

    // Update is called once per frame
    void Update()
    {
        moneyText.text = balance.ToString();

        worldScript.balance = balance;
    }
}
