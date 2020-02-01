using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Request : MonoBehaviour
{
    public string requestPartType;
    public string requestPartColor;
    //string[] colors = new string[] { "Red", "Blue", "Yellow" };
    //public string randomColor;

    private GameObject[] requestParts;

    //Material partMaterial;
    private Renderer requestPartRenderer;
    private GameObject requestPartObject;

    private GameObject requestCamera;
    //private Parts parts;
    //}

    //Je kan gewoon de parts nemen van de parts script
    void Start()
    {
        requestCamera = GameObject.Find("RequestCamera");
        requestPartType = gameObject.GetComponent<Parts>().partType;
        requestPartColor = gameObject.GetComponent<Parts>().partColor;

        for (int i = 0; i < requestParts.Length; i++)
        {

        }

       
   // StartCoroutine(DisableRequestCamera());


        //foreach (Transform child in transform)
        //{
        //    //child is your child transform
        //}

    }
// Update is called once per frame
////void Update()
////{
////        public List<GameObject> Children;

////        foreach (Transform child in transform)
////         {
////             if (child.tag == "Tag")
////             {
////                 Children.Add(child.gameObject);
////             }
////         }
////    DisableRequestCamera();
////}

void RequestCompleted()
{

}

private IEnumerator DisableRequestCamera()
{
    yield return new WaitForSeconds(0.5f);
    requestCamera.SetActive(false);
}


}
