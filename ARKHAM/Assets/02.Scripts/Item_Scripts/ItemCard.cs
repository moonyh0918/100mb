﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemCard : MonoBehaviour {

    public Image alphcahge;
    public enum ItemKind { CommonItem, UniquItem, Spell };
    public ItemKind itemkind;
    public string ItemName;
    public enum ItemType { Physical, Magical, Book, Nomal};
    public ItemType itemtype;
    public int price;
    public int hand;
    public Sprite ItemImage;
    public string ItemText;
    public bool useCheck = false;

    public static ItemCard instance = null;

    public ItemCard(string _ItemName, int _price, int _hand, ItemKind _ItemKind, ItemType _itemType, Sprite _ItemImage, string _ItemText)
    {
        itemkind = _ItemKind;
        this.ItemName = _ItemName;
        itemtype = _itemType;
        this.price = _price;
        this.hand = _hand;
        this.ItemImage = _ItemImage;
        this.ItemText = _ItemText;

        
    }
    private void Awake()
    {
        instance = this;
    }

    public virtual void ItemFuntion()
    {
        
       

    }

    //알파값 조작 코드 안되서 물어보기용
   /* public void Alphchange()
    {

        Color imageAlph;
        imageAlph = alphcahge.color;
        if (useCheck)
        {
            alphcahge.color = new Color(imageAlph.r, imageAlph.g, imageAlph.b, 1.0f);
        }
        else
        {
            alphcahge.color = new Color(imageAlph.r, imageAlph.g, imageAlph.b, 0.0f);
        }
    }*/

}
