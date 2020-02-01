using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;

    public float speed = 12f;
    public Transform model;

    public Transform pickupCheck;
    public float pickupDistance;
    public LayerMask pickupMask;

    private bool isHolding = false;
    private GameObject holdedItem;

    private void OnDrawGizmos()
    {
        if (pickupCheck)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(pickupCheck.position, pickupCheck.position + pickupCheck.forward * pickupDistance);
        }
    }

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
        if (Input.GetKeyDown(KeyCode.F) && !isHolding)
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
                holdedItem = hit.transform.gameObject;
                Rigidbody rb = holdedItem.GetComponent<Rigidbody>();
                rb.useGravity = false;
                rb.constraints = RigidbodyConstraints.FreezePosition;
            }
            //isholding = false; when dropping the shit
        }
    }

    private void Movement()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

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
        if(other.name == "Workbench")
        {
            if (isHolding && Input.GetKeyDown(KeyCode.F))
            {
                WorkbenchQTE wQTE = other.gameObject.GetComponent<WorkbenchQTE>();
                wQTE.SetCurrentGameobject(holdedItem);
                wQTE.StartQTE();
            }
        }
    }
}
