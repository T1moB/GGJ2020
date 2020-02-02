using UnityEngine;

public class ConveyorBelt : MonoBehaviour
{
    public GameObject belt;
    public Transform spawnPoint;
    public Transform endPoint;
    public float speed;
    public float time = 3f;
    public GameObject[] parts;

    private void Start()
    {
    }

    public void StartSpawning()
    {
        InvokeRepeating("Spawn", 0f, time);
    }

    private void OnTriggerStay(Collider other)
    {
        other.transform.position = Vector3.MoveTowards(other.transform.position, endPoint.transform.position, speed * Time.deltaTime);
    }

    private void Spawn()
    {
        //spawn a random
        Instantiate(parts[Random.Range(0, parts.Length)], spawnPoint.position, new Quaternion(0, 0, 0, 0));
    }
}
