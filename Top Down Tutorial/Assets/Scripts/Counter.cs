using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Collections.Generic;

public class Counter : MonoBehaviour
{
    public List<GameObject> foods = new List<GameObject>();
    public bool hasFood;

    public Dish[] dishes;

    bool contains;
    public void checkContents()
    {
        Debug.Log("checking");
        foreach(Dish dish in dishes)
        {
            contains = false;
            foreach (GameObject ingredient in dish.ingredients)
            {
                Debug.Log("FE");
                if (foods.Contains(ingredient))
                {
                    contains = true;
                }
                else
                {
                    contains = false;
                }
            }
            if (contains)
            {
                Debug.Log("Contains");
            }
        }
    }
}
