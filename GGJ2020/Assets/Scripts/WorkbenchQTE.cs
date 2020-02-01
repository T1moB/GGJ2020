using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkbenchQTE : MonoBehaviour
{
    const int fixTimer = 5;

    private GameObject currentGameobject;
    private GameObject currentPlayer;

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
            if (currentPlayer.GetComponent<PlayerMovement>().Device.Action2)
            {
                if (arrow.transform.rotation.eulerAngles.y  > circle.transform.rotation.eulerAngles.y - 15 && 
                    arrow.transform.rotation.eulerAngles.y  < circle.transform.rotation.eulerAngles.y + 15)
                {
                    //succes
                    //currentGameobject.GetComponent<Part>().partIsFixed = true;
                    Debug.Log("Fixed");
                    currentGameobject.GetComponent<Parts>().partIsFixed = true;
                    StartCoroutine(currentPlayer.GetComponent<PlayerMovement>().WorkAnimation(false));
                }
                else
                {
                    broken = true;
                    brokenParticle.SetActive(true);
                    currentPlayer.GetComponent<PlayerMovement>().FailAnimation();

                    StartCoroutine(currentPlayer.GetComponent<PlayerMovement>().WorkAnimation(false, 1));
                    StartCoroutine(Fix());
                }
                currentPlayer.GetComponent<PlayerMovement>().canMove = true;
                currentGameobject = null;
                //arrow.SetActive(false);
                QTEActive = false;
                circle.SetActive(false);
            }
        }
    }

    public void StartQTE()
    {
        if (broken) { return; }
        circle.SetActive(true);
        //arrow.SetActive(true);
        StartCoroutine(currentPlayer.GetComponent<PlayerMovement>().WorkAnimation(true));
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

    public void SetCurrentGameobject(GameObject gb, Transform player)
    {
        currentGameobject = gb;
        gb.transform.position = partPos.position;
        currentPlayer = player.gameObject;
    }
}
