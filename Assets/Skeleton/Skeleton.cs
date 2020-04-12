using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton : MonoBehaviour
{
    Transform player;

    // Start is called before the first frame update
    void Awake()
    {
        player = GameObject.Find("Player").transform;
        transform.position = new Vector3(player.position.x - 1f, 1.8f, player.position.z);
    }

    private void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
