using UnityEngine;

public class Parts : MonoBehaviour
{

    //public struct partProperties
    //{
    public string partType;
    public string partColor;
    string[] colors = new string[] { "Red", "Blue", "Yellow" };
    public string randomColor;

    public enum whichPartType
    {
        bottom,
        middle,
        upper,
        top
    }

    public whichPartType specificPartType;

    //For testing
    //[SerializeField]
    public bool partIsFixed = false;

    //Material partMaterial;
    public Renderer partRenderer;
    public GameObject partObject;
    //}

    void Start()
    {

        partObject = this.gameObject;
        partObject.name = this.gameObject.name;
        partRenderer = partObject.GetComponent<Renderer>();
        randomColor = colors[Random.Range(0, colors.Length)];

        switch (specificPartType)
        //switch (partObject.name)
        {
            case whichPartType.bottom:
                //case "Bottom-Part(Clone)":
                {
                    partType = "Bottom";
                    break;
                }
            case whichPartType.middle:
                //case "Middle-Part(Clone)":
                {
                    partType = "Middle";
                    break;
                }
            case whichPartType.upper:
                //case "Upper-Part(Clone)":
                {
                    partType = "Upper";
                    break;
                }
            case whichPartType.top:
                // case "Top-Part(Clone)":
                {
                    partType = "Top";
                    break;
                }
        }
        switch (randomColor)
        {
            case "Red":
                {
                    partColor = "Red";
                    partRenderer.material.SetColor("_Color", Color.red);
                    break;
                }
            case "Blue":
                {
                    partColor = "Blue";
                    partRenderer.material.SetColor("_Color", Color.blue);
                    break;
                }
            case "Yellow":
                {
                    partColor = "Yellow";
                    partRenderer.material.SetColor("_Color", Color.yellow);
                    break;
                }
        }

    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Trashcan")
        {
            Destroy(gameObject);
        }
    }

}
