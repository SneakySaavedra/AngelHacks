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
            Debug.Log(grabCheck.transform.tag);
        }
        if (heldObject != null)
        {
            heldObject.transform.position = transform.position;
        }

        if (Input.GetKeyDown(KeyCode.G))
        {
            if (!holding && grabCheck.collider != null)
            {
                if (grabCheck.collider.CompareTag("FoodObject"))
                {
                    holding = true;
                    heldObject = grabCheck.collider.gameObject;
                    heldObject.transform.parent = transform;
                }
                if(grabCheck.collider.CompareTag("Station") && grabCheck.collider.GetComponent<StirringStation>().hasFood)
                {
                    holding = true;
                    heldObject = grabCheck.collider.GetComponent<StirringStation>().food; // food is now the station's food
                    heldObject.transform.parent = transform;
                    grabCheck.collider.GetComponent<StirringStation>().hasFood = false;  // station no longer has food
                    grabCheck.collider.GetComponent<StirringStation>().food = null; // get rid of station food
                }
            }

            else if(holding)
            {
                if(grabCheck.collider != null && grabCheck.collider.CompareTag("Station") && !grabCheck.collider.GetComponent<StirringStation>().hasFood)
                {
                    holding = false;
                    grabCheck.collider.GetComponent<StirringStation>().food = heldObject; // station food is now player food
                    heldObject = null; // player has no food
                    grabCheck.collider.GetComponent<StirringStation>().hasFood = true; // station has food
                }
                else
                {
                    holding = false;
                    heldObject.transform.parent = null;
                    heldObject = null;
                } 
            }
        }
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(hold.position, hold.position + Vector3.right * transform.localScale.x * rayDist);
    }
}
