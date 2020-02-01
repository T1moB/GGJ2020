using UnityEngine;
using InControl;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;
    public InputDevice Device;
    public float speed = 12f;
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

    // Update is called once per frame
    void Update()
    {
        Pickup();
        Movement();
    }

    private void Pickup()
    {
        //if player presses action button and is not holding shit
        if (Device.Action1 && !isHolding)
        {
            //check if object in facing direction, 
            RaycastHit hit;
            Physics.Raycast(pickupCheck.position, pickupCheck.forward, out hit, pickupDistance, pickupMask);
            if (hit.transform)
            {
                isHolding = true;
                //then make player parent 
                hit.transform.parent = model;
                //and place infornt
                hit.transform.position = pickupCheck.position;
                heldItem = hit.transform.gameObject;
                Rigidbody rb = heldItem.GetComponent<Rigidbody>();
                rb.useGravity = false;
                rb.constraints = RigidbodyConstraints.FreezePosition;
            }
            //isholding = false; when dropping the shit
        }
    }

    private void Movement()
    {
        float x = Device.LeftStickX;// Input.GetAxis(hAxis);
        float z = Device.LeftStickY;//Input.GetAxis(vAxis);

        Vector3 move = transform.right * x + transform.forward * z;
        Rotate(move);

        controller.Move(move * speed * Time.deltaTime);
    }

    private void Rotate(Vector3 move)
    {
        //rotate model based on movement direction
        Vector3 lookDirection = new Vector3(move.x, 0, move.z);
        if (lookDirection != Vector3.zero)
            model.rotation = Quaternion.LookRotation(lookDirection * Time.deltaTime);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.name == "Workbench")
        {
            if (isHolding && Device.Action1)
            {
                WorkbenchQTE wQTE = other.gameObject.GetComponent<WorkbenchQTE>();
                wQTE.SetCurrentGameobject(heldItem);
                wQTE.StartQTE();
            }
        }
    }
}
