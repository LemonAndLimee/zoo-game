using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Facility : MonoBehaviour
{
    public string nameText;
    public int cost;
    public int comfortPoints;
}

public class Hygiene : Facility
{

}
public class Toilet : Hygiene
{
    void Awake()
    {
        nameText = "Toilet";
        cost = 50;
        comfortPoints = 5;
    }
}

public class Shop : Facility
{
    
}
public class BurgerStand : Shop
{
    void Awake()
    {
        nameText = "Burger Stand";
        cost = 80;
        comfortPoints = 10;
    }
}
public class IceCreamStand : Shop
{
    void Awake()
    {
        nameText = "Ice Cream Stand";
        cost = 80;
        comfortPoints = 10;
    }
}
public class LemonadeStand : Shop
{
    void Awake()
    {
        nameText = "Lemonade Stand";
        cost = 80;
        comfortPoints = 10;
    }
}
