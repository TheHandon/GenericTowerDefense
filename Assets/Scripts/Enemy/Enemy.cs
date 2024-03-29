﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Collider))]
public class Enemy : MonoBehaviour
{
    public float speed = 20.0f;
    public float health = 100.0f;

    public Material[] materials;

    public GameObject target;

    public Vector3 Velocity
    {
        get { return movement.velocity; }
        set { movement.velocity = value; }
    }

    public float TimeAlive { get; set; } = 0;

    NavMeshAgent movement;
    void Start()
    {
        movement = GetComponent<NavMeshAgent>();
        movement.speed = speed * 200.0f; //Speed gets multiplied as Terrain objects are 1kx1k areas(forced)
        UpdateDestination(target);

        int randIndex = Random.Range(0, materials.Length-1);
        gameObject.GetComponent<MeshRenderer>().material = materials[randIndex];
    }

    void Update()
    {
        movement.speed = speed;
        TimeAlive += Time.deltaTime;
    }

    public void ApplyForce(Vector3 force)
    {
        //
    }

    public void Stop()
    {
        movement.isStopped = true;
    }

    public void UpdateDestination(GameObject target)
    {
        movement.SetDestination(target.transform.position);
        this.target = target;
    }

    public void OnCollisionEnter(Collision collision)
    {
        GameObject colGO = collision.gameObject;

        if (colGO.tag == "Exit")
        {
            Destroy(this.gameObject);
        }
        else if (colGO.GetComponent<Projectile>())
        {
            Projectile proj = colGO.GetComponent<Projectile>();
            health -= proj.damage;
            DeathCheck();
        } else if (colGO.GetComponent<Explosion>())
        {
            Explosion exp = colGO.GetComponent<Explosion>();
            if (!exp.HasHit(this))
            {
                health -= exp.damage;
                DeathCheck();
            }
        }
    }

    private void DeathCheck()
    {
        if (health <= 0)
        {
            Destroy(this.gameObject);
            GameController.score += 25;
        }
    }
}