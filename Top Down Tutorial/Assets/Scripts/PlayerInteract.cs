using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    public float rayDist;
    public bool holding;
    public Transform hold;
    public GameObject heldObject;

    private void Update()
    {
        RaycastHit2D grabCheck = Physics2D.Raycast(hold.position, Vector2.right * transform.localScale.x, rayDist);
        if (grabCheck.collider)
        {
            //Debug.Log(grabCheck.collider.name);
            //Debug.Log(grabCheck.transform.tag);
        }
        //if (heldObject != null)
        //{
        //    heldObject.transform.position = hold.position;
        //}

        if (Input.GetKeyDown(KeyCode.G)) // pick up / put down
        {
            if (!holding && grabCheck.collider != null)
            {
                if (grabCheck.collider.CompareTag("FoodObject"))
                {
                    holding = true;
                    heldObject = grabCheck.collider.gameObject;
                    heldObject.transform.parent = transform;
                    heldObject.transform.position = hold.position;
                    heldObject.GetComponent<BoxCollider2D>().enabled = false;
                }
                if(grabCheck.collider.CompareTag("Station") && grabCheck.collider.GetComponent<Station>().hasFood)
                {
                    holding = true;
                    heldObject = grabCheck.collider.GetComponent<Station>().food; // food is now the station's food
                    heldObject.transform.parent = transform;
                    grabCheck.collider.GetComponent<Station>().hasFood = false;  // station no longer has food
                    grabCheck.collider.GetComponent<Station>().food = null; // get rid of station food
                    heldObject.GetComponent<BoxCollider2D>().enabled = false;
                }
                if (grabCheck.collider.CompareTag("Dispenser"))
                {
                    holding = true;
                    heldObject = grabCheck.collider.GetComponent<Spawner>().spawnFood();
                    heldObject.transform.parent = transform;
                    heldObject.transform.position = hold.position;
                    heldObject.GetComponent<BoxCollider2D>().enabled = false;
                }
                if (grabCheck.collider.CompareTag("Counter") && grabCheck.collider.GetComponent<Counter>().hasFood)
                {
                    holding = true;
                    heldObject = grabCheck.collider.GetComponent<Counter>().foods[grabCheck.collider.GetComponent<Counter>().foods.Count - 1];
                    grabCheck.collider.GetComponent<Counter>().foods.RemoveAt(grabCheck.collider.GetComponent<Counter>().foods.Count - 1);
                    grabCheck.collider.GetComponent<Counter>().foods.TrimExcess();
                    grabCheck.collider.GetComponent<Counter>().hasFood = grabCheck.collider.GetComponent<Counter>().foods.Count > 0 ? true : false;

                    heldObject.transform.parent = transform;
                    heldObject.transform.position = hold.position;
                    heldObject.GetComponent<BoxCollider2D>().enabled = false;
                }
            }

            else if(holding)
            {
                if(grabCheck.collider != null && grabCheck.collider.CompareTag("Station") && !grabCheck.collider.GetComponent<Station>().hasFood)
                {
                    holding = false;
                    grabCheck.collider.GetComponent<Station>().food = heldObject; // station food is now player food
                    heldObject = null; // player has no food
                    grabCheck.collider.GetComponent<Station>().hasFood = true; // station has food
                    grabCheck.collider.GetComponent<Station>().food.GetComponent<BoxCollider2D>().enabled = false;
                }
                else if (grabCheck.collider != null && grabCheck.collider.CompareTag("Counter"))
                {
                    holding = false;
                    heldObject.transform.parent = grabCheck.collider.transform;
                    heldObject.transform.position = grabCheck.transform.position;
                    grabCheck.collider.GetComponent<Counter>().foods.Add(heldObject); // station food is now player food
                    heldObject = null; // player has no food
                    grabCheck.collider.GetComponent<Counter>().hasFood = true; // station has food
                    grabCheck.collider.GetComponent<Counter>().foods[grabCheck.collider.GetComponent<Counter>().foods.Count-1].GetComponent<BoxCollider2D>().enabled = false;
                    grabCheck.collider.GetComponent<Counter>().checkContents();
                }
                else
                {
                    holding = false;
                    heldObject.GetComponent<BoxCollider2D>().enabled = true;
                    heldObject.transform.parent = null;
                    heldObject = null;
                } 
            }
        }
        if (Input.GetKey(KeyCode.E) && !holding)
        {
            if(grabCheck.collider && grabCheck.collider.CompareTag("Station") && grabCheck.collider.GetComponent<Station>().hasFood)
            {
                Food f = grabCheck.collider.GetComponent<Station>().food.GetComponent<Food>();
                if(f.status <= 0)
                {
                    f.state = "Cooked";
                    f.status = 0f;
                }
                else
                {
                    f.status -= Time.deltaTime;
                }
                
            }
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (grabCheck.collider && grabCheck.collider.CompareTag("Station") && grabCheck.collider.GetComponent<Station>().hasFood && grabCheck.collider.GetComponent<Station>().timed)
            {
                Food f = grabCheck.collider.GetComponent<Station>().food.GetComponent<Food>();
                f.isCooking = true;
            }
        }
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(hold.position, hold.position + Vector3.right * transform.localScale.x * rayDist);
    }
}
