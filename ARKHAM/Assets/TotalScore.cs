using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TotalScore : ScoreSetting
{

    public Text totalScore;

    void Start()
    {
        totalScore = GetComponent<Text>();
        totalScore.text = Scoreontroller.instanse.Score().ToString();

    }
}
