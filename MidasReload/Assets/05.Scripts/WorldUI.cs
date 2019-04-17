using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldUI : MonoBehaviour {

    public Transform Target;

    void Update()
    {
        transform.LookAt(Target);
        transform.Rotate(0, 180, 0);

    }
}
