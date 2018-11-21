using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonUIActive : MonoBehaviour {

    public GameObject mainButton;

    private void Start()
    {
        GameObject mainButtonObj = GameObject.Find("MainButtonObj");
        mainButton = mainButtonObj.transform.Find("MainButton").gameObject;
    }

    public void MainButtonActive()
    {
        mainButton.SetActive(true);
    }
}
