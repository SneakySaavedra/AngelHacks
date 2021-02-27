using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StirringStation : MonoBehaviour
{
    public Transform foodPoint;
    public GameObject food;
    public bool hasFood;

    private void Update()
    {

        if (food != null) //fix this and have this functionality in the player script
        {
            food.transform.parent = transform;
            food.transform.position = new Vector3(foodPoint.position.x, foodPoint.position.y, food.transform.position.z);
            hasFood = true;
        }
        else
        {
            hasFood = false;
        }
    }
}