﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Station : MonoBehaviour
{
    public Transform foodPoint;
    public GameObject food;
    public bool hasFood;
    public bool timed;
    public ProgressBar timer;
    public GameObject bar;

    private void Start()
    {
        bar.GetComponent<RectTransform>().anchoredPosition = transform.position.x > 0 ? new Vector2(1.25f, 0) : new Vector2(-1.25f, 0);
    }

    private void Update()
    {

        if (food != null) //fix this and have this functionality in the player script
        {
            bar.SetActive(true);
            food.transform.parent = transform;
            food.transform.position = new Vector3(foodPoint.position.x, foodPoint.position.y, food.transform.position.z);
            hasFood = true;
            timer.max = (int)(food.GetComponent<Food>().cookTime * 10);
            timer.current = (int)(food.GetComponent<Food>().status * 10);
        }
        else
        {
            bar.SetActive(false);
            hasFood = false;
            timer.current = 10;
            timer.max = 10;
        }
    }
}