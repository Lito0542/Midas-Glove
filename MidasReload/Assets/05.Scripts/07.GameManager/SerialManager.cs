using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;

public class SerialManager : MonoBehaviour {

    public bool leftHandOn = false;
    public bool rightHandOn = false;

    [Header("[Serial]")]
    SerialPort leftSerial;
    SerialPort rightSerial;

    public int nowLPort = 1;
    public int nowRPort = 1;

    public bool leftConnected = false;
    public bool rightConnected = false;

    [Header("[Data]")]
    public float[] leftHandData = new float[5];
    public float[] rightHandData = new float[5];

    void Awake()
    {
        if (PlayerPrefs.GetInt("leftHandOn", 0) == 1)
            leftHandOn = true;
        else
            leftHandOn = false;

        if (PlayerPrefs.GetInt("rightHandOn", 0) == 1)
            rightHandOn = true;
        else
            rightHandOn = false;

        nowLPort = PlayerPrefs.GetInt("nowLPort", 1);
        nowRPort = PlayerPrefs.GetInt("nowRPort", 1);
    }

    private void Start()
    {
        StartCoroutine("SerialUpdate");
    }

    IEnumerator SerialUpdate()
    {
        while (true)
        {
            if (leftHandOn && !leftConnected)
            {
                try
                {
                    leftSerial = new SerialPort("\\\\.\\COM" + nowLPort, 9600);
                    leftSerial.Open();
                }
                catch { }
            }
            if (rightHandOn && !rightConnected)
            {
                try
                {
                    rightSerial = new SerialPort("\\\\.\\COM" + nowRPort, 9600);
                    rightSerial.Open();
                }
                catch { }
            }

            if (leftHandOn)
            {
                if (leftSerial.IsOpen)
                    leftConnected = true;
                else
                    leftConnected = false;
            }
            else
                leftConnected = false;

            if (rightHandOn)
            {
                if (rightSerial.IsOpen)
                    rightConnected = true;
                else
                    rightConnected = false;
            }
            else
                rightConnected = false;

            if (leftConnected)
            {
                string[] data = leftSerial.ReadLine().Split(',');
                for (int i = 0; i < 5; i++)
                {
                    try
                    {
                        leftHandData[i] = int.Parse(data[i]);
                    }
                    catch { }
                }
            }
            if (rightConnected)
            {
                string[] data = rightSerial.ReadLine().Split(',');
                for (int i = 0; i < 5; i++)
                {
                    try
                    {
                        rightHandData[i] = int.Parse(data[i]);
                    }
                    catch {  }
                }
            }
            yield return new WaitForSeconds(0.025f);
        }
    }

    public void LeftHandActive(bool on)
    {
        leftHandOn = on;
        if (leftHandOn)
            PlayerPrefs.SetInt("leftHandOn", 1);
        else
            PlayerPrefs.SetInt("leftHandOn", 0);
    }

    public void RightHandActive(bool on)
    {
        rightHandOn = on;
        if (rightHandOn)
            PlayerPrefs.SetInt("rightHandOn", 1);
        else
            PlayerPrefs.SetInt("rightHandOn", 0);
    }

    public void RightPortSet(int port)
    {
        int temp = nowRPort;
        nowRPort = port;
        PlayerPrefs.SetInt("nowRPort", port);

        if(nowRPort != temp && rightConnected)
        {
            rightConnected = false;
        }
    }

    public void LeftPortSet(int port)
    {
        int temp = nowLPort;
        nowLPort = port;
        PlayerPrefs.SetInt("nowLPort", port);

        if (nowLPort != temp && leftConnected)
        {
            leftConnected = false;
        }
    }

    private void OnApplicationQuit()
    {
        CloseSerial();
    }

    public void CloseSerial()
    {
        if (leftSerial != null)
            leftSerial.Close();
        if (rightSerial != null)
            rightSerial.Close();
    }
}
