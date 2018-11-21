using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatController : MonoBehaviour {

    public Character character;
    public Monster monster;

    public CombatUI combatUI;

    public static CombatController instance = null;
    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        combatUI = gameObject.GetComponent<CombatUI>();
    }

    public  void SetCombatController(Character _character, Monster _monster)
    {
        character = _character;
        monster = _monster;

        combatUI.SetCombatUI(monster);
    }

    public void EvasionCheck()
    {
        combatUI.CombatUIActive(false);

        // 던지는 주사위 수 : 캐릭터의 EvadeCheck수치 + 몬스터의 공포레벨 / 주사위 성공값은 : 캐릭터의 minDiceSucc ~ 6 / 주사위의 용도는 공포체크  
        DiceController.instance.SetDice(character.CharacterEvadeCheck + monster.evasionLevel, character.minDiceSucc, 6, DiceController.Use.EvasionCheck);
    }

    public void EvasionCheckResult(int successCount)
    {
        //회피 성공시 전투종료, 실패시 데미지를 입고 처음으로 돌아간다;
        if (successCount > monster.evasionLevel)
            FinishCombat();
        else
        {
            character.DamagedStamina(monster.staminaDamage);
            combatUI.UdpatePlayerUI();

            // Damage를 입고 나서 캐릭터의 상태를 파악, 체력이 0이면 FAINT(실신)상태이므로 전투종료
            if (character.characterState == Character.State.FAINT)
                StartCoroutine(PlayerLose());
            else
                combatUI.CombatUIActive(true);
        }
    }

    public void FearCheck()
    {
        combatUI.CombatUIActive(false);

        DiceController.instance.SetDice(character.HorrorCheck + monster.fearLevel, character.minDiceSucc, 6, DiceController.Use.FearCheck);     
    }

    public void FearCheckResult(int successCount)
    {
        // 공포체크 실패, monster.sanityDamage만큼 정신피해;
        if (successCount == 0)
        {
            character.DamagedSanity(monster.sanityDamage);
            combatUI.UdpatePlayerUI();
        }

        // 공포체크 성공여부에 상관없이 전투체크로 넘어간다.
        if (character.characterState != Character.State.FAINT)
            CombatCheck();
        else
            StartCoroutine(PlayerLose());
    }


    public void CombatCheck()
    {
        /*
        // 물리저항

        if(monster.sAttribute.Contains(Monster.SAttribute.PhysicalResist))
        {
            character.CharacterpowerOfWeapon /= 2;
        }
        // 물리면역
        if (monster.sAttribute.Contains(Monster.SAttribute.PhysicalImmunity))
        {
            character.CharacterpowerOfWeapon = 0;
        }
        // 마법저항
        if (monster.sAttribute.Contains(Monster.SAttribute.MagicalResist))
        {
            character.CharacterpowerOfMagic /= 2;
        }
        // 마법면역
        if (monster.sAttribute.Contains(Monster.SAttribute.MagicalImmunity))
        {
            character.CharacterpowerOfMagic = 0;
        }*/
        DiceController.instance.SetDice(character.CombatCheck + monster.combatLevel, Character.instance.minDiceSucc, 6, DiceController.Use.CombatCheck);
    }


    public void CombatCheckResult(int successCount)
    {
        // 주사위 성공수가 몬스터 체력보다 적으면, 주사위 이벤트가 성공해도 전투체크는 실패
        if (successCount >= monster.hp)
        {
            StartCoroutine(PlayerWin());
        }
        else
        {
            character.DamagedStamina(monster.staminaDamage);
            combatUI.UdpatePlayerUI();

            if (Character.instance.characterState != Character.State.FAINT)
                combatUI.CombatUIActive(true);
            else
                StartCoroutine(PlayerLose());
        }
    }


    private IEnumerator PlayerWin()
    {
        // 플레이어는 전투를 통해 몬스터를 죽였다면, 몬스터HP만큼 트로피획득
        character.sumMonsterHpNum += monster.hp;

        MonsterController.instance.MonsterDie(monster);

        // 이동단계 때 전투에서 승리시 남은 이동력만큼 마저 이동 가능 
        character.currentMoveCount = character.maxMoveCount;

        // 애니메이션이 완료 된 후 Panel 비활성화를 위해 잠시 대기 
        combatUI.PlayerWinAnim();
        yield return new WaitForSeconds(3.0f);

        FinishCombat();
    }


    private IEnumerator PlayerLose()
    {
        combatUI.PlayerLoseAnim();
        yield return new WaitForSeconds(3.0f);

        FinishCombat();
    }

    private void FinishCombat()
    {
        Character.instance.CobatComplete();

        combatUI.InitCombatAnim();

        // 전투 종료 후 차원문의 콜라이더를 활성화 여부 체크 (만약 몬스터가 더 있다면 계속 전투 해야 하므로 차원문의 콜라이더는 꺼져있어야한다.)
        Local combatLocal = Local.GetLocalObjById(character.currentLocal_Id);

        Gate openGate = combatLocal.GetComponentInChildren<Gate>();  
        if(openGate != null)
        {
            openGate.GateColliderAct();
        }
    }
}
