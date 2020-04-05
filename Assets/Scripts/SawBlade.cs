using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SawBlade : MonoBehaviour
{

    public float rotationSpeed;
    private GameObject gameMaster;
    private GameMaster masterScript;
    public int damageAmount;

    private void Start()
    {

    }

    void Update()
    {
        transform.Rotate(new Vector3(0, 0, rotationSpeed));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
           

        }
    }
}
