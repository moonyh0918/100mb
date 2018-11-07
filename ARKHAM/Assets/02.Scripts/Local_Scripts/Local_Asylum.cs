using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Local_Asylum : Local {

    void Awake () {
        local_Id = 12;   //99=아컴지역 004=인접한거리(1번,3번,6번) 02=장소번호
        allowLocal_Id = new int[1];
        allowLocal_Id[0] = 10;

        position = transform.position;
        localFunction = " 정신 치료를 통해 정신력을 회복할 수 있습니다. 무료로 정신력 1을 회복하거나, $2를 내고 정신력을 최대로 회복할 수 있습니다.";
    }

    protected override void EventOne()
    {
        Debug.Log(gameObject.name + "EventOne");
    }

    protected override void EventTwo()
    {
        Debug.Log(gameObject.name + "EventTwo");
    }

    protected override void EventThree()
    {
        Debug.Log(gameObject.name + "EventThree");
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
