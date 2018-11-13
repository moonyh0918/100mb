  using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Character : MonoBehaviour
{
    public int characterid;

    IEnumerator MovingCharacter;
    public Sprite SheetImage;

    /////////////////////////  변수 이름 소문자 시작,    프로퍼티는 대문자 시작을 전부 바꿔놓기
    //캐릭터스텟
    public int sanity;
    public int CharacterSanity { get { return sanity; } set { sanity = value; } }
    public int maxSanity;
    public int MaxCharacterSanity { get { return maxSanity; } set { maxSanity = value; } }
    public int stamina;
    public int CharacterStamina { get { return stamina; } set { stamina = value; } }
    public int maxStamina;
    public int MaxCharacterStamina { get { return maxStamina; } set { maxStamina = value; } }

    public int speed;
    public int CharacterSpeed { get { return speed; } set { speed = value; } }
    public int sneak;
    public int CharacterSneak { get { return sneak ; } set { sneak = value; } }

    public int fight;
    public int CharacterFight { get { return fight; } set { fight = value; } }
    public int will;
    public int CharacterWill { get { return will; } set { will = value; } }

    public int lore;
    public int CharacterLore { get { return lore; } set { lore = value; } }
    public int luck;
    public int CharacterLuck { get { return luck; } set { luck = value; } }

    public int maxFocus;
    public int focus;
    public int CharacterFocus { get { return focus; } set { focus = value; } }

    public int minDiceSucc = 5;


    //소지품

    public int money = 0;
    public int clue = 0;
    public int nowHand = 0;


    public List<ItemCard> characterInventory;
    public int gateNum;
    public int sumMonsterHpNum;

    public bool retainer=false; //보유자산



    //전투 관련 변수 
    public int powerOfWeapon;  //무기공격력(한손x2 or 양손)
    public int CharacterpowerOfWeapon { get { return powerOfWeapon; } set { powerOfWeapon = value; } }
    public int powerOfMagic;   //마법공격력
    public int CharacterpowerOfMagic { get { return powerOfMagic; } set { powerOfMagic = value; } }
    public int evade;  //회피 (은둔 + 기술 or 조력자 의 회피+1 의 경우, 은둔체크는 기본스텟으로,회피체크는 이 변수로)
    public int CharacterEvadeCheck { get { return evade + sneak; } set { evade = value; } }
    public int characterHorrorCheck;    //공포체크(의지+공포체크)
    public int HorrorCheck { get { return will + characterHorrorCheck; } set { characterHorrorCheck = value; } } //공포
    public int characterCombatCheck;    //토탈
    public int CombatCheck { get { return powerOfWeapon + powerOfMagic + fight + characterCombatCheck; } set { characterCombatCheck = value; } } //투지+무기 수치
    
    //이동 관련
    public int maxMoveCount;  //이동가능 횟수
    public int currentMoveCount = 0;    //현재 이동횟수

    public Vector3 goalPosition;   //이동해야될 위치

    public int currentLocal_Id=0;
    
    // 캐릭터 상태 변수,  현재는 전투 중 죽을 경우에만 사용, 모든 경우에 적용시키기 
    public enum State {IDLE, MOVE, COMBAT, FAINT};
    public State characterState = State.IDLE;


    public bool specialLocalCheck=false;  //탐사 완료 표시

    //싱글턴 선언
    public static Character instance = null;


    private void Awake()
    {
       if(instance  == null)
        {
            instance = this;
        }
       else if(instance != this)
        {
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(this.gameObject);

    }

    public void StartMove(Vector3 goalPos)
    {
        goalPosition = goalPos;

        if (characterState == State.IDLE && currentMoveCount < maxMoveCount && transform.position != goalPosition)
        {
            characterState = State.MOVE; 
            currentMoveCount++;
            MovingCharacter = MoveController.instance.MovePosition(goalPosition);
            StartCoroutine(MovingCharacter);
        }   
    }
   
    public void MovingComplete()
    {
        if (characterState == State.MOVE)
            characterState = State.IDLE;
    }

    public void CobatComplete()
    {
        characterState = State.IDLE;
    }

    public void OnTriggerEnter(Collider other)
    {
        
        if (other.CompareTag("LOCAL"))
        {
            currentLocal_Id = other.GetComponent<Local>().local_Id;
        }
        // 이동 시 몬스터 지역으로 이동, 바로 전투의 경우만 해당 
        else if (other.CompareTag("MONSTER") && GameManager.instance.CheckGameState(GameManager.GameState.Move))
        {
            characterState = State.COMBAT;
            CombatController.instance.SetCombatController(this, other.GetComponent<Monster>());
        }
            

        if(other.CompareTag("Gate") && !specialLocalCheck)//specialLocalCheck는 탐사 완료 마커 기능 true일경우 차원무네 빨려가지 않게됨
        {

            StopCoroutine(MovingCharacter);
            MovingComplete();
            Transform OtherWorld = other.GetComponent<Gate>().OpenLocal.transform;
            transform.position = OtherWorld.position; //다른세계로 날려보내기

            AnotherWorldUI.instance.InOtherWorld();
            GateController.instance.characterInGate = other.GetComponent<Gate>();
        }
        else if(other.CompareTag("Gate") && specialLocalCheck)
        {
            GateController.instance.characterInGate=other.GetComponent<Gate>(); //현재 캐릭터가 있는 차원문 정보를 넘겨줌
        }

    }

    public void HealSanity(int heal)
    {
        CharacterSanity -= heal;

        if (MaxCharacterSanity < CharacterSanity)
            CharacterSanity = MaxCharacterSanity;
        NowMp.instance.PopMpUi();
    }
    public void HealStamina(int heal)
    {
        CharacterStamina -= heal;

        if (MaxCharacterStamina < CharacterStamina)
            CharacterStamina = MaxCharacterStamina;

        NowHp.instance.PopHpUi();

    }


    public void DamagedSanity(int damage)
    {
        CharacterSanity -= damage;
        NowMp.instance.PopMpUi();
        if (CharacterSanity <= 0)
            DieCuzSanity();
        
    }

    public void DamagedStamina(int damage)
    {
        CharacterStamina -= damage;
        NowHp.instance.PopHpUi();
        if (CharacterStamina <= 0)
            DieCuzStamina();
    }


    void DieCuzStamina()
    {
        Debug.Log("체력이 0이하가 되어 죽음");

        CharacterStamina = 1;
        NowHp.instance.PopHpUi();
        Local localAsylum = GameObject.FindObjectOfType<Local_Asylum>();

        CharacterDie(localAsylum);
    }

    void DieCuzSanity()
    {
        Debug.Log("정신력이 0이하가 되어 죽음");

        CharacterSanity = 1;
        NowMp.instance.PopMpUi();
        Local localHospitol = GameObject.FindObjectOfType<Local_STMarysHos>();

        CharacterDie(localHospitol);
    }


    // 체력이 없어 죽었을 경우 인자로 Asylum병원으로, 정신력 때문에 죽었을 경우 정신병원으로
    void CharacterDie(Local local)
    {
        /* 이계에서 죽었을 경우   - 미구현 
        if (currentLocal_Id / 10 == 11)
        {
            시공간상의 실종 함수 호출 
        }
        */

        characterState = State.FAINT;

        // 캐릭터와 카메라 해당 병원으로 이동
        Vector3 localPosition = local.transform.position;
        transform.position = new Vector3(localPosition.x, 1.0f, localPosition.z - 3.0f);

        // 잃어버릴때 애니메이션 적용 
        money = 0;
        clue = 0;
        // 아이템은 선택해서 버리게 

        //메인카메라 
    }

    public void ItemReset()
    {
        List<ItemCard> result;
        result =characterInventory.FindAll(delegate(ItemCard cards)
        {
            return cards.useCheck == true;
        }
        );

        if(result.Count!=0)
        {
            for (int i=0;i<result.Count;i++)
            {
                Debug.Log(result[i]);
                result[i].useCheck = false;
            }
        }

        powerOfMagic = 0;
        powerOfWeapon = 0;
        nowHand = 0;      
    }
}
