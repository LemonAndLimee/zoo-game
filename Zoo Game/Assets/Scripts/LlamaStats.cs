using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class LlamaStats
{
    public static int hungerPerDay = 30;
    public static int thirstPerDay = 40;

    public static int interestRating = 1; //out of 5

    public static int daysTilDeath = 2;

    public static int adultThreshold = 25;

    public static float adultSize = 0.5f;
    public static float babySize = 0.3f;

    public static int[] costs = new int[2] { 150, 200 };

    public static int costToFeed = 10;
}
