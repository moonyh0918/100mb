using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Scoreontroller : MonoBehaviour {

    private int gateTrophy;
    public int GateTrophy { get { return gateTrophy * 100; } set { gateTrophy = value; } }  //봉인 횟수 *100
    private int monsterHP;
    public int MonsterHP { get { return monsterHP * 25; } set { monsterHP = value; } }   //잡은 총합 몬스터 체력수 *25

    private int bossHp;
    public int BossHp { get { return bossHp * 1000; } set { bossHp = value; } }  //깍은 보스 hp *1000

    private int itemAmount;
    public int ItemAmount { get { return itemAmount * 10; } set { itemAmount = value; } }  //아이템 갯수*10

    private int clue;
    public int Clue { get { return clue * 5; } set { clue = value; } } //단서갯수*5

    private int basic;
    public int Basic { get { return basic*1000; } set { basic = value; } } //기본점수 (파멸트랙-공포트랙)*1000

    public int totalScaore;

    public GameObject mainCanvas;
    public GameObject finalbattleCanvas;
    public GameObject battleCanvas;


    public Text num1;
    public Text num2;
    public Text num3;
    public Text num4;
    public Text num5;
    public Text num6;



    public GameObject scoreCanvas;

    public static Scoreontroller instanse = null;

    private void Awake()
    {
        instanse = this;
    }

    public int BasicScore()
    {
        return FinalBattle.instance.bossDoomtrack - TerrorLevel.instance.terrorlevel;
    }

    public int Score()
    {
        return GateTrophy  + MonsterHP  + BossHp + Clue + Basic;
    }



    public void EndGame()
    {
        ScoreSetting();
        mainCanvas.SetActive(false);
        finalbattleCanvas.SetActive(false);
        battleCanvas.SetActive(false);

        scoreCanvas.SetActive(true);

    }



    public void ReStartBtn()
    {
        SceneManager.LoadScene("StartMain", LoadSceneMode.Single);
    }



    public void ScoreSetting()
    {
        GateTrophy = Character.instance.gateNum;
        MonsterHP = Character.instance.sumMonsterHpNum;
        BossHp = FinalBattle.instance.doomTrack-Boss.instance.BossDoomTrack;
        ItemAmount = Character.instance.characterInventory.Count;
        Clue = Character.instance.clue;
        Basic = FinalBattle.instance.doomTrack-TerrorLevel.instance.terrorlevel;


        num1.text = Basic.ToString();
        num2.text =  BossHp.ToString();
        num3.text = GateTrophy.ToString();
        num4.text =  MonsterHP.ToString();
        num5.text = Clue.ToString();
        num6.text = Score().ToString();

    }
    

}
