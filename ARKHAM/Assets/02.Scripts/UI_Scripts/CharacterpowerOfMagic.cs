using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterpowerOfMagic : MonoBehaviour {

    private Text powerOfMagic;

    void Start()
    {
        powerOfMagic = GetComponent<Text>();
        powerOfMagic.text = Character.instance.CharacterpowerOfMagic.ToString();
    }

    // Update is called once per frame
    void Update()
    {

        powerOfMagic.text = Character.instance.powerOfMagic.ToString();
    }
}
