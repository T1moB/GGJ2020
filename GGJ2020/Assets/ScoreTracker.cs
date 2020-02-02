using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreTracker : MonoBehaviour
{
    public GameObject[] sprites;

    // Start is called before the first frame update
    void Start()
    {
        TurnOffAllSprites();
    }

    public void TurnOffAllSprites()
    {
        for (int i = 0; i < sprites.Length; i++)
            sprites[i].SetActive(false);
    }

    public void IterateScore(int value)
    {
        if (value > sprites.Length) { return; }
        for (int i = 0; i < value; i++)
            sprites[i].SetActive(true);
    }
}
