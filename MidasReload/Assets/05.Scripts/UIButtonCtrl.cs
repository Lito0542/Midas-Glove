using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class UIButtonCtrl : MonoBehaviour {

    public UnityEvent Hit;
    public UnityEvent Released;

    int state = 0;
    int prevState = 0;

    public void OnTriggerEnter(Collider other)
    {
        state = 1;
    }
    public void OnTriggerStay(Collider other)
    {
        state = 1;
    }
    public void OnTriggerExit(Collider other)
    {
        state = 0;
    }

    private void Update()
    {
        if (state == 1 && prevState == 0)
            Hit.Invoke();
        else if (state == 0 && prevState == 1)
            Released.Invoke();

        prevState = state;
    }
}
