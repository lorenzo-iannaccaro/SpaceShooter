﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] int health = 200;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D otherObject)
    {
        DamageDealer otherObjectDamageDealer = otherObject.GetComponent<DamageDealer>();
        ProcessHit(otherObjectDamageDealer);
    }

    private void ProcessHit(DamageDealer otherObjectDamageDealer)
    {
        health -= otherObjectDamageDealer.GetDamage();
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
