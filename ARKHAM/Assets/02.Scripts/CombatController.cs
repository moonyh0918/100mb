using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatController : MonoBehaviour {

    public Character character;
    public Monster monster;

    public GameObject mainCamera;
    public GameObject combatCamera;

    public static CombatController instance = null;

    private void Awake()
    {
        instance = this;
    }

    public  void SetCombatController(Character _character, Monster _monster)
    {
        character = _character;
        monster = _monster;

        // CobatUIPanel 변경 후 호출 
        CombatUI.instance.SetCombatUI(monster);
        
        combatCamera.SetActive(true);
        mainCamera.SetActive(false);
    }

    public void EvasionCheck()
    {
        Debug.Log("EvasionCheck : " + character.CharacterEvadeCheck + "(Character sneak)  +  " + monster.evasionLevel + "(Monster EvasionLevel)" );

        CombatUI.instance.CombatUIActive(false);

        //character.EvasionCheck(monster.evasionLevel);

        DiceController.instance.SetDice(character.CharacterEvadeCheck + monster.evasionLevel, Character.instance.minDiceSucc, 6, DiceController.Use.EvasionCheck);
    }

    public void EvasionCheckResult(int successCount)
    {
        if (successCount > monster.evasionLevel)
        {
            Debug.Log("회피성공");
            FinishCombat();
        }
        else
        {
            Debug.Log("회피체크 실패, " + monster.staminaDamage + "만큼 체력피해를 입습니다");
            character.DamagedStamina(monster.staminaDamage);
            CombatUI.instance.UdpatePlayerUI();

            // Damage를 입고 나서 캐릭터의 상태를 파악, 체력이 0이면 FAINT(실신)상태이므로 전투종료
            if (Character.instance.characterState != Character.State.FAINT)
                CombatUI.instance.CombatUIActive(true);
            else
                StartCoroutine(PlayerLose());
        }
    }

    public void FearCheck()
    {
        Debug.Log("FearCheck: " + character.HorrorCheck + "(Character Will)  +  " + monster.fearLevel + "(Monster FearLevel)");

        // UI 변경 
        CombatUI.instance.CombatUIActive(false);

        DiceController.instance.SetDice(character.HorrorCheck + monster.fearLevel, Character.instance.minDiceSucc, 6, DiceController.Use.FearCheck);     
    }

    public void FearCheckResult(int successCount)
    {
        if (successCount == 0)
        {
            Debug.Log("공포체크 실패, " + monster.sanityDamage + "만큼 정신피해를 입습니다");
            character.DamagedSanity(monster.sanityDamage);
            CombatUI.instance.UdpatePlayerUI();
        }
        else
            Debug.Log("공포체크 성공 ");

        if (Character.instance.characterState != Character.State.FAINT)
            CombatCheck();
        else
            StartCoroutine(PlayerLose());
    }


    public void CombatCheck()
    {
        Debug.Log("CombayCheck: " + character.CombatCheck + "(Character Fight)  +  " + monster.combatLevel + "(Monster combatLevel)");

        // UI변경

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
        if (successCount >= monster.hp)
        {
            StartCoroutine(PlayerWin());
        }
        else
        {
            Debug.Log("전투체크 실패, " + monster.staminaDamage + "만큼 체력피해를 입습니다");

            character.DamagedStamina(monster.staminaDamage);
            CombatUI.instance.UdpatePlayerUI();

            if (Character.instance.characterState != Character.State.FAINT)
                CombatUI.instance.CombatUIActive(true);
            else
                StartCoroutine(PlayerLose());
        }
    }


    private IEnumerator PlayerWin()
    {
        CombatUI.instance.CombatUIActive(false);
        CombatUI.instance.CombatAnimUIActive(true);

        // 플레이어는 전투를 통해 몬스터를 죽였다면, 몬스터HP만큼 트로피획득
        character.sumMonsterHpNum += monster.hp;

        MonsterController.instance.MonsterDie(monster);

        // 이동단계 때 전투에서 승리시 남은 이동력만큼 마저 이동 가능 
        character.currentMoveCount = character.maxMoveCount;

        // 애니메이션이 완료 된 후 Panel 비활성화를 위해 잠시 대기 
        CombatUI.instance.PlayerWinAnim();
        yield return new WaitForSeconds(3.0f);
        FinishCombat();
    }


    private IEnumerator PlayerLose()
    {
        CombatUI.instance.CombatUIActive(false);
        CombatUI.instance.CombatAnimUIActive(true);

        CombatUI.instance.PlayerLoseAnim();
        yield return new WaitForSeconds(3.0f);
        FinishCombat();
    }

    private void FinishCombat()
    {
        Character.instance.CobatComplete();

        combatCamera.SetActive(false);
        mainCamera.SetActive(true);
        MaincameraController.instance.SetPosition();

        // UI초기화 
        CombatUI.instance.FinishCombatAnim();

        CombatUI.instance.CombatUIActive(false);
        CombatUI.instance.CombatAnimUIActive(false);

        // 전투 종료 후 차원문의 콜라이더 컴포넌트를 활성화 할지 결정 (만약 몬스터가 더 있다면 계속 전투 해야 하므로 차원문의 콜라이더는 꺼져있어야한다.)
        Local combatLocal = Local.GetLocalObjById(character.currentLocal_Id);
        combatLocal.GetComponentInChildren<Gate>().GateColliderAct();   //널값참조남
    }
}
