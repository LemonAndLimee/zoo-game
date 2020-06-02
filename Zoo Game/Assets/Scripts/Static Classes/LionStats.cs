using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class LionStats
{
    public static int hungerPerDay = 10;
    public static int thirstPerDay = 20;

    public static int interestRating = 5; //out of 5

    public static int daysTilDeath = 3;

    public static int adultThreshold = 25;

    public static float adultSize = 0.8f;
    public static float babySize = 0.5f;

    public static int[] costs = new int[2] { 2500, 3000 };

    public static int costToFeed = 100;
}
