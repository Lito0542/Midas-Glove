using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class UISliderCtrl : MonoBehaviour {

    public UnityEvent ValueChange;

    public Transform gripObj;
    public Vector3 nowDist = Vector3.zero;
    bool isGrip = false;

    public TextMesh valueText;

    [Header("[Value]")]
    public float value;
    public float maxValue = 1;
    public float minValue = 0;

    [Header("[Range]")]
    public float minPos= 0;
    public float maxPos = 0;
    public float range;
    public float multiply = 1;

    public enum State { x, y, z }
    public State nowState;

    public void OnTriggerEnter(Collider other)
    {
        if (!isGrip)
        {
            gripObj = other.transform;
            nowDist = transform.position - gripObj.position;
            isGrip = true;
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (isGrip && gripObj == other.transform)
        {
            gripObj = null;
            isGrip = false;
        }
    }

    private void Update()
    {
        if (isGrip)
        {
            float degree = 0;
            switch (nowState)
            {
                case State.x:
                    degree = transform.position.x - gripObj.position.x - nowDist.x;
                    if ((degree > 0 && value <= minValue) || (degree < 0 && value >= maxValue))
                        degree = 0;
                    else
                        transform.localPosition
                            = new Vector3(gripObj.position.x + nowDist.x, transform.localPosition.y, transform.localPosition.z);
                    break;
                case State.y:
                    degree = transform.position.y - gripObj.position.y - nowDist.y;
                    if ((degree > 0 && value <= minValue) || (degree < 0 && value >= maxValue))
                        degree = 0;
                    else
                        transform.localPosition
                        = new Vector3(transform.localPosition.x, gripObj.position.y + nowDist.y, transform.localPosition.z);
                    break;
                case State.z:
                    degree = transform.position.z - gripObj.position.z - nowDist.z;
                    if ((degree > 0 && value <= minValue) || (degree < 0 && value >= maxValue))
                        degree = 0;
                    else
                        transform.localPosition
                        = new Vector3(transform.localPosition.x, transform.localPosition.y, gripObj.position.z + nowDist.z);
                    break;
            }
            degree = Mathf.Round(degree * range * 1000) * 0.001f;
            if(degree != 0)
            {
                value -= degree * multiply;
                value = Mathf.Clamp(Mathf.Round(value * 100) * 0.01f, minValue, maxValue);
                valueText.text = value.ToString("F2");
                ValueChange.Invoke();
            }
        }
    }
}
