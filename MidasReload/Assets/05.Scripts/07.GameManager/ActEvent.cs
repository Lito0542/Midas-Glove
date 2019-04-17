using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ActEvent : MonoBehaviour {

    public Transform LeftTracker;
    public Transform RightTracker;

    public SteamVR_Controller.Device LeftDevice;
    public SteamVR_Controller.Device RightDevice;

    [Header("RightHand")]
    public UnityEvent RightHand_SwipLeft, RightHand_SwipRight;
    public UnityEvent RightHand_SwipUp,RightHand_SwipDown;
    public UnityEvent RightHand_SwipForward,RightHand_SwipBack;

    [Space]
    [Header("LeftHand")]
    public UnityEvent LefHand_SwipLeft,LefHand_SwipRight;
    public UnityEvent LefHand_SwipUp, LefHand_SwipDown;
    public UnityEvent LefHand_SwipForward,LefHand_SwipBack;

    bool isMoving = false;

    private void Start()
    {
        LeftDevice = SteamVR_Controller.Input((int)LeftTracker.GetComponent<SteamVR_TrackedObject>().index);
        RightDevice = SteamVR_Controller.Input((int)RightTracker.GetComponent<SteamVR_TrackedObject>().index);
    }

    void Update () {
        Vector3 RVec = Vector3.zero, LVec = Vector3.zero;
        if (RightTracker.gameObject.activeSelf)
            RVec = RightDevice.velocity;
        if (LeftTracker.gameObject.activeSelf)
            LVec = LeftDevice.velocity;

        if (RVec.magnitude > 1.2f && !isMoving)
        {
            isMoving = true;
            Invoke("MoveOff", 0.3f);
            float x = Mathf.Abs(RVec.x);
            float y = Mathf.Abs(RVec.y);
            float z = Mathf.Abs(RVec.z);

            if (x > y && x > z)
            {
                if (RVec.x < 0)
                    RightHand_SwipLeft.Invoke();
                else
                    RightHand_SwipRight.Invoke();
            }
            if (y > z)
            {
                if (RVec.y < 0)
                    RightHand_SwipDown.Invoke();
                else
                    RightHand_SwipUp.Invoke();
            }
            else
            {
                if (RVec.z < 0)
                    RightHand_SwipBack.Invoke();
                else
                    RightHand_SwipForward.Invoke();
            }
        }
    }

    void MoveOff()
    {
        isMoving = false;
    }
}
