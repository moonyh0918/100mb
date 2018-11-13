using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectCharacter : MonoBehaviour {

    public Character Characterset;

    public static SelectCharacter instance = null;

    public void Awake()
    {
        instance = this;
    }

    public void PickCharacter(int num)
    {
        switch(num)
        {
            case 0:
                AmandaSharpe();
                break;
            case 1:
                AshcanPete();
                break;
            case 2:
                BobJenkins();
                break;


        }
    }

    //임시 스텟부여
    public void AmandaSharpe()
    {
        Characterset.currentLocal_Id = 0;

        Characterset.CharacterSanity = 5;
        Characterset.MaxCharacterSanity = 5;

        Characterset.CharacterStamina = 5;
        Characterset.MaxCharacterStamina = 5;

        Characterset.CharacterSpeed = 1;
        Characterset.CharacterSneak = 4;

        Characterset.CharacterFight = 1;
        Characterset.CharacterWill = 4;

        Characterset.CharacterLore = 1;
        Characterset.CharacterLuck = 4;

        Characterset.maxFocus = 3;
        Characterset.CharacterFocus = 3;

        Characterset.minDiceSucc = 5;

        Characterset.money = 1;
        Characterset.clue = 1;

        Characterset.currentLocal_Id = 53;

        

        Characterset.SheetImage = Resources.Load<Sprite>("Sheet/AmandaSharpe");

    }

    public void AshcanPete()
    {
        Characterset.currentLocal_Id = 1;

        Characterset.CharacterSanity = 4;
        Characterset.MaxCharacterSanity = 4;

        Characterset.CharacterStamina = 6;
        Characterset.MaxCharacterStamina = 6;

        Characterset.CharacterSpeed = 0;
        Characterset.CharacterSneak = 6;

        Characterset.CharacterFight = 2;
        Characterset.CharacterWill = 5;

        Characterset.CharacterLore = 0;
        Characterset.CharacterLuck = 3;

        Characterset.maxFocus = 1;
        Characterset.CharacterFocus = 1;

        Characterset.minDiceSucc = 5;

        Characterset.money = 1;
        Characterset.clue = 3;

        Characterset.currentLocal_Id = 32;

        Characterset.SheetImage = Resources.Load<Sprite>("Sheet/AshcanPete");
    }
    public void BobJenkins()
    {
        Characterset.currentLocal_Id = 2;

        Characterset.CharacterSanity = 4;
        Characterset.MaxCharacterSanity = 4;

        Characterset.CharacterStamina = 6;
        Characterset.MaxCharacterStamina = 6;

        Characterset.CharacterSpeed = 2;
        Characterset.CharacterSneak = 3;

        Characterset.CharacterFight = 1;
        Characterset.CharacterWill = 6;

        Characterset.CharacterLore = 0;
        Characterset.CharacterLuck = 4;

        Characterset.maxFocus = 1;
        Characterset.CharacterFocus = 1;

        Characterset.minDiceSucc = 5;

        Characterset.money = 9;
        Characterset.clue = 0;

        Characterset.currentLocal_Id = 43;

        Characterset.SheetImage = Resources.Load<Sprite>("Sheet/BobJenkins");
    }
}
