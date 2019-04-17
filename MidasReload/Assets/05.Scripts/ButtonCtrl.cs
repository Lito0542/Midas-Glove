using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ButtonCtrl : MonoBehaviour {

    Transform parent;

    public Transform ButtonObj;

    public Transform PointA;
    public Transform PointB;
    
    public UnityEvent HitA;
    public UnityEvent HitB;

    public UnityEvent ReleasedA;
    public UnityEvent ReleasedB;

    int state = 0;
    int prevState = 0;

    private void Update()
    {
        if (parent != null)
        {
            ButtonObj.position = ClosestPointOnLine(parent.position);
        }
        else
        {
            ButtonObj.position = ClosestPointOnLine(ButtonObj.position);
        }

        if (ButtonObj.position == PointA.position)
            state = 1;
        else if (ButtonObj.position == PointB.position)
            state = 2;
        else
            state = 0;

        if (state == 1 && prevState == 0)
            HitA.Invoke();
        else if (state == 2 && prevState == 0)
            HitB.Invoke();
        else if (state == 0 && prevState == 1)
            ReleasedA.Invoke();
        else if (state == 0 && prevState == 2)
            ReleasedB.Invoke();

        prevState = state;
    }

    Vector3 ClosestPointOnLine(Vector3 point)
    {
        Vector3 va = PointA.position;
        Vector3 vb = PointB.position;

        Vector3 vVector1 = point - va;

        Vector3 vVector2 = (vb - va).normalized;

        float t = Vector3.Dot(vVector2, vVector1);

        if (t <= 0)
            return va;
        if (t >= Vector3.Distance(va, vb))
            return vb;

        Vector3 vVector3 = vVector2 * t;
        return va + vVector3;
    }
}
