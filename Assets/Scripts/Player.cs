﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Configuration variables and objects
    [Header("Movement")]
    [SerializeField] float movementSpeed = 10f;
    [SerializeField] float padding = 1f;
    [Header("Firing")]
    [SerializeField] GameObject playerLaserPrefab;
    [SerializeField] float laserSpeed = 25f;
    [SerializeField] float laserFiringPeriod = 0.3f;
    [SerializeField] AudioClip laserSfx;
    [SerializeField] [Range(0, 1)] float laserSfxVolume = 0.3f;
    [Header("Health")]
    [SerializeField] int health = 500;
    [Header("Death")]
    [SerializeField] GameObject explosionVfx;
    [SerializeField] AudioClip explosionSfx;
    [SerializeField] [Range(0, 1)] float explosionSfxVolume = 0.8f;

    Coroutine firingCoroutine;

    float xMin, xMax, yMin, yMax;

    // Start is called before the first frame update
    void Start()
    {
        SetupMovementBoundaries();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Fire();
    }

    private void SetupMovementBoundaries()
    {
        Camera gameCamera = Camera.main;
        xMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x + padding;
        xMax = gameCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x - padding;
        yMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).y + padding;
        yMax = gameCamera.ViewportToWorldPoint(new Vector3(0, 1, 0)).y - padding;
    }

    private void Move()
    {
        // Get input movement
        var deltaX = Input.GetAxis("Horizontal") * Time.deltaTime * movementSpeed;
        var deltaY = Input.GetAxis("Vertical") * Time.deltaTime * movementSpeed;

        // Calculate new position, within viewport
        var newXPos = Mathf.Clamp(transform.position.x + deltaX, xMin, xMax);
        var newYPos = Mathf.Clamp(transform.position.y + deltaY, yMin, yMax);

        // Set new position
        transform.position = new Vector2(newXPos, newYPos);
    }

    private void Fire()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            firingCoroutine = StartCoroutine(continuousFiring());
        }
        if (Input.GetButtonUp("Fire1"))
        {
            StopCoroutine(firingCoroutine);
        }
    }

    IEnumerator continuousFiring()
    {
        while (true)
        {
            GameObject playerLaser =
                Instantiate(playerLaserPrefab, transform.position, Quaternion.identity) as GameObject;
            playerLaser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, laserSpeed);
            AudioSource.PlayClipAtPoint(laserSfx, Camera.main.transform.position, laserSfxVolume);
            yield return new WaitForSeconds(laserFiringPeriod);
        }
    }

    private void OnTriggerEnter2D(Collider2D otherObject)
    {
        DamageDealer otherObjectDamageDealer = otherObject.GetComponent<DamageDealer>();
        if (!otherObjectDamageDealer) return;
        ProcessHit(otherObjectDamageDealer);
    }

    private void ProcessHit(DamageDealer otherObjectDamageDealer)
    {
        if(otherObjectDamageDealer.GetDamage() >= health)
        {
            health = 0;
            ProcessDestruction();
        }
        else
        {
            health -= otherObjectDamageDealer.GetDamage();
        }

        otherObjectDamageDealer.Hit();
    }

    private void ProcessDestruction()
    {
        FindObjectOfType<Level>().LoadGameOver();
        Destroy(gameObject);
        explosionVfx = Instantiate(explosionVfx, transform.position, transform.rotation);
        AudioSource.PlayClipAtPoint(explosionSfx, Camera.main.transform.position, explosionSfxVolume);
        Destroy(explosionVfx, 1f);
    }

    public int GetHealth()
    {
        return health;
    }
}
