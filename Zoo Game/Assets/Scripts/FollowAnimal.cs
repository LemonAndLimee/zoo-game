using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowAnimal : MonoBehaviour
{
    public GameObject animal;

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(animal.transform.position.x, animal.transform.position.y + 1.5f, 0f);
    }
}
