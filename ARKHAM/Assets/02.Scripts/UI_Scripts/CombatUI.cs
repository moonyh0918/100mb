using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CombatUI : MonoBehaviour {

    public GameObject combatPanel;
    public GameObject combatAnimPanel;

    public GameObject bottomMenuPanel;
    public GameObject mainButtonPanel;

    public GameObject mainCamera;
    public GameObject combatCamera;

    public Image charStamina;
    public Image charSanity;
    public Text charStat;

    public Image monsterImage;
    public Image monsterAnimImage;
    public Image monStamina;
    public Text monStat;
    public Text monDamage;
    public Text monAttibute;
    public Text monAbility;

    public void SetCombatUI(Monster mon)
    {
        combatAnimPanel.GetComponent<Animator>().SetBool("FinishCombat", false);

        monsterImage.sprite = Resources.Load<Sprite>("MonsterImages/" + mon.id);
        monsterAnimImage.sprite = monsterImage.sprite;
        monStamina.fillAmount = mon.hp / 10.0f;
        monStat.text = "공포수준 : " + mon.fearLevel + " 전투수준 : " + mon.combatLevel +" 회피 : " + mon.evasionLevel;
        monDamage.text = "체력피해 : " + mon.staminaDamage +" 정신피해 : " +  mon.sanityDamage;

        string attText = "속성 : ";
        for (int i = 0; i < mon.sAttribute.Count; i++)
        {
            string s = mon.sAttribute[i].ToString() + " ";
            attText = attText.Insert(attText.Length, s);
        }

        monAttibute.text = attText;

        /*
        if (mon.id == )
            abillityText.text = ; 
        else if (mon.id == )
            abillityText.text = ; 
        else 
             abilityText.text = "특정 능력 없음":
         */


        charStamina.fillAmount = Character.instance.CharacterStamina / 10.0f;
        charSanity.fillAmount = Character.instance.CharacterSanity / 10.0f;
        charStat.text = "투지 : " + Character.instance.CharacterFight + " 의지 : " + Character.instance.CharacterWill + " 은둔 : " + Character.instance.CharacterSneak;

        mainButtonPanel.SetActive(false);
        combatAnimPanel.SetActive(false);

        combatPanel.SetActive(true);

        Transform parentOj = GameObject.FindGameObjectWithTag("Inventory").transform;
        Vector3 parentvector = parentOj.transform.position;

        UpkeepButtonEvent.instance.ShowInventory(new Vector3(parentvector.x - 70, parentvector.y, parentvector.z));

        combatCamera.SetActive(true);
        mainCamera.SetActive(false);
    }


    public void CombatUIActive(bool value)
    {
        combatPanel.SetActive(value);
    }

    public void CombatAnimUIActive(bool value)
    {
        combatAnimPanel.SetActive(value);
    }

    public void PlayerWinAnim()
    {
        combatPanel.SetActive(false);
        combatAnimPanel.SetActive(true);

        combatAnimPanel.GetComponent<Animator>().SetBool("PlayerWin", true);
    }

    public void PlayerLoseAnim()
    {
        combatPanel.SetActive(false);
        combatAnimPanel.SetActive(true);

        combatAnimPanel.GetComponent<Animator>().SetBool("MonsterWin", true);
    }

    public void InitCombatAnim()
    {
        combatPanel.SetActive(false);
        combatAnimPanel.SetActive(false);

        combatCamera.SetActive(false);
        mainCamera.SetActive(true);
        MaincameraController.instance.SetPosition();

        combatAnimPanel.GetComponent<Animator>().SetBool("PlayerWin", false);
        combatAnimPanel.GetComponent<Animator>().SetBool("MonsterWin", false);
        combatAnimPanel.GetComponent<Animator>().SetBool("FinishCombat", true);

        mainButtonPanel.SetActive(true);
        bottomMenuPanel.SetActive(true);
    }

    // 체력, 정신력 잃을때 파괴 되는 애니메이션을 넣을려면 하나씩 생성, 삭제시 애니메이션 재생
    public void UdpatePlayerUI()
    {
        charStamina.fillAmount = Character.instance.CharacterStamina / 10.0f;
        charSanity.fillAmount = Character.instance.CharacterSanity / 10.0f;
    }
}
