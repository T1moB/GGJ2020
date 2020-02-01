using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;

    public float speed = 12f;
    public float gravity = 9.81f;
    public bool canMove = true;
    public Transform model;

    public Transform pickupCheck;
    public float pickupDistance;
    public LayerMask pickupMask;

    private bool isHolding = false;
    private GameObject heldItem;

    public bool GetHolding()
    {
        return isHolding;
    }

    public void SetHolding(bool value)
    {
        isHolding = value;
    }

    public void FailAnimation()
    {
        model.GetComponent<Animator>().SetBool("Moving", true);
    }

    public IEnumerator WorkAnimation(bool status, int seconds = 0)
    {
        yield return new WaitForSeconds(seconds);
       model.GetComponent<Animator>().SetBool("Working", status);

    }

    // Update is called once per frame
    void Update()
    {
        if (canMove)
            Movement();
    }
    
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Part")
        {
            Pickup(other.gameObject);
        }

        if (other.name == "Workbench")
        {
            if (isHolding && Input.GetKeyDown(KeyCode.F))
            {
                WorkbenchQTE wQTE = other.gameObject.GetComponent<WorkbenchQTE>();
                wQTE.SetCurrentGameobject(heldItem, transform);
                wQTE.StartQTE();
                canMove = false;
            }
        }
    }

    private void Pickup(GameObject other)
    {
        //if player presses action button and is not holding shit
        if (Input.GetKeyDown(KeyCode.F) && !isHolding)
        {
            isHolding = true;
            //then make player parent 
            other.transform.parent = model;
            //and place infornt
            other.transform.position = pickupCheck.position;
            heldItem = other.transform.gameObject;
            Rigidbody rb = heldItem.GetComponent<Rigidbody>();
            rb.useGravity = false;
            rb.constraints = RigidbodyConstraints.FreezePosition;
            //isholding = false; when dropping the shit
        }
    }

    private void Movement()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = Vector3.right * x + Vector3.forward * z;
        Rotate(move);

        //set animation
        if (move.x != 0 || move.z != 0)
            model.GetComponent<Animator>().SetBool("Moving", true);
        else
            model.GetComponent<Animator>().SetBool("Moving", false);

        controller.Move(move * speed * Time.deltaTime);
        controller.Move(new Vector3(0, -gravity, 0) * Time.deltaTime * Time.deltaTime);
    }

    private void Rotate(Vector3 move)
    {
        //rotate model based on movement direction
        Vector3 lookDirection = new Vector3(move.x, 0, move.z);
        if (lookDirection != Vector3.zero)
            transform.rotation = Quaternion.LookRotation(lookDirection * Time.deltaTime);
    }
}
