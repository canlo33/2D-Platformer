using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyParticles : MonoBehaviour
{
    public float destroyTimer;
    
    void Update()
    {
        destroyTimer -= Time.deltaTime;
        if(destroyTimer <= 0f)
        {
            Destroy(gameObject);
        }
    }
}
