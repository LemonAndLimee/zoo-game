using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Notification
{
    public string type;
    public GameObject teleportTarget;


    public void ClickAction()
    {
        if (type == "Warning")
        {
            Teleport(new Vector3(teleportTarget.transform.position.x, teleportTarget.transform.position.y, 0f));
        }
    }

    public void Teleport(Vector3 destination)
    {
        GameObject player = GameObject.Find("Player");
        player.transform.position = destination;
    }
}
