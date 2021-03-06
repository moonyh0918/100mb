﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gate_PlateauOfLeng : Gate {

    private void Awake()
    {
        OpenLocal = GameObject.Find("PlateauOfLeng").GetComponent<Local>();
        GateSimbol = Monster.Simbol.Diamond;
        Modifier = -1;
        gateImage = Resources.Load<Sprite>("gateImages/PlateauOfLeng");
    }
}
