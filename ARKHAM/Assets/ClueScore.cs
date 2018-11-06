using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClueScore : ScoreSetting
{


    public Text clueScore;

    void Start()
    {
        clueScore = GetComponent<Text>();
        clueScore.text = Scoreontroller.instanse.Clue.ToString();

    }
}
