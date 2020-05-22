﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPath : MonoBehaviour
{
    [SerializeField] List<Transform> waypoints;
    [SerializeField] float moveSpeed = 5f;

    int waypointIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = waypoints[waypointIndex].transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Move();

    }

    private void Move()
    {
        if (waypointIndex < waypoints.Count)
        {
            float moveSpeedThisframe = moveSpeed * Time.deltaTime;
            var targetPosition = waypoints[waypointIndex].transform.position;
            transform.position =
                Vector2.MoveTowards(transform.position, targetPosition, moveSpeedThisframe);

            if (transform.position == targetPosition)
            {
                waypointIndex++;
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
