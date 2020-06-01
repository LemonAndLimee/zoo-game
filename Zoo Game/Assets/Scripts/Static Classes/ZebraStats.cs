using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ZebraStats
{
    public static int hungerPerDay = 30;
    public static int thirstPerDay = 40;

    public static int interestRating = 3; //out of 5

    public static int daysTilDeath = 2;

    public static int adultThreshold = 25;

    public static float adultSize = 0.6f;
    public static float babySize = 0.4f;

    public static int[] costs = new int[2] { 800, 1000 };

    public static int costToFeed = 50;
}
