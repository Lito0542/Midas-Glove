using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterCtrl : MonoBehaviour {

    public PaintManager paintManager;
    public int color = 0;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Hand"))
        {
            paintManager.nowPaint = color;
        }
    }
}
