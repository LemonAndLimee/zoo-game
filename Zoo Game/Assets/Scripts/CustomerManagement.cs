using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public class CustomerManagement : MonoBehaviour
{
    public int totalInterestRating;
    public float meanInterestRating;

    public WorldManagement worldScript;

    public int customerNumber;

    public Text customerText;

    public float customerEntryFee = 5f;

    public int reputation;
    public int starsRating;

    public int reputationPreComfortPoints; // without being affected by comfort points
    public int comfortPoints;
    public int expectedComfortPoints;
    public int comfortPointsDifference;

    public Image[] stars;
    public Color ratingGreen;
    public Color ratingGrey;

    // Start is called before the first frame update
    void Start()
    {
        StartManagement startScript = GameObject.FindGameObjectWithTag("StartManager").GetComponent<StartManagement>();

        if (startScript.isLoad == true)
        {
            worldScript.Load();
            reputationPreComfortPoints = worldScript.reputation;
        }
        else
        {
            reputationPreComfortPoints = 80;
        }
    }

    // Update is called once per frame
    void Update()
    {
        expectedComfortPoints = Mathf.RoundToInt(worldScript.animals.Count * meanInterestRating);
        comfortPointsDifference = comfortPoints - expectedComfortPoints;

        reputation = reputationPreComfortPoints + comfortPointsDifference;

        if (reputation >= 100)
        {
            starsRating = 5;
        }
        else
        {
            starsRating = Convert.ToInt32(Mathf.Round(reputation / 20f));
        }

        if (worldScript.animals.Count >= 1) //calculates interest rating
        {
            int total = 0;

            for (int i = 0; i < worldScript.animals.Count; i++)
            {
                AnimalStats statsScript = worldScript.animals[i].GetComponent<AnimalStats>();

                total += statsScript.interestRating;
            }

            totalInterestRating = total;

            meanInterestRating = totalInterestRating / worldScript.animals.Count;
        }

        int num = Mathf.RoundToInt(worldScript.animals.Count * meanInterestRating * starsRating * 2);
        customerNumber = num;
        customerText.text = customerNumber.ToString();

        worldScript.reputation = reputationPreComfortPoints;

        for (int i = 0; i < stars.Count(); i++)
        {
            if (i < starsRating)
            {
                stars[i].color = ratingGreen;
            }
            else
            {
                stars[i].color = ratingGrey;
            }
        }
    }
}
