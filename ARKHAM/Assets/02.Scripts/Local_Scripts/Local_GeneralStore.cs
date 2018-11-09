using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Local_GeneralStore : Local {

    void Awake () {
        local_Id = 43;   //99=아컴지역 004=인접한거리(1번,3번,6번) 02=장소번호
        allowLocal_Id = new int[1];
        allowLocal_Id[0] = 40;

        position = transform.position;

        localFunction = "일반 아이템 카드 3장을 뽑은 다음 그중 1장을 액면가를 지불하고 살 수 있습니다. 나머지 2장은 카드더미 밑에 넣습니다.";
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
