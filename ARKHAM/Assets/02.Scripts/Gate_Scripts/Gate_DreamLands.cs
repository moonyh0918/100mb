﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gate_DreamLands : Gate {

    private void Awake()
    {
        OpenLocal = GameObject.Find("DreamLands").GetComponent<Local>();
        GateSimbol = Monster.Simbol.BackSlash;
        Modifier = 1;
        gateImage = Resources.Load<Sprite>("gateImages/TheDreamlands");
    }
}

