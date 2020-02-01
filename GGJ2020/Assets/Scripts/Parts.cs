using UnityEngine;

public class Parts : MonoBehaviour
{
    public GameObject model;
    public bool partIsFixed = false;

    public Color partColor;
    public partType myPartType;

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

        GetComponent<Renderer>().material.SetColor("_Color", partColor);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Trashcan")
        {
            Destroy(gameObject);
        }
    }

}
