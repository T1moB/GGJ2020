using UnityEngine;
using System.Collections;

public class Parts : MonoBehaviour
{
    //public GameObject model;
    public bool partIsFixed = false;
    private float despawnTime = 10f;
    public const float ResetTime = 10f;
    public Color partColor;
    public partType myPartType;
    public bool isPickedUp;

    public enum partType
    {
        bottom = 0,
        middle = 1,
        upper = 2,
        top = 3
    };
    
    void Start()
    {
        generatePart();
        TimeReset();
    }

    public void generatePart()
    {
        int randomColor = Random.Range(0, 3);

        switch (randomColor)
        {
            case 0:
                partColor = Color.red;
                break;
            case 1:
                partColor = Color.yellow;
                break;
            case 2:
                partColor = Color.blue;
                break;
        }

        GetComponentInChildren<Renderer>().material.SetColor("_Color", partColor);
    }

    public void TimeReset()
    {
        despawnTime = ResetTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Trashcan")
        {
            //check for player
            var parentObject = GetComponentInParent<PlayerMovement>();
            if (parentObject)
            {
                parentObject.heldItem = null;
                parentObject.isHolding = false;
            }
            //kill yourself
            Destroy(gameObject);
        }
    }

    public void Update()
    {
        //only update when not fixed
        if (!partIsFixed && !isPickedUp)
        {
            despawnTime -= Time.deltaTime;
            if (despawnTime < 0)
                Destroy(gameObject);
        }
    }

}
