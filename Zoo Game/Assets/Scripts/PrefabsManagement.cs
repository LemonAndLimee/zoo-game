using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class PrefabsManagement : MonoBehaviour
{
    public string[] animalNames;

    public GameObject[] animals;

    public Sprite[] animalImages;

    public Color[] animalColours;

    public List<Animal> scripts = new List<Animal>();
    public List<Habitat> habitatScripts = new List<Habitat>();

    public string[] habitatNames;
    public string[] habitatTypeNames;

    public GameObject[] habitats;

    public Sprite[] habitatImages;

    public List<Path> pathScripts = new List<Path>();
    public List<Hygiene> hygieneScripts = new List<Hygiene>();
    public List<Shop> shopScripts = new List<Shop>();
    public List<Plant> plantScripts = new List<Plant>();

    public GameObject[] hygieneFacilities;
    public GameObject[] shops;

    public Sprite[] pathImages;
    public Sprite[] hygieneImages;
    public Sprite[] shopImages;
    public Sprite[] plantImages;

    public GameObject[] stonePaths;
    public GameObject[] oakTrees;
    public GameObject[] appleTrees;

    void Start()
    {
        scripts.Add(gameObject.AddComponent<Pig>());
        scripts.Add(gameObject.AddComponent<Llama>());
        scripts.Add(gameObject.AddComponent<Zebra>());
        scripts.Add(gameObject.AddComponent<Lion>());
        scripts.Add(gameObject.AddComponent<Monkey>());
        scripts.Add(gameObject.AddComponent<Tiger>());
        scripts.Add(gameObject.AddComponent<Penguin>());
        scripts.Add(gameObject.AddComponent<PolarBear>());
        scripts.Add(gameObject.AddComponent<Shark>());
        scripts.Add(gameObject.AddComponent<Clownfish>());
        scripts.Add(gameObject.AddComponent<Camel>());
        scripts.Add(gameObject.AddComponent<Lizard>());

        habitatScripts.Add(gameObject.AddComponent<Farm>());
        habitatScripts.Add(gameObject.AddComponent<Savannah>());
        habitatScripts.Add(gameObject.AddComponent<Jungle>());
        habitatScripts.Add(gameObject.AddComponent<Arctic>());
        habitatScripts.Add(gameObject.AddComponent<Water>());
        habitatScripts.Add(gameObject.AddComponent<Desert>());

        pathScripts.Add(gameObject.AddComponent<StonePath>());
        pathScripts[0].prefabs = stonePaths;

        hygieneScripts.Add(gameObject.AddComponent<Toilet>());

        shopScripts.Add(gameObject.AddComponent<BurgerStand>());
        shopScripts.Add(gameObject.AddComponent<IceCreamStand>());
        shopScripts.Add(gameObject.AddComponent<LemonadeStand>());

        plantScripts.Add(gameObject.AddComponent<OakTree>());
        plantScripts[0].prefabs = oakTrees;
        plantScripts.Add(gameObject.AddComponent<AppleTree>());
        plantScripts[1].prefabs = appleTrees;
    }

}
