using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowAnimal : MonoBehaviour
{
    public GameObject animal;
    public Vector3 offset;

    // Update is called once per frame
    void Update()
    {
        transform.position = animal.transform.position + offset;
    }
}
