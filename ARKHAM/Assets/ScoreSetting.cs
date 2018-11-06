using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreSetting : MonoBehaviour {

    public virtual void SetScoreText(string Num)
    {
        Text Score;
        Score = GetComponent<Text>();
        Score.text = Num;
    }
}
