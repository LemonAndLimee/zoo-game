using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

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

    public int currentCost;
    public MoneyLogic moneyScript;

    public List<GameObject> continousPrefabs = new List<GameObject>();

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
            Invoke("PlaceModeTrue", 0.3f);
            prefab = obj;
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

    public void PlaceModeTrue()
    {
        placeMode = true;
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
                //if not trees or rocks
                Debug.Log(i);
                if (worldScript.objects[i].tag != "Terrain")
                {
                    //makes object go red
                    currentObject.GetComponent<SpriteRenderer>().enabled = true;
                    canPlace = false;
                }

            }
        }
    }

    public void Place()
    {
        //deletes any trees or rocks at same position
        DeleteTerrainAtPosition();

        moneyScript.balance -= currentCost;

        placeMode = false;
        placeModeText.text = "";

        worldScript.objects.Add(currentObject);
        worldScript.names.Add(prefab.name);
        worldScript.x_positions.Add(currentObject.transform.position.x);
        worldScript.y_positions.Add(currentObject.transform.position.y);
        worldScript.sizes.Add(currentObject.transform.localScale.x);

        currentObject.transform.position = new Vector3(currentObject.transform.position.x, currentObject.transform.position.y, -1f);

        if (currentObject.tag == "Habitat")
        {
            HabitatStats statsScript = currentObject.GetComponent<HabitatStats>();
            worldScript.habitatsSpaceLeft.Add(statsScript.spaceLeft);
            worldScript.habitats.Add(currentObject);
            statsScript.worldScriptIndex = worldScript.habitats.IndexOf(currentObject);
            statsScript.objectIndex = worldScript.objects.IndexOf(currentObject);
            statsScript.placed = true;
        }


        previousObject = null;
        currentObject = null;

        previousSnappedPos = new Vector2(800f, 800f);

        if (continuousPlacingMode == true)
        {
            int num = Random.Range(0, continousPrefabs.Count);
            prefab = continousPrefabs[num];

            TogglePlacing(prefab, true, true, true);
        }
    }

    public void DeleteTerrainAtPosition()
    {
        //deletes any trees or rocks at same position
        for (int i = 0; i < worldScript.x_positions.Count; i++)
        {
            if (worldScript.x_positions[i] == currentObject.transform.position.x && worldScript.y_positions[i] == currentObject.transform.position.y)
            {
                //ensures only trees or rocks
                if (worldScript.objects[i].tag == "Terrain")
                {

                    Destroy(worldScript.objects[i]);
                    worldScript.objects.RemoveAt(i);
                    worldScript.names.RemoveAt(i);
                    worldScript.x_positions.RemoveAt(i);
                    worldScript.y_positions.RemoveAt(i);
                    worldScript.sizes.RemoveAt(i);
                }

            }
        }

        TriggerDetection touchingScript = currentObject.GetComponent<TriggerDetection>();
        GameObject ob = touchingScript.collisionObject;
        int index = worldScript.objects.IndexOf(ob);
        if (index > -1)
        {
            Destroy(worldScript.objects[index]);
            worldScript.objects.RemoveAt(index);
            worldScript.names.RemoveAt(index);
            worldScript.x_positions.RemoveAt(index);
            worldScript.y_positions.RemoveAt(index);
            worldScript.sizes.RemoveAt(index);
        }
        
    }
}