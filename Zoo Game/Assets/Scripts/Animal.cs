using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animal : MonoBehaviour
{
    public int hungerPerDay;
    public int thirstPerDay;

    public int interestRating;

    public int daysTilDeath;

    public int adultThreshold;

    public float adultSize;
    public float babySize;

    public int[] costs;

    public int costToFeed;

    public int averageSpeed;
}

public class Pig : Animal
{
    void Awake()
    {
        hungerPerDay = 20;
        thirstPerDay = 30;
        interestRating = 1; //out of 5
        daysTilDeath = 2;
        adultThreshold = 20;
        adultSize = 0.4f;
        babySize = 0.25f;
        costs = new int[2] { 100, 150 };
        costToFeed = 5;
        averageSpeed = 15;
    }
}

public class Llama : Animal
{
    void Awake()
    {
        hungerPerDay = 30;
        thirstPerDay = 40;
        interestRating = 2; //out of 5
        daysTilDeath = 2;
        adultThreshold = 25;
        adultSize = 0.5f;
        babySize = 0.3f;
        costs = new int[2] { 150, 200 };
        costToFeed = 10;
        averageSpeed = 10;
    }
}

public class Zebra : Animal
{
    void Awake()
    {
        hungerPerDay = 30;
        thirstPerDay = 40;
        interestRating = 3; //out of 5
        daysTilDeath = 2;
        adultThreshold = 25;
        adultSize = 0.6f;
        babySize = 0.4f;
        costs = new int[2] { 800, 1000 };
        costToFeed = 50;
        averageSpeed = 10;
    }
}

public class Lion : Animal
{
    void Awake()
    {
        hungerPerDay = 10;
        thirstPerDay = 20;
        interestRating = 5; //out of 5
        daysTilDeath = 3;
        adultThreshold = 25;
        adultSize = 0.8f;
        babySize = 0.5f;
        costs = new int[2] { 2500, 3000 };
        costToFeed = 100;
        averageSpeed = 20;
    }
}

public class Monkey : Animal
{
    void Awake()
    {
        hungerPerDay = 20;
        thirstPerDay = 30;
        interestRating = 4; //out of 5
        daysTilDeath = 2;
        adultThreshold = 15;
        adultSize = 0.3f;
        babySize = 0.2f;
        costs = new int[2] { 400, 500 };
        costToFeed = 15;
        averageSpeed = 25;
    }
}

public class Tiger : Animal
{
    void Awake()
    {
        hungerPerDay = 10;
        thirstPerDay = 20;
        interestRating = 5; //out of 5
        daysTilDeath = 3;
        adultThreshold = 25;
        adultSize = 0.8f;
        babySize = 0.5f;
        costs = new int[2] { 2500, 3000 };
        costToFeed = 100;
        averageSpeed = 25;
    }
}

public class Penguin : Animal
{
    void Awake()
    {
        hungerPerDay = 20;
        thirstPerDay = 30;
        interestRating = 3; //out of 5
        daysTilDeath = 2;
        adultThreshold = 15;
        adultSize = 0.3f;
        babySize = 0.15f;
        costs = new int[2] { 350, 400 };
        costToFeed = 15;
        averageSpeed = 15;
    }
}

public class PolarBear : Animal
{
    void Awake()
    {
        hungerPerDay = 10;
        thirstPerDay = 20;
        interestRating = 5; //out of 5
        daysTilDeath = 3;
        adultThreshold = 25;
        adultSize = 0.8f;
        babySize = 0.5f;
        costs = new int[2] { 2500, 3000 };
        costToFeed = 100;
        averageSpeed = 20;
    }
}
