﻿using System.Collections;
using UnityEngine;

public class ConveyorBelt : MonoBehaviour
{
    public GameObject belt;
    public Transform spawnPoint;
    public Transform endPoint;
    public float speed;
    public GameObject[] parts;

    private void Start()
    {
        InvokeRepeating("Spawn", 0f, 3f);
    }

    private void OnTriggerStay(Collider other)
    {
        other.transform.position = Vector3.MoveTowards(other.transform.position, endPoint.transform.position, speed * Time.deltaTime);
    }

    private void Spawn()
    {
        //spawn a random
        Instantiate(parts[Random.Range(0, parts.Length - 1)], spawnPoint.position, new Quaternion(0, 0, 0, 0));
    }
}
