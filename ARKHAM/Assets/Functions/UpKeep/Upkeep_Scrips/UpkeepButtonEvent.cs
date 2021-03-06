﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpkeepButtonEvent : MonoBehaviour {

    public GameObject upkeepEncounterPanel;

    public static UpkeepButtonEvent instance;

    public GameObject Inventory;
    public GameObject cluepanel;
    public GameObject moneypanel;
    public GameObject tokenpanel1;
    public GameObject tokenpanel2;


    public GameObject BlessPanel;
    public GameObject onMoneyPanel;
    public GameObject succPanel;
    public GameObject failePanel;
    public GameObject CursePanel;

    public Transform InventoryCard;

    public Text cluetext;

    private void Awake()
    {
        instance = this;
    }

    //upkeeppanel 활성화
    public void UpkeepEnCounterStep()
    {
        upkeepEncounterPanel.SetActive(true);
        cluepanel.GetComponent<Text>().text = Character.instance.clue.ToString();
        moneypanel.GetComponent<Text>().text = Character.instance.money.ToString();
        tokenpanel1.GetComponent<Text>().text = Character.instance.gateNum.ToString();
        tokenpanel2.GetComponent<Text>().text = Character.instance.sumMonsterHpNum.ToString();


        //작동 했다 안햇다 해서 주석처리
        //ItemReset();

        Transform parentOj = GameObject.FindGameObjectWithTag("Inventory").transform;
        Vector3 parentvector = parentOj.transform.position;


        ShowInventory(new Vector3(parentvector.x - 70, parentvector.y, parentvector.z));
        
        Character.instance.CharacterFocus = Character.instance.maxFocus;

        

    }

    //upkeeppanel 비활성화 및 집중력0으로 변경
    public void UpkeepStepEnd()
    {
        Transform[] childList = Inventory.GetComponentsInChildren<Transform>();
        if (childList.Length>1)
        {
            for (int i = 1; i < childList.Length; i++)
            {
                if (childList[i] != transform)
                {
                    Destroy(childList[i].gameObject);
                }
            }
        }
        
        
        upkeepEncounterPanel.SetActive(false);
        Character.instance.CharacterFocus = 0;
        Character.instance.currentMoveCount = 0;
        Character.instance.maxMoveCount = Character.instance.CharacterSpeed;
        
    }


    public void ShowInventory(Vector3 showPotition)
    {
        Transform parentOj = GameObject.FindGameObjectWithTag("Inventory").transform;
        //Vector3 parentvector = GameObject.FindGameObjectWithTag("Inventory").transform.position;
        Vector3 parentvector = parentOj.transform.position;
        Transform childOj;

        if (0 < Character.instance.characterInventory.Count)
        {
            for (int i = 0; i < Character.instance.characterInventory.Count; i++)
            {
                childOj = Instantiate(InventoryCard,showPotition+new Vector3(i*50,0,0) /*new Vector3(parentvector.x - 70+(i*24), parentvector.y, parentvector.z)*/, Quaternion.identity, parentOj);
                childOj.GetComponent<Image>().sprite = Character.instance.characterInventory[i].ItemImage;
                childOj.GetComponent<ItemCard>().ItemName = Character.instance.characterInventory[i].ItemName;
                childOj.GetComponentInChildren<Text>().text = Character.instance.characterInventory[i].ItemText;

            }

        }

    }

    public void retainerAndBless()
    {
        if(Character.instance.retainer == true)
        {
            Debug.Log("보유자산 체크");
            GameObject temponMoneyPanel = Instantiate(onMoneyPanel, GameObject.Find("Canvas").transform);
            Destroy(temponMoneyPanel.gameObject, 1.0f);
            Character.instance.money += 1;
            DiceController.instance.SetDice(1,1,1,DiceController.Use.retainer);      
        }
        if(Character.instance.minDiceSucc == 4)
        {
            GameObject tempblessPanel = Instantiate(BlessPanel, GameObject.Find("Canvas").transform);
            Destroy(tempblessPanel.gameObject, 1.0f);
            DiceController.instance.SetDice(1, 1, 1, DiceController.Use.Bless);

        }
        else if(Character.instance.minDiceSucc == 6)
        {
            GameObject tempcursePanel = Instantiate(CursePanel, GameObject.Find("Canvas").transform);
            Destroy(tempcursePanel.gameObject, 1.0f);
            DiceController.instance.SetDice(1, 1, 1, DiceController.Use.Bless);
        }
    }
    public void resultretainer(int dice)
    {
        if (1 == dice)
        {
            Character.instance.retainer = false;
        }
    }
    public void resultBless(int dice)
    {
        if (1 == dice)
        {
            GameObject tempfailePanel = Instantiate(failePanel, GameObject.Find("Canvas").transform);
            Destroy(tempfailePanel.gameObject, 1.0f);
            Character.instance.minDiceSucc = 5;
        }
        else
        {
            GameObject tempsuccPanel = Instantiate(succPanel, GameObject.Find("Canvas").transform);
            Destroy(tempsuccPanel.gameObject, 1.0f);

        }
            
    }
    public void ItemReset()
    {
        Character.instance.ItemReset();
    }

    


}
