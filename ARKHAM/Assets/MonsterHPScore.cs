using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MonsterHPScore : ScoreSetting
{

    public Text monsterHPScore;

    void Start()
    {
        monsterHPScore = GetComponent<Text>();
        monsterHPScore.text = Scoreontroller.instanse.BossHp.ToString();

    }
}
