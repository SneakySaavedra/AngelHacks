using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Spawner : MonoBehaviour
{
    public GameObject dispense;
    public GameObject frame;
    public Sprite image;
    private void Start()
    {
        frame.GetComponent<SpriteRenderer>().sprite = image;
    }

    public GameObject spawnFood()
    {
        return Instantiate(dispense);
    }
}
