using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkbenchQTE : MonoBehaviour
{
    const int fixTimer = 5;

    public GameObject currentGameobject;
    
    [SerializeField] private GameObject circle;
    [SerializeField] private GameObject arrow;
    [SerializeField] private GameObject brokenParticle;

    private bool broken = false;
    private bool QTEActive = false;

    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            StartQTE();
        }

        if (QTEActive)
        {

            arrow.transform.Rotate(0, 0, 300 * Time.deltaTime);
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (arrow.transform.rotation.eulerAngles.z  > circle.transform.rotation.eulerAngles.z - 15 && 
                    arrow.transform.rotation.eulerAngles.z  < circle.transform.rotation.eulerAngles.z + 15)
                {
                    //succes
                    //currentGameobject.GetComponent<Part>().partIsFixed = true;
                    Debug.Log("Fixed");
                }
                else
                {
                    broken = true;
                    brokenParticle.SetActive(true);
                    Debug.Log("You got fucked");
                    StartCoroutine(Fix());
                }

                circle.SetActive(false);
                //arrow.SetActive(false);
                QTEActive = false;
            }
        }
    }

    void StartQTE()
    {
        if (broken) { return; }
        circle.SetActive(true);
        //arrow.SetActive(true);

        circle.transform.Rotate(0,0,Random.Range(1,359));
        arrow.transform.rotation = new Quaternion(0,0,0,0);

        QTEActive = true;

    }

    private IEnumerator Fix() 
    {
        yield return new WaitForSeconds(fixTimer);
        broken = false;
        brokenParticle.SetActive(false);
    }
}
