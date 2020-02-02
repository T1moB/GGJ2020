﻿using System.Collections;
using UnityEngine;

public class WorkbenchQTE : MonoBehaviour
{
    const int fixTimer = 5;

    private GameObject currentGameobject;
    private GameObject currentPlayer;
    private AudioSource mAudio;

    public AudioClip smitting;
    public AudioClip crowd_cheer;
    public AudioClip crowd_laugh;
    public AudioClip crow_clapping;

    [SerializeField] private GameObject circle;
    [SerializeField] private GameObject arrow;
    [SerializeField] private GameObject brokenParticle;
    [SerializeField] private Transform partPos;

    private bool broken = false;
    private bool QTEActive = false;

    private void Start()
    {
        mAudio = GetComponent<AudioSource>();
        mAudio.Stop();
    }

    // Update is called once per frame
    void Update()
    {
        if (QTEActive)
        {
            arrow.transform.Rotate(0, 0, 300 * Time.deltaTime);
            if (currentPlayer.GetComponent<PlayerMovement>().Device.Action2)
            {
                if (arrow.transform.rotation.eulerAngles.y > circle.transform.rotation.eulerAngles.y - 15 &&
                    arrow.transform.rotation.eulerAngles.y < circle.transform.rotation.eulerAngles.y + 15)
                {
                    //succes
                    //currentGameobject.GetComponent<Part>().partIsFixed = true;

                    mAudio.Stop();
                    mAudio.PlayOneShot(crow_clapping);

                    Debug.Log("Fixed");
                    currentGameobject.GetComponent<Parts>().partIsFixed = true;
                    StartCoroutine(currentPlayer.GetComponent<PlayerMovement>().WorkAnimation(false));
                }
                else
                {
                    //fialuer
                    broken = true;

                    //set audio
                    mAudio.Stop();
                    mAudio.PlayOneShot(crowd_laugh);

                    //animations
                    brokenParticle.SetActive(true);
                    currentPlayer.GetComponent<PlayerMovement>().FailAnimation();
                    StartCoroutine(currentPlayer.GetComponent<PlayerMovement>().WorkAnimation(false, 1));

                    //wait until not broken
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
        if (broken || QTEActive) { return; }

        circle.SetActive(true);
        //arrow.SetActive(true);
        StartCoroutine(currentPlayer.GetComponent<PlayerMovement>().WorkAnimation(true));

        if (!mAudio.isPlaying)
        {
            mAudio.clip = smitting;
            mAudio.Play();
        }

        circle.transform.Rotate(0, 0, Random.Range(1, 359));
        arrow.transform.rotation = new Quaternion(0, 0, 0, 0);

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
        if (gb)
        {
            currentGameobject = gb;
            gb.transform.position = partPos.position;
            currentPlayer = player.gameObject;
        }
    }
}
