using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ResultScore : MonoBehaviour
{
    public GameObject score_object = null;
    int resultScore;

    void Start()
    {
        resultScore = PlayerController.getScore();
        if (score_object != null)
        {
            Text score_text = score_object.GetComponent<Text>();
            score_text.text =  resultScore + " ‘Ì";
            Debug.Log(resultScore);
        }
    }

}
