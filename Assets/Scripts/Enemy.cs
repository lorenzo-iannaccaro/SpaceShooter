using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Health")]
    [SerializeField] int health = 200;
    [Header("Firing")]
    [SerializeField] GameObject enemyLaserPrefab;
    [SerializeField] float enemyLaserSpeed = -15f;
    float fireTimer;
    float fireTimerMin = 0.2f;
    float fireTimerMax = 2.4f;

    // Start is called before the first frame update
    void Start()
    {
        ResetFireTimer();
    }

    // Update is called once per frame
    void Update()
    {
        fireTimer -= Time.deltaTime;
        if(fireTimer <= 0)
        {
            Fire();
            ResetFireTimer();
        }
    }

    private void ResetFireTimer()
    {
        fireTimer = UnityEngine.Random.Range(fireTimerMin, fireTimerMax);
    }

    private void Fire()
    {
        GameObject enemyLaser = Instantiate(enemyLaserPrefab, transform.position, Quaternion.identity);
        enemyLaser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, enemyLaserSpeed);
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
