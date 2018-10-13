﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clue : MonoBehaviour {

    public void OnTriggerStay(Collider other)
    {

            if (other.tag == "Player")
            {
                if (GameManager.instance.gameState == GameManager.GameState.Encounter)
                {
                    Debug.Log("클루 삭제");
                    Character.instance.clue += 1;
                    Destroy(this);
                    
                }
            }


    }

    
}
