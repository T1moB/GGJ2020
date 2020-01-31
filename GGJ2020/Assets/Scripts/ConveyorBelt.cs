using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConveyorBelt : MonoBehaviour
{
    public GameObject belt;
    public Transform endPoint;
    public float speed;

    private void OnTriggerStay(Collider other)
    {
        other.transform.position = Vector3.MoveTowards(other.transform.position, endPoint.transform.position, speed * Time.deltaTime);
    }
}
