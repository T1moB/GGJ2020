using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collection : MonoBehaviour
{

    private bool collected = false;
    [SerializeField] private Request request;
    [SerializeField] private Transform[] partPositions = new Transform[4]; 
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private bool CheckCollection(Parts currentPart)
    {
        if (request.isPartInRequest(currentPart))
        {
            string partTypePos = "";
            switch (currentPart.myPartType)
            {
                case Parts.partType.bottom:
                    partTypePos = "BottomPos";
                    break;
                case Parts.partType.middle:
                    partTypePos = "MiddlePos";
                    break;
                case Parts.partType.upper:
                    partTypePos = "UpperPos";
                    break;
                case Parts.partType.top:
                    partTypePos = "TopPos";
                    break;
            }

            for (int i = 0; i < partPositions.Length; i++)
            {
                if(partPositions[i].name == partTypePos)
                {
                    currentPart.transform.parent = partPositions[i];
                    currentPart.transform.position = Vector3.zero;
                }
            }
            return true;
        }
        return false;
    }
}
