using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Request : MonoBehaviour
{
    [SerializeField]
    private string[] requestPartType;   //= [4];
    public string requestPartColor;
    //string[] colors = new string[] { "Red", "Blue", "Yellow" };
    //public string randomColor;

    private GameObject[] requestParts;

    [SerializeField]
    Parts[] parts = new Parts[4];

    //Material partMaterial;
    private Renderer requestPartRenderer;
    private GameObject requestPartObject;

    private GameObject requestCamera;

    public List<GameObject> childParts;
    //private Parts parts;
    //}

    //Je kan gewoon de parts nemen van de parts script
    void Start()
    {
        requestCamera = GameObject.Find("RequestCamera");
        requestPartType = new string[4];
        StartCoroutine(DisableRequestCamera());

        //Find child parts of requested object
        parts = GetComponentsInChildren<Parts>();
        for (int i = 0; i < parts.Length; i++)
        {
            Debug.Log(parts.Length);
            Debug.Log(parts[i].partType);
            requestPartType[i] = parts[i].partType;
        }
    }
    // Update is called once per frame
    void Update()
    {
        DisableRequestCamera();
        IsSetCompleted();

    }
    private bool IsSetCompleted()
    {
        for (int i = 0; i < parts.Length; i++)
        {
            if (parts[i].partIsFixed == false)
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


}
