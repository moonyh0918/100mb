﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Street_NorthSide : Local {

    void Start () {

        local_Id = 9906899;   //99=아컴지역 136=인접한거리(1번,3번,6번) 94=장소(10의자리9일경우거리 1의자리는 거리 번호)
        allowLocal_Id = new int[5];
        allowLocal_Id[0] = 9900901;
        allowLocal_Id[1] = 9900906;
        allowLocal_Id[2] = 9900905;
        allowLocal_Id[3] = 99458996;
        allowLocal_Id[4] = 9967998;
    }


    protected override void EventOne()
    {

    }

    protected override void EventTwo()
    {

    }

    protected override void EventThree()
    {

    }

    protected override void EventFour()
    {

    }

    protected override void EventFive()
    {

    }

    protected override void EventSiz()
    {

    }

    protected override void EventSeven()
    {

    }
}