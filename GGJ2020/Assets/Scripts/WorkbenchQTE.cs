using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkbenchQTE : MonoBehaviour
{
    const int fixTimer = 5;

    private GameObject currentGameobject;
    
    [SerializeField] private GameObject circle;
    [SerializeField] private GameObject arrow;
    [SerializeField] private GameObject brokenParticle;
    [SerializeField] private Transform partPos;

    private bool broken = false;
    private bool QTEActive = false;

    // Update is called once per frame
    void Update()
    {
        if (QTEActive)
        {
            arrow.transform.Rotate(0, 0, 300 * Time.deltaTime);
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (arrow.transform.rotation.eulerAngles.y  > circle.transform.rotation.eulerAngles.y - 15 && 
                    arrow.transform.rotation.eulerAngles.y  < circle.transform.rotation.eulerAngles.y + 15)
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

    public void StartQTE()
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

    public void SetCurrentGameobject(GameObject gb)
    {
        currentGameobject = gb;
        gb.transform.position = partPos.position;
    }
}
