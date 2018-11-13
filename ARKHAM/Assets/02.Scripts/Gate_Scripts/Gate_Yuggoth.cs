﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gate_Yuggoth : Gate {

    private void Awake()
    {
        OpenLocal = GameObject.Find("Yuggoth").GetComponent<Local>();
        GateSimbol = Monster.Simbol.Circle;
        Modifier = -2;
        gateImage = Resources.Load<Sprite>("gateImages/Yuggoth");
    }
}
