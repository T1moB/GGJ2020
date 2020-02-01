using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Request : MonoBehaviour
{
    [SerializeField]
    Parts[] requestedParts = new Parts[4];
    
    private GameObject requestPartObject;

    private GameObject requestCamera;

    private int requestCompleted = 0;
    //Je kan gewoon de parts nemen van de parts script
    void Start()
    {
        requestCamera = GameObject.Find("RequestCamera");
        StartCoroutine(DisableRequestCamera());

        //Find child parts of requested object
        requestedParts = GetComponentsInChildren<Parts>();
    }

    public void RequestCompleted()
    {
        requestCompleted++;
        foreach(Parts p in requestedParts)
        {
            p.generatePart();
        }
        //Reset the requested parts
    }

    private IEnumerator DisableRequestCamera()
    {
        yield return new WaitForSeconds(0.5f);
        requestCamera.SetActive(false);
    }

    public Parts[] GetRequestedParts()
    {
        return requestedParts;
    }

    public bool isPartInRequest(Parts currentPart) 
    {
        foreach(Parts p in requestedParts)
        {
            if(currentPart.myPartType == p.myPartType && currentPart.partColor == p.partColor)
            {
                return true;
            }
        }
        return false;
    }
}
