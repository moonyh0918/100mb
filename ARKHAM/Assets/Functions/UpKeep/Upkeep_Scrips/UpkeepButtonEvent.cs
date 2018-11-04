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
        tokenpanel1.GetComponent<Text>().text = Character.instance.GateNum.ToString();
        tokenpanel2.GetComponent<Text>().text = Character.instance.SumMonsterHP.ToString();

        ItemReset();
        ShowInventory();
        
        Character.instance.characterFocus = Character.instance.MaxFocus;

        

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
        Character.instance.characterFocus = 0;
        Character.instance.currentMoveCount = 0;
        Character.instance.maxMoveCount = Character.instance.characterSpeed;
        
    }


    public void ShowInventory()
    {
        Transform parentOj = GameObject.FindGameObjectWithTag("Inventory").transform;
        //Vector3 parentvector = GameObject.FindGameObjectWithTag("Inventory").transform.position;
        Vector3 parentvector = parentOj.transform.position;
        Transform childOj;

        if (0 < Character.instance.CharacterInventory.Count)
        {
            for (int i = 0; i < Character.instance.CharacterInventory.Count; i++)
            {
                childOj = Instantiate(InventoryCard, new Vector3(parentvector.x - 70+(i*24), parentvector.y, parentvector.z), Quaternion.identity, parentOj);
                childOj.GetComponent<Image>().sprite = Character.instance.CharacterInventory[i].ItemImage;
                childOj.GetComponent<ItemCard>().ItemName = Character.instance.CharacterInventory[i].ItemName;

            }

        }

    }

    public void RetainerAndBless()
    {
        if(Character.instance.Retainer == true)
        {
            Debug.Log("보유자산 체크");
            GameObject temponMoneyPanel = Instantiate(onMoneyPanel, GameObject.Find("Canvas").transform);
            Destroy(temponMoneyPanel.gameObject, 1.0f);
            Character.instance.money += 1;
            DiceController.instance.SetDice(1,1,1,DiceController.Use.Retainer);      
        }
        if(Character.instance.MinDiceSucc==4)
        {
            GameObject tempblessPanel = Instantiate(BlessPanel, GameObject.Find("Canvas").transform);
            Destroy(tempblessPanel.gameObject, 1.0f);
            Debug.Log("축복 체크");
            DiceController.instance.SetDice(1, 1, 1, DiceController.Use.Bless);

        }
        else if(Character.instance.MinDiceSucc==6)
        {
            GameObject tempcursePanel = Instantiate(CursePanel, GameObject.Find("Canvas").transform);
            Destroy(tempcursePanel.gameObject, 1.0f);
            DiceController.instance.SetDice(1, 1, 1, DiceController.Use.Bless);
        }
    }
    public void resultRetainer(int dice)
    {
        if (1 == dice)
        {
            Character.instance.Retainer = false;
        }
    }
    public void resultBless(int dice)
    {
        if (1 == dice)
        {
            GameObject tempfailePanel = Instantiate(failePanel, GameObject.Find("Canvas").transform);
            Destroy(tempfailePanel.gameObject, 1.0f);
            Character.instance.MinDiceSucc = 5;
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
