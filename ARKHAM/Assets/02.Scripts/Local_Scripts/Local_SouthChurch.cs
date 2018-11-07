using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Local_SouthChurch : Local {

    void Awake()
    {
        local_Id = 82;   //99=아컴지역 004=인접한거리(1번,3번,6번) 02=장소번호
        allowLocal_Id = new int[1];
        allowLocal_Id[0] = 80;

        position = transform.position;
        localFunction = "획득한 괴물을 체력의 합이 5 이상 되게 내거나, 획득한 차원문 1개를 내면 원하는 조사자를 축복받은 상태로 만들 수 있습니다.";
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
