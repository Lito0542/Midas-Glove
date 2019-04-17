using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandsPanel : MonoBehaviour {
    
    GameObject RPortUp, LPortUp,RPortDown, LPortDown;
    public TextMesh RPortTextMesh, LPortTextMesh;
    Renderer RActiveRenderer,LActiveRenderer, RActiveEdge, LActiveEdge;

    public TextMesh RConnectTextMesh, LConnectTextMesh;

    public Material OnMaterial, OffMaterial;
    public Material OnEdge, OffEdge;

    public GameObject GoButton;
    public SerialManager serialManager;

    private void Awake()
    {
        RPortUp = GameObject.Find("RightHandPanel").transform.GetChild(4).gameObject;
        RPortDown = GameObject.Find("RightHandPanel").transform.GetChild(5).gameObject;

        LPortUp = GameObject.Find("LeftHandPanel").transform.GetChild(4).gameObject;
        LPortDown = GameObject.Find("LeftHandPanel").transform.GetChild(5).gameObject;

        RActiveRenderer
            = GameObject.Find("RightHandPanel").transform.GetChild(1).GetChild(0).GetComponent<Renderer>();
        LActiveRenderer
            = GameObject.Find("LeftHandPanel").transform.GetChild(1).GetChild(0).GetComponent<Renderer>();

        RActiveEdge
            = GameObject.Find("RightHandPanel").transform.GetChild(1).GetChild(0).GetChild(0).GetComponent<Renderer>();
        LActiveEdge
            = GameObject.Find("LeftHandPanel").transform.GetChild(1).GetChild(0).GetChild(0).GetComponent<Renderer>();
    }

    private void Start()
    {
        if (PlayerPrefs.GetInt("leftHandOn", 0) == 1)
        {
            LActiveRenderer.material = OnMaterial;
            LActiveEdge.material = OnEdge;
        }
        else
        {
            LActiveRenderer.material = OffMaterial;
            LActiveEdge.material = OffEdge;
        }

        if (PlayerPrefs.GetInt("rightHandOn", 0) == 1)
        {
            RActiveRenderer.material = OnMaterial;
            RActiveEdge.material = OnEdge;
        }
        else
        {
            RActiveRenderer.material = OffMaterial;
            RActiveEdge.material = OffEdge;
        }

        {
            int nowRport = serialManager.nowRPort;
            RPortTextMesh.text = "COM" + nowRport;
            if (nowRport <= 1)
                RPortDown.SetActive(false);
            else
                RPortDown.SetActive(true);

            if (nowRport >= 20)
                RPortUp.SetActive(false);
            else
                RPortUp.SetActive(true);
        } //우포트 설정

        {
            int nowLport = serialManager.nowLPort;
            LPortTextMesh.text = "COM" + nowLport;
            if (nowLport <= 1)
                LPortDown.SetActive(false);
            else
                LPortDown.SetActive(true);

            if (nowLport >= 20)
                LPortUp.SetActive(false);
            else
                LPortUp.SetActive(true);
        } //좌포트 설정
    }

    void Update ()
    {
        if (serialManager.leftConnected)
            LConnectTextMesh.text = "Connected";
        else
            LConnectTextMesh.text = "DisConnected";
        if (serialManager.rightConnected)
            RConnectTextMesh.text = "Connected";
        else
            RConnectTextMesh.text = "DisConnected";

        if ((serialManager.leftHandOn && serialManager.leftConnected && !serialManager.rightHandOn)
            || (serialManager.rightHandOn && serialManager.rightConnected && !serialManager.leftHandOn)
            || (serialManager.leftConnected && serialManager.rightConnected))
        {
            GoButton.SetActive(true);
        }
        else
            GoButton.SetActive(false);
    }


    public void RPortPlus(int num)
    {
        serialManager.RightPortSet(serialManager.nowRPort += num);
        RPortTextMesh.text = "COM" + serialManager.nowRPort;

        if (serialManager.nowRPort <= 1)
            RPortDown.SetActive(false);
        else
            RPortDown.SetActive(true);

        if (serialManager.nowRPort >= 25)
            RPortUp.SetActive(false);
        else
            RPortUp.SetActive(true);
    }

    public void LPortPlus(int num)
    {
        serialManager.LeftPortSet(serialManager.nowLPort += num);
        LPortTextMesh.text = "COM" + serialManager.nowLPort;

        if (serialManager.nowLPort <= 1)
            LPortDown.SetActive(false);
        else
            LPortDown.SetActive(true);

        if (serialManager.nowLPort >= 25)
            LPortUp.SetActive(false);
        else
            LPortUp.SetActive(true);
    }

    public void RPortActvie()
    {
        if (serialManager.rightHandOn)
        {
            serialManager.RightHandActive(false);
            RActiveRenderer.material = OffMaterial;
            RActiveEdge.material = OffEdge;
        }
        else
        {
            serialManager.RightHandActive(true);
            RActiveRenderer.material = OnMaterial;
            RActiveEdge.material = OnEdge;
        }
    }

    public void LPortActvie()
    {
        if (serialManager.leftHandOn)
        {
            serialManager.LeftHandActive(false);
            LActiveRenderer.material = OffMaterial;
            LActiveEdge.material = OffEdge;
        }
        else
        {
            serialManager.LeftHandActive(true);
            LActiveRenderer.material = OnMaterial;
            LActiveEdge.material = OnEdge;
        }
    }
}
