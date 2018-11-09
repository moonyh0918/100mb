using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnotherWorldUI : MonoBehaviour {

    public GameObject otherWorldPanel;

    public static AnotherWorldUI instance;


    private void Awake()
    {
        instance = this;

        otherWorldPanel = transform.GetChild(0).gameObject;
    }


    public void InOtherWorld()
    {
        otherWorldPanel.SetActive(true);
    }


    public void OutOtherWorld()
    {
        otherWorldPanel.SetActive(false);
    }

}
