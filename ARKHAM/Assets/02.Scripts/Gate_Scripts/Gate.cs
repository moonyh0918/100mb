﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gate : MonoBehaviour {

    public Monster.Simbol GateSimbol;
    public Local OpenLocal;
    public int Modifier;
    public Sprite GateImage;

    public Monster[] BackMonster;

    public ShowGateInfo showGateInfo;

    private void Start()
    {
        Transform gateInfoPanel = GameObject.Find("GateInfoPanel").transform.GetChild(0);
        showGateInfo = gateInfoPanel.GetComponent<ShowGateInfo>();
    }

    public void CloseGate()
    {
        
        BackMonster = FindObjectsOfType<Monster>();
        
        Debug.Log("게이트 닫힐시 해당 심볼 몬스터 삭제");
        for (int i = 0;i<BackMonster.Length ;i++)
        {
            if (BackMonster[i].simbol == GateSimbol)
            {
                GameObject DestroyMonster = GameObject.Find(BackMonster[i].name);
                Debug.Log(DestroyMonster.name);
                Destroy(DestroyMonster.gameObject);

                //몬스터 삭제
            }
        }
        Character.instance.specialLocalCheck = false;
        OpenLocal.allowLocal_Id[0] = 0;
        Destroy(this.gameObject);


    }


    public void ClosesGateCheck(int ChecktNumGate)
    {
        switch (ChecktNumGate)
        {
            //지식체크
            case 0:
                DiceController.instance.SealGateSetDice(this, Character.instance.characterLore, Character.instance.MinDiceSucc, 6);
                break;
            //투지체크
            case 1:
                DiceController.instance.SealGateSetDice(this, Character.instance.characterFight, Character.instance.MinDiceSucc, 6);
                break;
        }

        

    }

    public virtual void SealGateResulte(int successCount)
    {
        Debug.Log("닫기 시도 결과");
        Debug.Log(successCount);
        if (successCount > 0)
        {
            if(Character.instance.clue > 4)
            {            
                Debug.Log("봉인 여부 선택");
                GateController.instance.SealPanel();      
            }
            else
            {
                GateController.instance.SetMainButtonOn();
            }
        }
        else
        {
            Debug.Log("실패");
        }
    }

    public void SealbuttonCheck(int n)
    {
        Local seallocal = this.GetComponentInParent<Local>();
        if (n==0)
        {

            Character.instance.clue -= 5;
            Character.instance.GateNum += 1;
            seallocal.SealMark = true;
            
        }
        seallocal.gateOpenCheck = false;
        CloseGate();

    }


    //차원문과 멀어지면 탐사완료 마크 사라짐
    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            Character.instance.specialLocalCheck = false;
        }
    }

    private void OnMouseEnter()
    {
        if (GameManager.instance.CheckGameState(GameManager.GameState.Move))
        {
            showGateInfo.gameObject.SetActive(true);
            showGateInfo.UpdateInfoUI(this);
        }

    }

    private void OnMouseExit()
    {
        showGateInfo.gameObject.SetActive(false);
    }
}
