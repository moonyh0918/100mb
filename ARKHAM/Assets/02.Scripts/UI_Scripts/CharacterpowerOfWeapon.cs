using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterpowerOfWeapon : MonoBehaviour {

    private Text powerOfWeapon;

    void Start () {
        powerOfWeapon = GetComponent<Text>();
        powerOfWeapon.text = Character.instance.CharacterpowerOfWeapon.ToString();
    }

	void Update () {
        powerOfWeapon.text = Character.instance.CharacterpowerOfWeapon.ToString();
    }
}
