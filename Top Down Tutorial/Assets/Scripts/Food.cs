using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour
{
    public string id;
    public bool isHeld;
    public float cookTime;
    public float status;
    public string state = "Raw";
    public bool isCooking;
    public Sprite completed;

    private void Update()
    {
        if (isCooking)
        {
            status -= Time.deltaTime;
            Mathf.Clamp(status, 0, cookTime);
        }

        if (status <= 0)
        {
            status = 0;
            isCooking = false;
            GetComponent<SpriteRenderer>().sprite = completed;
        }
    }
}
