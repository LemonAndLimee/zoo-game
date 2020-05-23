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

    public float meanCustomerRating;

    public Image[] stars;
    public Color ratingGreen;
    public Color ratingGrey;

    // Start is called before the first frame update
    void Start()
    {
        //temporary 5 star rating assignment
        meanCustomerRating = 5f;
    }

    // Update is called once per frame
    void Update()
    {
        if (worldScript.animals.Count >= 1)
        {
            int total = 0;

            float ratingTotal = 0;

            for (int i = 0; i < worldScript.animals.Count; i++)
            {
                AnimalStats statsScript = worldScript.animals[i].GetComponent<AnimalStats>();

                total += statsScript.interestRating;

                if (statsScript.isAlive == true)
                {
                    ratingTotal += 5f;
                }
            }
            totalInterestRating = total;

            meanInterestRating = totalInterestRating / worldScript.animals.Count;

            if (ratingTotal > 0)
            {
                meanCustomerRating = Mathf.Round(ratingTotal / worldScript.animals.Count);
            }
            else
            {
                meanCustomerRating = 1;
            }
        }



        int num = Mathf.RoundToInt(worldScript.animals.Count * meanInterestRating * meanCustomerRating * 2);
        customerNumber = num;
        customerText.text = customerNumber.ToString();

        for (int i = 0; i < stars.Count(); i++)
        {
            if (i < meanCustomerRating)
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
