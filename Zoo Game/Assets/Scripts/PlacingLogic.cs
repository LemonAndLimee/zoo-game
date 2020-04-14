﻿using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public class PlacingLogic : MonoBehaviour
{

    public bool placeMode;
    public GameObject prefab;

    public bool isEvenX = true;
    public bool isEvenY = true;
    public Vector2 snappedPos;
    public Vector2 previousSnappedPos = new Vector2(800f, 800f);

    public GameObject currentObject;
    public GameObject previousObject;

    public WorldManagement worldScript;

    public bool continuousPlacingMode;

    //tells user to press x to quit
    public Text placeModeText;

    public bool canPlace;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        

        if (placeMode == true)
        {
            PlacingUpdate();
        }
        
    }

    //to be run every update while placemode is on
    public void PlacingUpdate()
    {
        snappedPos = snapPosition(getMousePos(), isEvenX, isEvenY);

        //if mouse position has changed, delete old object and spawn new at new mouse pos
        if (snappedPos != previousSnappedPos || currentObject == null)
        {
            if (previousObject != null)
            {
                Destroy(previousObject);
            }
            currentObject = Instantiate(prefab);
            currentObject.transform.position = new Vector3(snappedPos.x, snappedPos.y, 0f);

        }

        //checks if space is taken - subroutine assigns a value to canPlace
        CheckAvailableSpace();

        //if mouse is clicked
        if (Input.GetMouseButtonDown(0))
        {
            if (canPlace == true)
            {
                Place();
            }
            else
            {
                TogglePlacing(prefab, false, true, true);
            }
        }

        // if x is pressed, exit placing mode
        if (Input.GetKeyDown("x"))
        {
            TogglePlacing(prefab, false, true, true);
        }

        previousSnappedPos = snappedPos;
        previousObject = currentObject;
    }

    public void TogglePlacing(GameObject obj, bool continous, bool evenX, bool evenY)
    {
        isEvenX = evenX;
        isEvenY = evenY;
        if (placeMode == false)
        {
            placeMode = true;
            prefab = obj;
            //getMousePos();
            continuousPlacingMode = continous;
            placeModeText.text = "Press 'x' to exit placing mode";
        }
        else
        {
            placeMode = false;
            Destroy(currentObject);
            previousSnappedPos = new Vector2(800f, 800f);
            placeModeText.text = "";
        }
    }

    public static Vector2 getMousePos()
    {
        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

        if (hit.transform != null)
        {
            return hit.point;
        }
        else
        {
            return new Vector2(0f, 0f);
        }
    }

    public static Vector2 snapPosition(Vector2 vector, bool evenX, bool evenY)
    {
        Vector2 snap = math.round(vector);

        //if snap x is odd
        if (snap.x % 2 != 0)
        {
            if (evenX == true)
            {
                snap.x = math.round(vector.x / 2) * 2;
            }
        }
        //if snap x is even
        else
        {
            if (evenX == false)
            {
                //either odd number . 5+ (e.g., 3.6, snap = 4)
                //snap - vector = 0.4 <-- will be 0.5 or under
                //1 - (snap - vector) = 0.6
                //vector - difference = 3
                float difference = snap.x - vector.x;
                if (difference >= 0)
                {
                    difference = 1 - difference;
                    
                }
                else
                {
                    difference = -1 - difference;
                }
                snap.x = vector.x - difference;

                //or even number . 5- (e.g. 4.3, snap = 4)
                //snap - vector = -0.3
                //-1 - -0.3 = -0.7
                //vector - difference (- -0.7) = 5
            }
        }

        //if snap y is odd
        if (snap.y % 2 != 0)
        {
            if (evenY == true)
            {
                snap.y = math.round(vector.y / 2) * 2;
            }
        }
        else
        {
            if (evenY == false)
            {
                float difference = snap.y - vector.y;
                if (difference >= 0)
                {
                    difference = 1 - difference;

                }
                else
                {
                    difference = -1 - difference;
                }
                snap.y = vector.y - difference;
            }
        }

        return snap;
    }

    public void CheckAvailableSpace()
    {
        //checks if object is touching another object
        TriggerDetection touchingScript = currentObject.GetComponent<TriggerDetection>();
        if (touchingScript.isTouching == true)
        {
            currentObject.GetComponent<SpriteRenderer>().enabled = true;
            canPlace = false;
        }
        else
        {
            canPlace = true;
        }
        //also checks for position overlaps
        for (int i = 0; i < worldScript.x_positions.Count; i++)
        {
            if (worldScript.x_positions[i] == currentObject.transform.position.x && worldScript.y_positions[i] == currentObject.transform.position.y)
            {
                //makes object go red
                currentObject.GetComponent<SpriteRenderer>().enabled = true;
                canPlace = false;
            }
        }
    }

    public void Place()
    {
        placeMode = false;

        worldScript.objects.Add(currentObject);
        worldScript.names.Add(prefab.name);
        worldScript.x_positions.Add(currentObject.transform.position.x);
        worldScript.y_positions.Add(currentObject.transform.position.y);
        worldScript.sizes.Add(currentObject.transform.localScale.x);

        previousObject = null;
        currentObject = null;

        previousSnappedPos = new Vector2(800f, 800f);

        if (continuousPlacingMode == true)
        {
            TogglePlacing(prefab, true, true, true);
        }
    }
}