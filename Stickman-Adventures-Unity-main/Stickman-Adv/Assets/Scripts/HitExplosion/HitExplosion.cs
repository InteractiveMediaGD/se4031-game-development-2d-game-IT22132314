using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitExplosion : MonoBehaviour
{
    private float lifeTime = 0.2f;


    void Update()
    {
        lifeTime -= Time.deltaTime;
        if (lifeTime <= 0)
        {
            Destroy(gameObject);
        }        
    }
}
