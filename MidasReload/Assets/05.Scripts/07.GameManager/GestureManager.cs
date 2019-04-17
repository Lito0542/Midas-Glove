using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GestureManager : MonoBehaviour {

    public int[] fistPer = new int[5];
    public int[] stretchPer = new int[5];

    public enum State { Fist, None, Stretch }
    public State[] RFingerState = new State[5];
    public State[] LFingerState = new State[5];

    public RGripEvent RGrip;

    public int nowRGripItem = 0;
    public int nowLGripItem = 0;

    bool isRFistDown = false;
    bool isLFistDown = false;
    bool isRFist = false;
    bool isLFist = false;
    bool isPrevRFist = false;
    bool isPrevLFist = false;
    bool isRFistUp = false;
    bool isLFistUp = false;

    public FingerMove fingerMove;

	void Update () {
        for (int i = 0; i < 5; i++)
        {
            if (fingerMove.RbentDegree[i] < fistPer[i])
                RFingerState[i] = State.Fist;
            else if (fingerMove.RbentDegree[i] > stretchPer[i])
                RFingerState[i] = State.Stretch;
            else
                RFingerState[i] = State.None;

            if (fingerMove.LbentDegree[i] < fistPer[i])
                LFingerState[i] = State.Fist;
            else if (fingerMove.LbentDegree[0] > stretchPer[i])
                LFingerState[i] = State.Stretch;
            else
                LFingerState[i] = State.None;
        }

        //잡기
        {
            if (RFingerState[0] != State.Fist && RFingerState[1] != State.Fist && RFingerState[2] != State.Fist)
            {
                isRFist = false;
                if (isPrevRFist)
                    isRFistUp = true;
            }
            if (RFingerState[0] == State.Fist && RFingerState[1] == State.Fist && RFingerState[2] == State.Fist)
            {
                isRFist = true;
                if (!isPrevRFist)
                    isRFistDown = true;
            }
            isPrevRFist = isRFist;

            if (LFingerState[0] != State.Fist && LFingerState[1] != State.Fist && LFingerState[2] != State.Fist)
            {
                isLFist = false;
                if (isPrevLFist)
                    isLFistUp = true;
            }
            if (LFingerState[0] == State.Fist && LFingerState[1] == State.Fist && LFingerState[2] == State.Fist)
            {
                isLFist = true;
                if (!isPrevLFist)
                    isLFistDown = true;
            }
            isPrevLFist = isLFist;
        }

        if (isRFistDown)
            RGrip.GripEvent();

        if (isRFistUp && RGrip.isGripped)
            RGrip.ReleaseEvent();

        isRFistDown = false;
        isRFistUp = false;
    }
}
