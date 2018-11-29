using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    public float enemyHealth = 100.0f;
    Collider enemyHitBox;
    public bool wasHit = false;

    // Use this for initialization
    void Start()
    {
        enemyHitBox = GetComponent<Collider>();
    }

    // Update is called once per frame
    void Update()
    {
        if (wasHit == true)
        {
            //play hit animation
        }
    }
}
