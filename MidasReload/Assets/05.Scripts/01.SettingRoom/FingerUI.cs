using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FingerUI : MonoBehaviour {

    [Header("[Finger UI]")]
    public Transform[] LfingerUI = new Transform[5];
    public Transform[] RfingerUI = new Transform[5];
    public Text[] LfingerText = new Text[5];
    public Text[] RfingerText = new Text[5];
    public Text RPortText, LPortText;

    float MaxSensor = 250;

    public SerialManager serialManager;

    void Start()
    {
        for (int i = 0; i < 5; i++)
        {
            LfingerUI[i] = GameObject.Find("LHandPos").transform.GetChild(i).GetChild(0).GetChild(0);
            RfingerUI[i] = GameObject.Find("RHandPos").transform.GetChild(i).GetChild(0).GetChild(0);

            LfingerText[i] = GameObject.Find("LHandPos").transform.GetChild(i).GetChild(1).GetComponent<Text>();
            RfingerText[i] = GameObject.Find("RHandPos").transform.GetChild(i).GetChild(1).GetComponent<Text>();
        }
    }

	void Update () {
        if (serialManager.leftConnected)
        {
            float[] temp = serialManager.leftHandData;
            
            for (int i = 0; i < 5; i++)
            {
                LfingerUI[i].localPosition = new Vector3(0, -200 + (temp[i] * (200f / MaxSensor)), 0);
                LfingerText[i].text = temp[i].ToString();
            }
        }
        if (serialManager.rightConnected)
        {
            float[] temp = serialManager.rightHandData;

            for (int i = 0; i < 5; i++)
            {
                RfingerUI[i].localPosition = new Vector3(0, -200 + (temp[i] * (200f / MaxSensor)), 0);
                RfingerText[i].text = temp[i].ToString();
            }
        }
    }
}
