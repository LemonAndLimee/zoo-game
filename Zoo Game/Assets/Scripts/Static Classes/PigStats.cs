using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PigStats
{
    public static int hungerPerDay = 20;
    public static int thirstPerDay = 30;

    public static int interestRating = 1; //out of 5

    public static int daysTilDeath = 2;

    public static int adultThreshold = 20;

    public static float adultSize = 0.4f;
    public static float babySize = 0.25f;

    public static int[] costs = new int[2] {100, 150};

    public static int costToFeed = 5;
}
