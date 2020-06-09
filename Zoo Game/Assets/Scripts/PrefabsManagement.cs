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

        habitatScripts.Add(gameObject.AddComponent<Farm>());
        habitatScripts.Add(gameObject.AddComponent<Savannah>());
        habitatScripts.Add(gameObject.AddComponent<Jungle>());
        habitatScripts.Add(gameObject.AddComponent<Arctic>());
        habitatScripts.Add(gameObject.AddComponent<Water>());
    }

}
