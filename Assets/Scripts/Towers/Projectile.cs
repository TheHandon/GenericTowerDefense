﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Projectile : MonoBehaviour
{
    public float speed = 10.0f;
    public float damage = 1.0f;

    public GameObject target { get; set; }

    Rigidbody rb;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        target = FindObjectOfType<Enemy>().gameObject;
    }

    private void Update()
    {
        Vector3 direction = Vector3.zero;

        direction += (target.transform.position - transform.position).normalized;

        rb.AddForce(direction * (speed * 200) * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            Destroy(this.gameObject);
        }
    }
}
