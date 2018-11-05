using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowMonsterInfo : MonoBehaviour {

    public Text nameText;
    public Text combatLevelText;
    public Text fearLevelText;
    public Text evasionText;
    public Text hpText;
    public Text staminaDamageText;
    public Text sanityDamageText;
    public Text atrributeText;
    //public Text abillityText;

    public void Awake()
    {
        nameText = transform.Find("Name").GetComponentInChildren<Text>();
        combatLevelText = transform.Find("CombatLevel").GetComponentInChildren<Text>();
        fearLevelText = transform.Find("FearLevel").GetComponentInChildren<Text>();
        evasionText = transform.Find("EvasionLevel").GetComponentInChildren<Text>();

        hpText = transform.Find("HP").GetComponentInChildren<Text>();
        staminaDamageText = transform.Find("StaminaDamage").GetComponentInChildren<Text>();
        sanityDamageText = transform.Find("SanityDamage").GetComponentInChildren<Text>();

        atrributeText = transform.Find("Attribute").GetComponentInChildren<Text>();
        //abillityText = transform.Find("Ability").GetComponentInChildren<Text>();
    }
    
    public void UpdateInfoUI(Monster mon)
    {
        nameText.text = mon.name;

        combatLevelText.text = "전투수준 :" + mon.combatLevel;
        fearLevelText.text = "공포수준 :" + mon.fearLevel;
        evasionText.text = "회피수준 : " + mon.evasionLevel;

        hpText.text = "HP : " + mon.name;
        staminaDamageText.text = "체력피해 :" + mon.staminaDamage;
        sanityDamageText.text = "정신피해 : " + mon.sanityDamage;

        string attText = "속성 : ";
        for(int i =0; i < mon.sAttribute.Count; i++)
        {
            string s = mon.sAttribute[i].ToString() + " ";
            attText = attText.Insert(attText.Length, s);
        }

        atrributeText.text = attText;

        /*
        if (mon.id == )
            abillityText.text = ; 
        else if (mon.id == )
            abillityText.text = ; 
        else 
             abilityText.text = "특정 능력 없음":
         */
    }

}
