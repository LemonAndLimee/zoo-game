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

    void Start()
    {
        scripts.Add(gameObject.AddComponent<Pig>());
        scripts.Add(gameObject.AddComponent<Llama>());
        scripts.Add(gameObject.AddComponent<Zebra>());
        scripts.Add(gameObject.AddComponent<Lion>());
        scripts.Add(gameObject.AddComponent<Monkey>());
        scripts.Add(gameObject.AddComponent<Tiger>());

        habitatScripts.Add(gameObject.AddComponent<Farm>());
        habitatScripts.Add(gameObject.AddComponent<Savannah>());
        habitatScripts.Add(gameObject.AddComponent<Jungle>());
    }

}
