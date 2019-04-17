using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaintManager : MonoBehaviour {

    public int nowPaint = 0; //0 : none, 1 : red, 2 : green, 3 : blue

    public Transform RedPaint;
    public Transform GreenPaint;
    public Transform BluePaint;

    public Transform PaintPoint;

    public GestureManager gestureManager;

	void Update () {
		if(gestureManager.RFingerState[0] == GestureManager.State.Fist && 
            gestureManager.RFingerState[1] == GestureManager.State.Stretch &&
            gestureManager.RFingerState[2] == GestureManager.State.Fist &&
            gestureManager.RFingerState[3] == GestureManager.State.Fist &&
            gestureManager.RFingerState[4] == GestureManager.State.Fist )
        {
            switch (nowPaint)
            {
                case 1:
                    RedPaint.position = PaintPoint.position;
                        break;
                case 2:
                    GreenPaint.position = PaintPoint.position;
                    break;
                case 3:
                    BluePaint.position = PaintPoint.position;
                    break;
            }
        }
	}
}
