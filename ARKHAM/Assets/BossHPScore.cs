using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHPScore : ScoreSetting
{


    public Text bosshpscorea;

    void Start () {
        bosshpscorea = GetComponent<Text>();
        bosshpscorea.text = Scoreontroller.instanse.BossHp.ToString();

    }
	

}
