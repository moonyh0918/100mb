using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GateTrophyScore : ScoreSetting
{

    public Text gateTrophyScore;

    void Start()
    {
        gateTrophyScore = GetComponent<Text>();
        gateTrophyScore.text = Scoreontroller.instanse.GateTrophy.ToString();

    }
}
