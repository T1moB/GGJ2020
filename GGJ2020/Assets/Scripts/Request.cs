using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Request : MonoBehaviour
{
    [SerializeField]
    Parts[] requestedParts = new Parts[4];
    
    private GameObject requestPartObject;

    private GameObject requestCamera;

    //Je kan gewoon de parts nemen van de parts script
    void Start()
    {
        requestCamera = GameObject.Find("RequestCamera");
        StartCoroutine(DisableRequestCamera());

        //Find child parts of requested object
        requestedParts = GetComponentsInChildren<Parts>();
    }
    // Update is called once per frame
    void Update()
    {
        IsSetCompleted();

    }
    private bool IsSetCompleted()
    {
        for (int i = 0; i < requestedParts.Length; i++)
        {
            if (requestedParts[i].partIsFixed == false)
            {
                return false;
            }
        }
        return true;
    }

    void RequestCompleted()
    {
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
