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
    //public Text atrributeText;
    //public Text abillityText;

    public void Awake()
    {
        nameText = transform.Find("Name").GetComponentInChildren<Text>();
        combatLevelText = transform.Find("CombatLevel").GetComponentInChildren<Text>();
        fearLevelText = transform.Find("Fear").GetComponentInChildren<Text>();
        evasionText = transform.Find("Evasion").GetComponentInChildren<Text>();

        hpText = transform.Find("HP").GetComponentInChildren<Text>();
        staminaDamageText = transform.Find("StaminaDamage").GetComponentInChildren<Text>();
        sanityDamageText = transform.Find("sSanityDamage").GetComponentInChildren<Text>();

        //atrributeText = transform.Find("Atrribute").GetComponentInChildren<Text>();
        //abillityText = transform.Find("Ability").GetComponentInChildren<Text>();
    }

    private void Start()
    {
        gameO0bject.SetActive(false);
    }

    public void UpdateInfoUI(Monster mon)
    {
        nameText.text = mon.name;

        combatLevelText.text = mon.combatLevel.ToString();
        fearLevelText.text = mon.fearLevel.ToString();
        evasionText.text = mon.evasionLevel.ToString();

        hpText.text = mon.name;
        staminaDamageText.text = mon.staminaDamage.ToString();
        sanityDamageText.text = mon.sanityDamage.ToString();

        /*
        for(int i =0; i < mon.sAttribute.Count; i++)
        {
                //문자열 연결 함수 호출하기
        }

        atrributeText.text = mon.sAttribute;
        
        if (mon.id == )
            abillityText.text = ; 
        else if (mon.id == )
            abillityText.text = ; 
        else 
             abilityText.text = NULL:
         */
    }

}
