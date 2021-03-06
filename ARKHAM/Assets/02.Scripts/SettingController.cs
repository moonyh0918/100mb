﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingController : MonoBehaviour {

	public static SettingController instance = null;
    public GameObject upkeeppanel;

    private void Awake()
    {
        instance = this;
    }

    public void SheetSetting()
    {
        upkeeppanel.SetActive(true);
        GameObject.FindGameObjectWithTag("Boss").GetComponent<Boss>().Worshippers();    //필드에 영향을 주는 보스능력


        Vector3 startPosition;

        Local startloal = Local.GetLocalObjById(Character.instance.currentLocal_Id);
        startPosition = new Vector3(startloal.transform.position.x, 1, startloal.transform.position.z-3);    
        GameObject.Find("character").GetComponent<Transform>().position = startPosition;

        GameObject.Find("CharacterSheet").GetComponent<Image>().sprite = Character.instance.SheetImage;
        UpkeepButtonEvent.instance.UpkeepEnCounterStep();
        Character.instance.focus = 100;
        Character.instance.currentMoveCount = Character.instance.maxMoveCount;

    }

    public void EndSetting()
    {

        UpkeepButtonEvent.instance.UpkeepStepEnd();

        MythosController.instance.FirstMythos();
    }


}
