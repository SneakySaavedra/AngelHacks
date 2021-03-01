using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Counter : MonoBehaviour
{
    public List<GameObject> foods = new List<GameObject>();
    public bool hasFood;

    public GameObject[] dishes;

    bool contains;
    public void checkContents()
    {

        foreach(GameObject dish in dishes)
        {
            bool hasAll = true;
            bool[] contains = new bool[dish.GetComponent<Dish>().ingredients.Length];
            List<GameObject> delete = new List<GameObject>();

            for(int i = 0; i < dish.GetComponent<Dish>().ingredients.Length; i++)
            {
                foreach(GameObject f in foods)
                {
                    if(dish.GetComponent<Dish>().ingredients[i].GetComponent<Food>().id == f.GetComponent<Food>().id)
                    {
                        contains[i] = true;
                        delete.Add(f);
                    }
                }
            }

            foreach (bool b in contains) Debug.Log(b.ToString());

            foreach (bool b in contains)
            {
                if (b == false)
                {
                    hasAll = false;
                }
            }
            Debug.Log("Has all is " + hasAll);

            if (hasAll)
            {
                foreach(GameObject f in delete)
                {
                    foods.Remove(f);
                    Destroy(f);
                }

                foods.TrimExcess();
                GameObject a = Instantiate(dish, transform);
                foods.Add(a);

            }
        }
    }
}
