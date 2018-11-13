using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 캐릭터의 상태와 게임 단계에 따라 진행, 한 버튼으로 동작하게 

// 1. 보스 랜덤 선택 후 출력 
// 2. 캐릭터 선택 

    
// 3. 신화 단계 

// 4. 유지 단계 
// 5. 이동 단계
// 6. 조우 단계 
// 7. 신화 단계 
// 8. 순서 4 로 이동 

public class GameManager : MonoBehaviour {

    public enum GameState {Setting,Upkeep, Move, Encounter, Mythos, finalbattle}
    public GameState gameState;

    public GameStateUI gameSateUI;

    public static GameManager instance = null;
   

    private void Awake()
    {
        instance = this;
    }


    void Start()
    {
        gameState = GameState.Setting;
        //RandomBoss();
        SettingController.instance.SheetSetting();    
    }


    public void DownButton()
    {
        switch(gameState)
        {
            case GameState.Setting:
                GameSetting();
                break;
            case GameState.Upkeep:
                MoveState();
                break;
            case GameState.Move:
                EncounterState();
                break;
            case GameState.Encounter:
                MythosState();
                break;
            case GameState.Mythos:
                UpkeepState();
                break;
        }
    }


    /*public void RandomBoss()
    {
        Debug.Log("Boss Random Choice");

        // 랜덤 선택 후 출력 그 다음에 자동으로 캐릭터 선택 단계로
        ChoiceCharacter();
    }*/


    /*public void ChoiceCharacter()
    {
        Debug.Log("Character Choice Complete");
        // 캐릭터 선택 완료 후 

        // 처음 신화는 소문이 아닌 카드를 뽑을 때 까지 Draw()
        MythosController.instance.FirstMythos();
    }*/


    public void GameSetting()
    {
        NowPhase.instance.PopNowPhase();
        gameState = GameState.Mythos;
        gameSateUI.gameObject.SetActive(true);
        gameSateUI.UpdateStateUI("신 화 단 계");

        SettingController.instance.EndSetting();
    }



    public void UpkeepState()
    {
        NowPhase.instance.PopNowPhase();
        MythosController.instance.MythosStateEnd();
   
        gameState = GameState.Upkeep;
        gameSateUI.gameObject.SetActive(true);
        gameSateUI.UpdateStateUI("유 지 단 계");

        UpkeepButtonEvent.instance.UpkeepEnCounterStep();
        UpkeepButtonEvent.instance.retainerAndBless();
    }


    public void MoveState()
    {
        NowPhase.instance.PopNowPhase();

        gameState = GameState.Move;
        gameSateUI.gameObject.SetActive(true);
        gameSateUI.UpdateStateUI("이 동 단 계");

        UpkeepButtonEvent.instance.UpkeepStepEnd();
        MoveController.instance.CheckOtherWorld();

        // 이동 단계 UI 결정되면 출력함수 호출
    }


    public void EncounterState()
    {
        // 이동 단계 UI 비활성화 함수 호출  
        NowPhase.instance.PopNowPhase();

        gameState = GameState.Encounter;
        gameSateUI.gameObject.SetActive(true);
        gameSateUI.UpdateStateUI("조 우 단 계");

        LocalEventController.instance.LocalEnCounterStep();
    }


    public void MythosState()
    {
        NowPhase.instance.PopNowPhase();
        //이벤트 화면 비활성화 홈수
        LocalEventController.instance.ExitEvent();
        
        gameState = GameState.Mythos;
        gameSateUI.gameObject.SetActive(true);
        gameSateUI.UpdateStateUI("신 화 단 계");

        MythosController.instance.MythosStep();

    }


    public bool CheckGameState(GameState confirmState)
    {
        if (confirmState == gameState)
            return true;
        else
            return false;
    }
}
