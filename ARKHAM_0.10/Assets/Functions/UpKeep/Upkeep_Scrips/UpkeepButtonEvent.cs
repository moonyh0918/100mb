﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpkeepButtonEvent : MonoBehaviour {

    public GameObject upkeepEncounterPanel;

    public static UpkeepButtonEvent instance;

    private void Awake()
    {
        instance = this;
    }

    //upkeeppanel 활성화
    public void UpkeepEnCounterStep()
    {
        upkeepEncounterPanel.SetActive(true);

    }

    //upkeeppanel 비활성화 및 집중력0으로 변경
    public void UpkeepStepEnd()
    {
        upkeepEncounterPanel.SetActive(false);
        Character.instance.characterFocus = 0;
    }




}