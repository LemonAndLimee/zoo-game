﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//manages day cycle
public class DayLogic : MonoBehaviour
{
    public float dayLength;
    public float timer;
    public int dayCount;
    public int yearCount;

    public WorldManagement worldScript;
    public StartManagement startScript;

    public Text dayText;
    public Text yearText;

    public MoneyLogic moneyScript;

    public StaffManagement staffScript;
    public CustomerManagement customerScript;

    // Start is called before the first frame update
    void Start()
    {
        startScript = GameObject.FindGameObjectWithTag("StartManager").GetComponent<StartManagement>();
        if (startScript.isNew == true && startScript.isLoad == false)
        {
            timer = 0;
            dayCount = 0;
            yearCount = 0;
        }
        else if (startScript.isLoad == true && startScript.isNew == false)
        {
            worldScript.Load();
            timer = worldScript.dayTimer;
            dayCount = worldScript.dayCount;
            yearCount = worldScript.yearCount;
        }
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        //each day
        if (timer >= dayLength || Input.GetKeyDown("."))
        {
            timer = 0;
            dayCount += 1;
            //add customer payment
            moneyScript.balance += Mathf.RoundToInt(customerScript.customerNumber * customerScript.customerEntryFee);

            for (int i = 0; i < worldScript.animals.Count; i++)
            {
                worldScript.animals[i].GetComponent<AnimalStats>().Day();
            }

            staffScript.Day();
        }
        //each year
        if (dayCount >= 366)
        {
            yearCount += 1;
            dayCount = 0;
        }

        worldScript.dayCount = dayCount;
        worldScript.dayTimer = timer;

        dayText.text = "Day " + dayCount.ToString();
        yearText.text = "Year " + yearCount.ToString();
    }

    
}
