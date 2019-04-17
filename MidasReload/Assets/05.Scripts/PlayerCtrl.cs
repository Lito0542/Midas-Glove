using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCtrl : MonoBehaviour {

    public float speed = 6.0F;
    public float gravity = 20.0F;

    public Transform Player;

    void Update()
    {
        if(Input.GetKey(KeyCode.W))
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }
}
