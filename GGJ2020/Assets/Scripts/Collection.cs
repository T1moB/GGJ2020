using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collection : MonoBehaviour
{
    [SerializeField] private Request request;
    private Transform[] partPositions;
    private bool collected = false;
    private int itemsCollected = 0;

    
    void Start()
    {
        partPositions = GetComponentsInChildren<Transform>();
    }

    public bool CheckCollection(Parts currentPart)
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
                    currentPart.transform.localPosition = Vector3.zero;
                    currentPart.tag = "Untagged";
                }
            }
            itemsCollected++;
            if(itemsCollected >= 4)
            {
                collected = true;
                request.RequestCompleted();
                foreach(Parts p in GetComponentsInChildren<Parts>())
                {
                    Destroy(p.gameObject);
                }
                itemsCollected = 0;
            }
            return true;
        }
        return false;
    }
}
