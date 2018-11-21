﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterMoveController : MonoBehaviour {

    public Monster monster;
    public Local currentLocal;
    public Local goalLocal;
    public Transform skyLocal;
    public Transform outskirtsLocal;

    public enum Color { White, Black };
    Color color;


    public IEnumerator MoveEachType(Color _color)
    {
        color = _color;

        monster = gameObject.GetComponent<Monster>();
        Monster.Type type = monster.type;

        switch (type)
        {
            case Monster.Type.Normal:
                yield return StartCoroutine(NormalMove());
                break;
            case Monster.Type.Fast:
                yield return StartCoroutine(FastMove());
                break;
            case Monster.Type.Fly:
                FlyMove();
                break;
            case Monster.Type.Fixed:
                FixeMove();
                break;
            case Monster.Type.SMovement:
                SMovement();
                break;
        }
    }


    IEnumerator NormalMove()
    {
        currentLocal = monster.GetComponentInParent<Local>();

        // 지역에서 거리로      ////////////////////////////////  Local Id값들 변경 후 조건문 바꾸기  
        if (currentLocal.allowLocal_Id.Length == 1)
            goalLocal = Local.GetLocalObjById(currentLocal.allowLocal_Id[0]);
        // 거리에서 거리or 지역으로 
        else
        {
            if (color == Color.White) 
                goalLocal = Local.GetLocalObjById(currentLocal.whitePath_id);
            else
                goalLocal = Local.GetLocalObjById(currentLocal.blackPath_id);
        }
        IEnumerator moveCoroutine = MovePosition(goalLocal.position);
        yield return StartCoroutine(moveCoroutine);
      
        //Debug.Log(monster + "(" + color + ")" + " : " + currentLocal + "to" + goalLocal);
  
        yield return null;
    }


    IEnumerator FastMove()
    {
        yield return StartCoroutine(NormalMove());

        // 조사자와 같은 지역에 있을 경우 이동 중지 
        if (!monster.meetPlayer)
            yield return StartCoroutine(NormalMove());

        yield return null;
    }


    private void FlyMove()
    {
        currentLocal = monster.GetComponentInParent<Local>();

        // 모든 조사자 들을 불러와 거리에있는 조사자만 분리해서 저장    
        // Find드는 배열에만 저장되므로 배열사용, 최대 4인게임이므로 크기는 4
        Character[] characters = new Character[4];
        List<Character> streetCharacter = new List<Character>();
        characters = GameObject.FindObjectsOfType<Character>();

        for (int i = 0; i < characters.Length; i++)
        {
            if (characters[i] != null)
            {
                Debug.Log("캐릭터 리스트 : " + characters[i]);

                // 캐릭터가 위치한 지역의 이동가능경로 수를 보고 지역인지 장소인지 판별
                Local local = Local.GetLocalObjById(characters[i].currentLocal_Id);

                //////////////////////////     이부분도 Localid 변경되면 바꾸기
                if (local.allowLocal_Id.Length > 1)
                {
                    Debug.Log("캐릭터 리스트 : " + characters[i] + "는 거리에 있습니다");
                    streetCharacter.Add(characters[i]);
                }
            }
        }

        // 몬스터가 하늘 인 경우 
        if (currentLocal.local_Id == 121)
        {
            // 거리에 조사자가 없으면 하늘에 머문다
            if (streetCharacter.Count == 0)
            {
                Debug.Log(monster + " 거리에 조사자가 없어 하늘에 머뭅니다.");
                return;
            }

            // 거리에 조사자가 있다면 운둔이 가장 낮은놈을 판별 후 그곳으로 이동 
            Character lowsneakChar = streetCharacter[0];

            for (int i = 0; i < streetCharacter.Count; i++)
            {
                if (streetCharacter[i].sneak < lowsneakChar.sneak)
                    lowsneakChar = streetCharacter[i];
            }

            goalLocal = Local.GetLocalObjById(lowsneakChar.currentLocal_Id);

            Debug.Log(monster + " 하늘에서 거리에있는 " + lowsneakChar + "에게로 이동합니다");

            // 하늘에서 이동이므로 MoveToward가 아닌 바로이동 
            monster.transform.position = new Vector3(goalLocal.position.x, 1.2f, goalLocal.position.z - 3.0f);
            monster.transform.SetParent(goalLocal.transform);

            SkyUI.instance.UpdateSkyUI();
        }
        // 몬스터가 지역, 거리에 있을 경우 
        else
        {
            //조사자가 거리에 없다면 하늘로  
            if (streetCharacter.Count == 0)
            {
                Debug.Log(monster + "거리에 조사자가 없어 하늘로 올라갑니다.");

                skyLocal = GameObject.FindObjectOfType<Local_Sky>().transform;

                monster.transform.SetParent(skyLocal);

                monster.transform.localPosition = Vector3.zero;

                SkyUI.instance.UpdateSkyUI();

                return;
            }

            //지역, 거리에있다면 자신으로부터 인접한 거리의 조사자에게 이동,
            int distance = 0;
            int farthest = 0;
            Character farthestChar = null;

            for (int i = 0; i < streetCharacter.Count; i++)
            {
                if (streetCharacter[i] != null)
                {
                    distance = DistanceBetween.instance.GetDistance(currentLocal.local_Id, streetCharacter[i].currentLocal_Id);

                    if (distance > farthest)
                    {
                        farthest = distance;
                        farthestChar = streetCharacter[i];
                    }
                }
            }
            Debug.Log(monster + " 가장 멀리 떨어져있는 캐릭터 :" + farthestChar.name + "에게 이동합니다");

            goalLocal = Local.GetLocalObjById(farthestChar.currentLocal_Id);
            StartCoroutine(MovePosition(goalLocal.position));
        }
    }


    private void FixeMove()
    {
        Debug.Log(monster + "Move FixeMove");
        // 이동 없음 아무것도 하지 않는다.
    }


    private void SMovement()
    {
        Debug.Log(monster + "Move SMovement");

        // 몬스터의 이름이 HoundofTindalos else Chthonian,  if else문 만들기
        // HoundofTindalos - 이동할 때, 조사자가 있는 아컴의 장소 중 가장 가까운 곳으로 이동한다.(아컴 정신병원과 세인트 메리 병원은 제외)
        // Chthonian - 이동해야 할 때, 이동하지 않고 그 대신 주사위를 1개 굴려 4~6이 나오면 아컴에 있는 모든 조사자는 체력 1을 잃는다.
        if (monster.id == "HoundofTindalos")
        {
            currentLocal = monster.GetComponentInParent<Local>();

            // 모든 조사자 들을 불러와 장소에만있는 조사자만 분리해서 저장
            GameObject[] characters = new GameObject[4];
            List<Character> LocalCharacter = new List<Character>();
            characters = GameObject.FindGameObjectsWithTag("Player");

            for (int i = 0; i < characters.Length; i++)
            {
                Debug.Log("캐릭터 리스트 : " + characters[i]);

                if (characters[i] != null)
                {
                    // 캐릭터가 위치한 지역의 이동가능경로 수를 보고 지역인지 장소인지 판별
                    Character ch = characters[i].GetComponent<Character>();
                    Local local = Local.GetLocalObjById(ch.currentLocal_Id);

                    //////////////////////////     이부분도 Localid 변경되면 바꾸기
                    if (local.allowLocal_Id.Length == 1)
                    {
                        Debug.Log("캐릭터 리스트 : " + characters[i] + "는 거리에 있습니다");
                        LocalCharacter.Add(ch);
                    }
                }
            }


            int distance = 0;
            int farthest = 0;
            Character farthestChar = null;

            for (int i = 0; i < LocalCharacter.Count; i++)
            {
                if (LocalCharacter[i] != null)
                {
                    distance = DistanceBetween.instance.GetDistance(currentLocal.local_Id, LocalCharacter[i].currentLocal_Id);

                    if (distance > farthest)
                    {
                        farthest = distance;
                        farthestChar = LocalCharacter[i];
                    }
                }
            }
            Debug.Log("가장 멀리 떨어져있는 캐릭터 :" + farthestChar.name);

            goalLocal = Local.GetLocalObjById(farthestChar.currentLocal_Id);
            StartCoroutine(MovePosition(goalLocal.position));
        }
        //크토니안 
        else
        {
            //주사위 이벤트 호출
        }
    }

    private IEnumerator MovePosition(Vector3 position)
    {
        Vector3 goalPosition = new Vector3(position.x, 1.2f, position.z - 3.0f);

        while (Vector3.Distance(goalPosition, transform.position) >= 1.0f)
        {
            //Debug.Log(this.name + "    Distance : "  + Vector3.Distance(goalPosition, transform.position) + "   currentPosition : " + transform.position + "    GoalPosition : " + goalPosition);

            transform.position = Vector3.MoveTowards(transform.position, goalPosition, 10.0f * Time.deltaTime); //현재 캐릭터 정보에있는 위치와 이동해야될 위치를 보고 직선으로 이동 

            yield return new WaitForSeconds(0.01f);
        }

        monster.transform.SetParent(goalLocal.transform);

        yield return null;
    }
}
