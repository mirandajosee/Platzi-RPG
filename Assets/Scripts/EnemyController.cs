﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float enemySpeed = 2;
    private Rigidbody2D enemyRigidBody;

    private bool isMoving;

    public float timeBetweenSteps;
    private float timeBetweenStepsCounter;

    public float timeToMakeStep;
    private float timeToMakeStepCounter;

    public Vector2 directionToMakeStep;

    private Animator enemyAnimator;
    private const string horizontal = "Horizontal";
    private const string vertical = "Vertical";

    // Start is called before the first frame update
    void Start()
    {
        enemyRigidBody = GetComponent<Rigidbody2D>();
        enemyAnimator = GetComponent<Animator>();

        timeBetweenStepsCounter = timeBetweenSteps*Random.Range(0.5f,1.5f);
        timeToMakeStepCounter = timeToMakeStep * Random.Range(0.5f, 1.5f);
    }

    // Update is called once per frame
    void Update()
    {
        if (isMoving)
        {
            timeToMakeStepCounter -= Time.deltaTime;
            enemyRigidBody.velocity = directionToMakeStep;

            if (timeToMakeStepCounter < 0)
            {
                isMoving = false;
                timeBetweenStepsCounter = timeBetweenSteps;
                enemyRigidBody.velocity = Vector2.zero;
            }
        }
        else
        {
            timeBetweenStepsCounter -= Time.deltaTime;
            if (timeBetweenStepsCounter < 0)
            {
                isMoving = true;
                timeToMakeStepCounter = timeToMakeStep;

                directionToMakeStep = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f))*enemySpeed;
            }
        }

        enemyAnimator.SetFloat(horizontal, directionToMakeStep.x);
        enemyAnimator.SetFloat(vertical, directionToMakeStep.y);
    }
}
