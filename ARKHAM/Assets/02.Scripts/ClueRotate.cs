using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClueRotate : MonoBehaviour {

    void FixedUpdate()
    {
        transform.Rotate(Vector3.forward * Time.deltaTime * 22.0f);
    }
}
