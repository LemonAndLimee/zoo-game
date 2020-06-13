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

    public int capacityPoints; // 1 = small, 2 = medium, 4 = large

    public bool isPredator;
    public string[] foodAnimals;
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
        capacityPoints = 2;
        isPredator = false;
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
        capacityPoints = 2;
        isPredator = false;
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
        capacityPoints = 2;
        isPredator = false;
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
        capacityPoints = 4;
        isPredator = true;
        foodAnimals = new string[1] {"Zebra"};
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
        capacityPoints = 2;
        isPredator = false;
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
        capacityPoints = 4;
        isPredator = true;
        foodAnimals = new string[1] { "Monkey" };
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
        capacityPoints = 2;
        isPredator = false;
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
        capacityPoints = 4;
        isPredator = true;
        foodAnimals = new string[1] { "Penguin" };
    }
}

public class Shark : Animal
{
    void Awake()
    {
        hungerPerDay = 20;
        thirstPerDay = 30;
        interestRating = 5; //out of 5
        daysTilDeath = 2;
        adultThreshold = 10;
        adultSize = 0.9f;
        babySize = 0.5f;
        costs = new int[2] { 1800, 2000 };
        costToFeed = 75;
        averageSpeed = 25;
        capacityPoints = 4;
        isPredator = true;
        foodAnimals = new string[1] { "Clownfish" };
    }
}

public class Clownfish : Animal
{
    void Awake()
    {
        hungerPerDay = 30;
        thirstPerDay = 40;
        interestRating = 2; //out of 5
        daysTilDeath = 2;
        adultThreshold = 10;
        adultSize = 0.2f;
        babySize = 0.1f;
        costs = new int[2] { 5, 10 };
        costToFeed = 1;
        averageSpeed = 25;
        capacityPoints = 1;
        isPredator = false;
    }
}

public class Camel : Animal
{
    void Awake()
    {
        hungerPerDay = 30;
        thirstPerDay = 40;
        interestRating = 3; //out of 5
        daysTilDeath = 2;
        adultThreshold = 25;
        adultSize = 0.8f;
        babySize = 0.6f;
        costs = new int[2] { 500, 600 };
        costToFeed = 30;
        averageSpeed = 10;
        capacityPoints = 2;
        isPredator = false;
    }
}

public class Lizard : Animal
{
    void Awake()
    {
        hungerPerDay = 20;
        thirstPerDay = 30;
        interestRating = 3; //out of 5
        daysTilDeath = 2;
        adultThreshold = 15;
        adultSize = 0.4f;
        babySize = 0.25f;
        costs = new int[2] { 100, 150 };
        costToFeed = 10;
        averageSpeed = 20;
        capacityPoints = 1;
        isPredator = false;
    }
}
