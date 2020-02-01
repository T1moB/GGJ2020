using UnityEngine;

public class Parts : MonoBehaviour
{

    //public struct partProperties
    //{
    public string partType;
    public string partColor;
    string[] colors = new string[] { "Red", "Blue", "Yellow" };
    public string randomColor;

    //For testing
    [SerializeField]
    bool partIsFixed = false;

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

        switch (partObject.name)
        {
            case "Bottom-Part":
                {
                    partType = "Bottom";
                    break;
                }
            case "Middle-Part":
                {
                    partType = "Middle";
                    break;
                }
            case "Upper-Part":
                {
                    partType = "Upper";
                    break;
                }
            case "Top-Part":
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
