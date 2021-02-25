using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    public float rayDist;
    public bool isHolding;
    public Transform left;
    public Transform right;
    public GameObject heldObject;

    private void Update()
    {
        RaycastHit2D grabCheck = Physics2D.Raycast(right.position, Vector2.right * transform.localScale.x, rayDist);

        if (Input.GetKeyDown(KeyCode.G))
        {
            Debug.Log("G is pressed");
            if (!isHolding && grabCheck.collider)
            {
                if (grabCheck.collider.CompareTag("FoodObject") && !grabCheck.collider.GetComponent<Food>().isHeld)
                {
                    GameObject col = grabCheck.collider.gameObject;
                    Debug.Log("Contact with food");
                    col.GetComponent<Food>().isHeld = true;
                    heldObject = grabCheck.collider.gameObject;
                    col.transform.position = transform.position;
                    col.transform.parent = transform;
                    isHolding = true;

                }

                if(grabCheck.collider.CompareTag("Station"))
                {
                    Debug.Log("Grabbing from station");
                    GameObject col = grabCheck.collider.gameObject;
                    heldObject = col.GetComponent<StirringStation>().food;
                    col.GetComponent<StirringStation>().food = null;
                    heldObject.transform.parent = transform;
                    heldObject.transform.position = transform.position;
                    isHolding = true;
                }
            }
            else
            {
                heldObject.transform.parent = null;
                heldObject.GetComponent<Food>().isHeld = false;
                isHolding = false;
            }
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            if (isHolding)
            {
                if (grabCheck.collider && grabCheck.collider.CompareTag("Station"))
                {
                    heldObject.GetComponent<Food>().isHeld = false;
                    grabCheck.collider.GetComponent<StirringStation>().food = heldObject;
                    heldObject = null;
                    isHolding = false;
                }
            }
        }
    }
    private void FixedUpdate()
    {
        
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(right.position, right.position + Vector3.right * transform.localScale.x * rayDist);
    }
}
