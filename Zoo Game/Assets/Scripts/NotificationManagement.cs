using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class NotificationManagement : MonoBehaviour
{
    public GameObject[] buttons;
    public List<Notification> notifications = new List<Notification>();

    public Sprite warningIcon;

    public GameObject target;

    public List<GameObject> animals = new List<GameObject>();

    public GameObject messageText;

    void Start()
    {
        for (int i = 0; i < buttons.Count(); i++)
        {
            buttons[i].SetActive(false);
        }
    }

    void Update()
    {
        for (int i = 0; i < buttons.Count(); i++)
        {
            buttons[i].SetActive(false);
        }

        if (notifications.Count >= 1) // very important - script assumes the no of animals is the same as no of notifications, and that animal 0 corresponds to notif. 0, etc.
        {
            for (int i = 0; i < notifications.Count; i++)
            {
                buttons[i].SetActive(true);
                if (notifications[i].type == "Warning")
                {
                    Button btn = buttons[i].GetComponent<Button>();
                    btn.image.sprite = warningIcon;

                    AnimalStats statsScript = animals[i].GetComponent<AnimalStats>();
                    if (statsScript.isDanger == false)
                    {
                        buttons[i].SetActive(false);
                        animals.RemoveAt(i);
                        notifications.RemoveAt(i);
                    }
                }
            }
        }
    }

    public void AddNotification(string type)
    {
        Notification n = new Notification();
        n.type = type;
        if (type == "Warning")
        {
            n.teleportTarget = target;
        }
        notifications.Add(n);

    }

    public void DeathMessage(string animal, string mannerOfDeath)
    {
        Text txt = messageText.GetComponent<Text>();
        txt.text = "A " + animal + " " + mannerOfDeath;
        Animation anim = messageText.GetComponent<Animation>();
        anim.Play("MessageDisplay");
    }

    public void Button0()
    {
        notifications[0].ClickAction();
    }
    public void Button1()
    {
        notifications[1].ClickAction();
    }
    public void Button2()
    {
        notifications[2].ClickAction();
    }
    public void Button3()
    {
        notifications[3].ClickAction();
    }
    public void Button4()
    {
        notifications[4].ClickAction();
    }
    public void Button5()
    {
        notifications[5].ClickAction();
    }
}
