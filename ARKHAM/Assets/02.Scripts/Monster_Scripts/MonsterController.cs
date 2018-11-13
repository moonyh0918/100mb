using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


// 이동은 남에게 부탁할시 여러개가 동시에 움직여야한다면 배열로 전달해서 한번씩돌아가면서 movetowards 하던가
// 왠만하면 이동은 움직이는대상스크립트에 넣어서 인스턴스되어 각자 스스로 움직이게 되게하자 
public class MonsterController : MonoBehaviour {
    
    public Monster monsterPrefab;
    public Local library;

    [SerializeField]
    public List<Monster> monsters = new List<Monster>();

    public int number = 0;
    
    public static MonsterController instance = null;

    private void Awake()
    {
        instance = this;
    }

    public void CreateMonster(Local local)
    {
        Monster dictionaryMon = MonsterDictionary.instance.RandomMonster();

        // 몬스터가 필드에 4마리가 있다면 다음 몬스터는 외각에 UI형태로 생성, 오브젝트를 생성하지 않음
        if (monsters.Count >= 4)
        {
            OutSkirtsUI.instance.UpdateOutSkirtsUI(dictionaryMon.id);
            return;
        }


        Monster instanceMon = Instantiate(monsterPrefab, Vector3.zero, Quaternion.Euler(0, 0, 0));
        instanceMon.transform.parent = local.transform;
        instanceMon.transform.localPosition = new Vector3(0, -1, 1);
        instanceMon.transform.position = new Vector3(instanceMon.transform.position.x, 1.2f, instanceMon.transform.position.z);

        instanceMon.CopyValue(ref dictionaryMon);
        instanceMon.name = instanceMon.name + number;

        // 이미지를 Resources파일에서 가져옴
        Texture monsterTexture = Resources.Load<Texture>("MonsterImages/" + instanceMon.id);
        instanceMon.GetComponent<MeshRenderer>().material.mainTexture = monsterTexture;

        monsters.Add(instanceMon);

        number++;
    }


    public IEnumerator MoveOneByOne(List<Monster.Simbol> simbol, MonsterMoveController.Color color)
    {
        for (int k = 0; k < simbol.Count; k++)
        {
            //Debug.Log("Simbol : " + simbol[k] + "   Color : " + color);

            for (int i = 0; i < monsters.Count; i++)
            {

                if (monsters[i].simbol == simbol[k])
                {
                    //Debug.Log(monsters[i] + " is move Start");

                    MonsterMoveController moveCtrl = monsters[i].GetComponent<MonsterMoveController>();

                    // 카메라 focus 몬스터에게 이동 
                    MaincameraController.instance.ChangeTarget(monsters[i].gameObject);

                    IEnumerator coroutine = moveCtrl.MoveEachType(color);
                    yield return StartCoroutine(coroutine);
                    
                    MaincameraController.instance.ChangeTarget(FindObjectOfType<Character>().gameObject);
                    Debug.Log(monsters[i] + " is move finished");
                }
            }
        }
        yield return null;
    }

    public void MonsterDie(Monster mon)
    { 
        // id값으로 하면 같은종류의 몬스터가 필드에있을때 엉뚱한 몬스터가 사라질수 있으므로 +number를 붙인 name으로 검색 
        Monster resultMon = monsters.Find(item => item.name == mon.name);
        monsters.Remove(resultMon);

        Destroy(mon.gameObject);
    }
}
