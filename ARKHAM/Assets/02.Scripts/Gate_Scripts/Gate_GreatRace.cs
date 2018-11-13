﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gate_GreatRace : Gate {

    private void Awake()
    {
        OpenLocal = GameObject.Find("GreatRace").GetComponent<Local>();
        GateSimbol = Monster.Simbol.Triangle;
        Modifier = 0;
        gateImage = Resources.Load<Sprite>("gateImages/TheCityOfTheGreatRace");
    }
}
