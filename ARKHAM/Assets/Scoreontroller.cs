using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scoreontroller : MonoBehaviour {

	public int GateTrophy { get { return GateTrophy; } set { GateTrophy = value; } }  //봉인 횟수 *100
    public int MonsterHP { get { return MonsterHP; } set { MonsterHP = value; } }   //잡은 총합 몬스터 체력수 *25
    public int BossHp { get { return BossHp; } set { BossHp = value; } }  //깍은 보스 hp *1000

    public int ItemAmount { get { return ItemAmount; } set { ItemAmount = value; } }  //아이템 갯수*10

    public int Clue { get { return Clue; } set { Clue = value; } } //단서갯수*5

    public int Basic { get { return Basic; } set { Basic = value; } } //기본점수 (파멸트랙-공포트랙)*1000

    public GameObject mainCanvas;
    public GameObject finalbattleCanvas;
    public GameObject battleCanvas;


    public GameObject scoreCanvas;

    public static Scoreontroller instanse = null;

    private void Start()
    {
        instanse = this;
    }
    public void EndGame()
    {
        mainCanvas.SetActive(false);
        finalbattleCanvas.SetActive(false);
        battleCanvas.SetActive(false);

        scoreCanvas.SetActive(true);

    }

    public void 





}
