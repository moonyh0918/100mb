﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Gate_Abyss : Gate {

    private void Awake()
    {
        OpenLocal = GameObject.Find("Abyss").GetComponent<Local>();
        GateSimbol = Monster.Simbol.Hexagon;
        Modifier = -2;
        gateImage = Resources.Load<Sprite>("gateImages/TheAbyss");
    }

}
