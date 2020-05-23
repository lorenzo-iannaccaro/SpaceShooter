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
    [SerializeField] AudioClip laserSfx;
    [SerializeField] [Range(0, 1)] float laserSfxVolume = 0.5f;
    [Header("Death")] 
    [SerializeField] GameObject explosionVfx;
    [SerializeField] AudioClip explosionSfx;
    [SerializeField] [Range(0,1)] float explosionSfxVolume = 0.8f;

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
        AudioSource.PlayClipAtPoint(laserSfx, Camera.main.transform.position, laserSfxVolume);
    }

    private void OnTriggerEnter2D(Collider2D otherObject)
    {
        DamageDealer otherObjectDamageDealer = otherObject.GetComponent<DamageDealer>();
        if (!otherObjectDamageDealer) return;
        ProcessHit(otherObjectDamageDealer);
    }

    private void ProcessHit(DamageDealer otherObjectDamageDealer)
    {
        health -= otherObjectDamageDealer.GetDamage();
        otherObjectDamageDealer.Hit();
        if (health <= 0)
        {
            ProcessDestruction();
        }
    }

    private void ProcessDestruction()
    {
        Destroy(gameObject);
        explosionVfx = Instantiate(explosionVfx, transform.position, transform.rotation);
        AudioSource.PlayClipAtPoint(explosionSfx, Camera.main.transform.position, explosionSfxVolume);
        Destroy(explosionVfx, 1f);
    }
}
