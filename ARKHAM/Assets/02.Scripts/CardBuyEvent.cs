﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class CardBuyEvent : MonoBehaviour {

    public GameObject EventPanel;
    public GameObject ItemPanel1;
    public GameObject ItemPanel2;
    public GameObject ItemPanel3;
    public List<ItemCard> Drowcard;
    public GameObject Nomoney;
    private int nowItemDeck;
    

    public static CardBuyEvent instance = null;
    


    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        Drowcard = ItemDictionary.instance.CommonItemDeck;
    }


    //카드 구입 이벤트시 아이템 종류 드로우
    public void Drowcardsetting(int num)
    {
        nowItemDeck = num;
        if (num==1)
        {
            Drowcard = ItemDictionary.instance.CommonItemDeck;
            ItemPanel1.GetComponent<Image>().sprite = Drowcard[0].ItemImage;
            ItemPanel2.GetComponent<Image>().sprite = Drowcard[1].ItemImage;
            ItemPanel3.GetComponent<Image>().sprite = Drowcard[2].ItemImage;
        }
        if(num==2)
        {
            Drowcard = ItemDictionary.instance.UniquItemDeck;
            ItemPanel1.GetComponent<Image>().sprite = Drowcard[0].ItemImage;
            ItemPanel2.GetComponent<Image>().sprite = Drowcard[1].ItemImage;
            ItemPanel3.GetComponent<Image>().sprite = Drowcard[2].ItemImage;
        }

        if(num==3)//마법
        {

        }

        if(num==11)
        {
            Drowcard[0] = ItemDictionary.instance.CommonItemDeck[0];
            Drowcard[1] = ItemDictionary.instance.CommonItemDeck[0];
            Drowcard[2] = ItemDictionary.instance.CommonItemDeck[0];
            ItemPanel1.GetComponent<Image>().sprite = Drowcard[0].ItemImage;
            ItemPanel2.GetComponent<Image>().sprite = Drowcard[0].ItemImage;
            ItemPanel3.GetComponent<Image>().sprite = Drowcard[0].ItemImage;
        }

    }

    //아이템 선택시 구매가능 여부 판별 및 구입
    public void SelectItem(int num)
    {
        
        if(Character.instance.money>Drowcard[num].price)
        {
            Character.instance.money -= Drowcard[num].price;
            Character.instance.CharacterInventory.Add(Drowcard[num]);
            
            EventPanel.SetActive(false);
            LocalEventController.instance.NextButtonSet();
        }
        else
        { 
            GameObject NomoneyPanel= Instantiate(Nomoney, GameObject.Find("Canvas").transform);
            if (GameObject.Find("NomoneyPanel(clone)") ==true)
            {
                Destroy(NomoneyPanel);
            }
            Destroy(NomoneyPanel.gameObject,1.5f);

        }

    }
    //카드 드로우 이벤트
    public void CardDrow(int num)
    {
        ItemDictionary.instance.DrowOneCard(num);

    }


    public void NotItemBtn()
    {
        if (nowItemDeck == 1)
        {
            Drowcard = ItemDictionary.instance.CommonItemDeck;
            Notselect(Drowcard);


        }
        if (nowItemDeck == 2)
        {
            Drowcard = ItemDictionary.instance.UniquItemDeck;
            Notselect(Drowcard);

        }
    }


    public void Notselect(List<ItemCard> item)
    {
        for(int i= 0; i<3; i++)
        {
            item.Add(ItemDictionary.instance.CommonItemDeck[0]);
            item.RemoveAt(0);
        }

        EventPanel.SetActive(false);
        LocalEventController.instance.NextButtonSet();

    }


}
