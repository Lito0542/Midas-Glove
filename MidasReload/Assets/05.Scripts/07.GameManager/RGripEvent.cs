﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RGripEvent : MonoBehaviour {
    
    public SteamVR_TrackedObject trackedObj;
    private SteamVR_Controller.Device controller;

    //public Collider[] collidingObj = new Collider[5];
    public Transform collidingObj;
    public Transform gripObj;
    public bool isGripped = false;

    //public int gripCount = 0;

    public LayerMask gripMask;

    public FingerMove fingerMove;
    public GestureManager gestureManager;

    void Start () {
		
	}
	
	void Update ()
    {
        //for (int i = 0; i < 5; i++)
        //{
        //    if (gestureManager.RFingerState[i] == GestureManager.State.Fist)
        //    {
        //        fingerMove.RFingerOff(i);
        //        Collider[] col= Physics.OverlapBox(fingerMove.RThirdJoint[i].position,new Vector3(0.015f,0.025f,0.015f),Quaternion.identity,gripMask);
        //        if(col.Length > 0)
        //            collidingObj[i] = col[0];
        //        else
        //            collidingObj[i] = null;
        //    }
        //    else
        //        fingerMove.RFingerOn(i);
        //}

        //if (collidingObj[0] != null)
        //{
        //    for (int i = 1; i < 5; i++)
        //    {
        //        if (collidingObj[0] == collidingObj[i])
        //            gripCount++;
        //    }
        //    Debug.Log(gripCount);
        //}

        //if (isGripped && gripCount < 2)
        //    ReleaseEvent();
        //else if (!isGripped && gripCount > 1)
        //    GripEvent();

        //gripCount = 0;
    }

    public void GripEvent()
    {
        if (collidingObj != null && !isGripped)
        {
            isGripped = true;
            gripObj = collidingObj.transform;

            var joint = AddFixedJoint();
            joint.connectedBody = gripObj.GetComponent<Rigidbody>();


            for (int i = 0; i < 5; i++)
                fingerMove.RFingerOff(i);
        }
    }

    private FixedJoint AddFixedJoint()
    {
        FixedJoint fx = gameObject.AddComponent<FixedJoint>();
        fx.breakForce = 20000;
        fx.breakTorque = 20000;
        return fx;
    }

    public void ReleaseEvent()
    {
        if (GetComponent<FixedJoint>())
        {
            GetComponent<FixedJoint>().connectedBody = null;
            Destroy(GetComponent<FixedJoint>());

            Rigidbody gripRb = gripObj.GetComponent<Rigidbody>();
            gripRb.velocity = controller.velocity;
            gripRb.angularVelocity = controller.angularVelocity;
        }
        gripObj = null;
        isGripped = false;

        for (int i = 0; i < 5; i++)
            fingerMove.RFingerOn(i);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (collidingObj == null)
            collidingObj = other.transform;
    }

    private void OnTriggerStay(Collider other)
    {
        if (collidingObj == null)
            collidingObj = other.transform;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform == collidingObj)
            collidingObj = null;
    }
}
