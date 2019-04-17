using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FingerMove : MonoBehaviour {

    public Transform RightWrist;
    public Transform LeftWrist;

    Transform[] RFirstJoint = new Transform[5];
    Transform[] RSecondJoint = new Transform[5];
    public Transform[] RThirdJoint = new Transform[5];

    Transform[] LFirstJoint = new Transform[5];
    Transform[] LSecondJoint = new Transform[5];
    public Transform[] LThirdJoint = new Transform[5];

    Collider[] RFirstCollider = new Collider[5];
    Collider[] RSecondCollider = new Collider[5];

    Collider[] LFirstCollider = new Collider[5];
    Collider[] LSecondCollider = new Collider[5];

    Vector3[] RoriginRot = new Vector3[5];
    Vector3[] LoriginRot = new Vector3[5];

    public float[] LhandFist = new float[5];
    public float[] RhandFist = new float[5];
    public float[] LhandRange = new float[5];
    public float[] RhandRange = new float[5];

    public float[] LbentDegree = new float[5];
    public float[] RbentDegree = new float[5];

    public float[] LbentData = new float[5];
    public float[] RbentData = new float[5];

    public SerialManager serialManager;

    void Start()
    {
        FingerReset();
    }

    public void FingerReset()
    {
        for (int i = 0; i < 5; i++)
        {
            LhandFist[i] = PlayerPrefs.GetFloat("LhandFist" + i, 40);
            RhandFist[i] = PlayerPrefs.GetFloat("RhandFist" + i, 40);

            LhandRange[i] = PlayerPrefs.GetFloat("LhandRange" + i, 100);
            RhandRange[i] = PlayerPrefs.GetFloat("RhandRange" + i, 100);
        }

        RFirstJoint[0] = RightWrist.GetChild(0);
        RSecondJoint[0] = RFirstJoint[0].GetChild(0);
        LFirstJoint[0] = LeftWrist.GetChild(0);
        LSecondJoint[0] = LFirstJoint[0].GetChild(0);

        for (int i = 0; i < 5; i++)
        {
            if (i != 0)
            {
                RFirstJoint[i] = RightWrist.GetChild(1).GetChild(i - 1);
                RSecondJoint[i] = RFirstJoint[i].GetChild(0);
                RThirdJoint[i] = RSecondJoint[i].GetChild(0);

                LFirstJoint[i] = LeftWrist.GetChild(1).GetChild(i - 1);
                LSecondJoint[i] = LFirstJoint[i].GetChild(0);
                LThirdJoint[i] = LSecondJoint[i].GetChild(0);
            }
            RFirstCollider[i] = RFirstJoint[i].GetComponent<Collider>();
            RSecondCollider[i] = RSecondJoint[i].GetComponent<Collider>();

            LFirstCollider[i] = LFirstJoint[i].GetComponent<Collider>();
            LSecondCollider[i] = LSecondJoint[i].GetComponent<Collider>();

            RoriginRot[i] = RFirstJoint[i].localEulerAngles;
            LoriginRot[i] = LFirstJoint[i].localEulerAngles;
        }
    }

    void Update()
    {
        if (serialManager.leftConnected)
            LbentDegree = serialManager.leftHandData;

        if (serialManager.rightConnected)
            RbentDegree = serialManager.rightHandData;

        for (int i = 0; i < 5; i++)
            LbentData[i] = (LbentDegree[i] - LhandFist[i]) * (90f / LhandRange[i]);

        LFirstJoint[0].localEulerAngles = LoriginRot[0] + new Vector3(0, (90 - LbentData[0]), 0);
        LSecondJoint[0].localEulerAngles = new Vector3(-(90 - LbentData[0]) * 0.8f, 0, 0);

        for (int i = 1; i < 5; i++)
        {
            LFirstJoint[i].localEulerAngles = LoriginRot[i] + new Vector3(-(90 - LbentData[i]), 0, 0);
            LSecondJoint[i].localEulerAngles = new Vector3(-(90 - LbentData[i]) * 0.85f, 0, 0);
            LThirdJoint[i].localEulerAngles = new Vector3(-(90 - LbentData[i]) * 0.73f, 0, 0);
        }

        for (int i = 0; i < 5; i++)
            RbentData[i] = (RbentDegree[i] - RhandFist[i]) * (90f / RhandRange[i]);

        RFirstJoint[0].localEulerAngles = RoriginRot[0] + new Vector3(0, (90 - RbentData[0]), 0);
        RSecondJoint[0].localEulerAngles = new Vector3(-(90 - RbentData[0]) * 0.8f, 0, 0);

        for (int i = 1; i < 5; i++)
        {
            RFirstJoint[i].localEulerAngles = RoriginRot[i] + new Vector3(-(90-RbentData[i]), 0, 0);
            RSecondJoint[i].localEulerAngles = new Vector3(-(90 - RbentData[i]) * 0.85f, 0, 0);
            RThirdJoint[i].localEulerAngles = new Vector3(-(90 - RbentData[i]) * 0.73f, 0, 0);
        }
    }

    public void RFingerOn(int index)
    {
        RFirstCollider[index].enabled = true;
        RSecondCollider[index].enabled = true;
    }
    public void RFingerOff(int index)
    {
        RFirstCollider[index].enabled = false;
        RSecondCollider[index].enabled = false;
    }

    public void LFingerOn(int index)
    {
        LFirstCollider[index].enabled = true;
        LSecondCollider[index].enabled = true;
    }
    public void LFingerOff(int index)
    {
        LFirstCollider[index].enabled = false;
        LSecondCollider[index].enabled = false;
    }
}
