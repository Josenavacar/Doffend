﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyAI : MonoBehaviour
{
    public Animator animator;

    [Header("Pathfinding")]
    public Transform target;
    public float activateDistance = 50f;
    public float pathUpdateSeconds = 0.5f;

    [Header("Physics")]
    public float speed = 200f;
    public float nextWaypointDistance = 3f;

    public float jumpNodeHeightRequirement = 0.8f;
    public float jumpModifier = 0.3f;
    public float jumpCheckOffset = 0.1f;


    [Header("Custom Behavior")]
    public bool followEnabled = true;
    public bool jumpEnabled = true;
    public bool directionLookEnabled = true;

    private Path path;
    private int currentWaypoint = 0;

    RaycastHit2D isGrounded;
    Seeker seeker;
    Rigidbody2D rb;
    public int forceApplied = 20;

    public void Start()
    {
        seeker = GetComponent<Seeker>();
        //Fetch Rigidbody from game object
        rb = GetComponent<Rigidbody2D>();
        //Ignore the collision between layer 12 (custom) and later 12 (custom)
        Physics2D.IgnoreLayerCollision(12, 12);

        InvokeRepeating("UpdatePath", 0f, pathUpdateSeconds);   
    }

    private void FixedUpdate()
    {
        if (TargetInDistance() && followEnabled)
        {
            PathFollow();
        }
    }

    private void UpdatePath()
    {
        if (followEnabled && TargetInDistance() && seeker.IsDone())
        {
            seeker.StartPath(rb.position, target.position, OnPathComplete);
        }
    }

    private void PathFollow()
    {
        if (path == null)
        {
            return;
        }
        //Reached end of path
        if (currentWaypoint >= path.vectorPath.Count)
        {
            return;
        }
        // See if colliding with anything
        Vector3 startOffset = transform.position - new Vector3(0f, GetComponent<Collider2D>().bounds.extents.y + jumpCheckOffset);
        isGrounded = Physics2D.Raycast(startOffset, -Vector3.up, 0.05f);

        //direction calculation
        Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized;
        Vector2 force = direction * speed * Time.deltaTime;

        // Jump
        if (jumpEnabled && isGrounded)
        {
            StartCoroutine(JumpCooldown());
        }

        IEnumerator JumpCooldown()
        {
            if (direction.y > jumpNodeHeightRequirement)
            {
                rb.AddForce(Vector2.up * jumpModifier);
            }

            jumpEnabled = false;
            yield return new WaitForSeconds(2);
            jumpEnabled = true;
        }


        //movement
        rb.AddForce(force);
        animator.SetFloat("Speed", Mathf.Abs(rb.velocity.x));

        //next waypoint
        float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);
        if (distance < nextWaypointDistance)
        {
            currentWaypoint++;
        }

        // Direction Graphics Handling
        if (directionLookEnabled)
        {
            if (rb.velocity.x > 0.05f)
            {
                transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            }
            else if (rb.velocity.x < -0.05f)
            {
                transform.localScale = new Vector3(-1f * Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            }
        }
    }

        private bool TargetInDistance()
        {
            return Vector2.Distance(transform.position, target.transform.position) < activateDistance;
        }

        private void OnPathComplete(Path p)
        {
            if (!p.error)
            {
                path = p;
                currentWaypoint = 0;
            }
        }

        void OnCollisionEnter2D(Collision2D collision)
        {
            if(collision.gameObject.GetComponent<Health>() != null)
            {
                if(gameObject.transform.position.x > target.transform.position.x)
                {
                    Vector2 push = Vector2.zero;
                    push.y = 1f;
                    push.x = target.position.x;
                    rb.AddForce(push * forceApplied);
                }
                else
                {
                    Vector2 push = Vector2.zero;
                    push.y = 1f;
                    push.x = -target.position.x;
                    rb.AddForce(push * forceApplied);
                }
            }
        }
    }
