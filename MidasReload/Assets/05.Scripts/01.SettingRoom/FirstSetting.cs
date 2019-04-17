using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FirstSetting : MonoBehaviour {

    [Header("[Finger UI]")]
    public Transform[] leftFingerUI = new Transform[5];
    public Transform[] rightFingerUI = new Transform[5];
    public Text[] leftFingerText = new Text[5];
    public Text[] rightFingerText = new Text[5];

    [Header("[UI]")]
    public Text DirText;
    public Text DirShadowText;
    public Image TimerImg;

    [Header("[Hand Data]")]
    float[] LFistdegree = new float[5] { 90, 90, 90, 90, 90 };
    float[] RFistdegree = new float[5] { 90, 90, 90, 90, 90 };
    float[] LStretchdegree = new float[5];
    float[] RStretchdegree = new float[5];

    [Header("[Panels]")]
    public GameObject SettingPanel;
    public TextMesh SettingScreenText;
    public GameObject HandsPanel;
    public GameObject DirPanel;
    public TextMesh DirScreenText;

    public FingerMove fingerMove;
    SerialManager serialManager;

    private void Start()
    {
        Transform RHandPos = GameObject.Find("RHandPos").transform;
        Transform LHandPos = GameObject.Find("LHandPos").transform;
        for (int i = 0; i < 5; i++)
        {
            leftFingerUI[i] = LHandPos.GetChild(i).GetChild(0).GetChild(0);
            rightFingerUI[i] = RHandPos.GetChild(i).GetChild(0).GetChild(0);

            leftFingerText[i] = LHandPos.GetChild(i).GetChild(1).GetComponent<Text>();
            rightFingerText[i] = RHandPos.GetChild(i).GetChild(1).GetComponent<Text>();
        }
        serialManager = GetComponent<SerialManager>();
    }

    public void GoButton()
    {
        SettingPanel.SetActive(false);
        HandsPanel.SetActive(true);
    }

    public void HandGoButton()
    {
        HandsPanel.SetActive(false);
        StartCoroutine("SerialSetting");
    }

    IEnumerator SerialSetting()
    {
        yield return new WaitForSeconds(2f);
        DirScreenText.text = "설정이 진행 중입니다...";
        DirPanel.SetActive(true);

        yield return new WaitForSeconds(2f);
        DirText.text = "게임을 시작하기전 설정 초기화를 위해 다음 지시를 따라주시길 바랍니다";
        DirShadowText.text = "게임을 시작하기전 설정 초기화를 위해 다음 지시를 따라주시길 바랍니다";

        yield return new WaitForSeconds(3f);
        if (serialManager.leftHandOn)
        {
            if (serialManager.rightHandOn)
            {
                DirText.text = "양손을 3초간 주먹 쥐어주세요";
                DirShadowText.text = "양손을 3초간 주먹 쥐어주세요";
            }
            else
            {
                DirText.text = "왼손을 3초간 주먹 쥐어주세요";
                DirShadowText.text = "왼손을 3초간 주먹 쥐어주세요";
            }
        }
        else
        {
            DirText.text = "오른손을 3초간 주먹 쥐어주세요";
            DirShadowText.text = "오른손을 3초간 주먹 쥐어주세요";
        }

        yield return new WaitForSeconds(4f);
        StartCoroutine("FistSetting");

        yield return new WaitForSeconds(8f);
        if (serialManager.leftHandOn)
        {
            if (serialManager.rightHandOn)
            {
                DirText.text = "양손을 3초간 펴주세요";
                DirShadowText.text = "양손을 3초간 펴주세요";
            }
            else
            {
                DirText.text = "왼손을 3초간 펴주세요";
                DirShadowText.text = "왼손을 3초간 펴주세요";
            }
        }
        else
        {
            DirText.text = "오른손을 3초간 펴주세요";
            DirShadowText.text = "오른손을 3초간 펴주세요";
        }

        yield return new WaitForSeconds(4f);
        StartCoroutine("StretchSetting");

        yield return new WaitForSeconds(8f);
        DirText.text = "모든 설정이 끝났습니다";
        DirShadowText.text = "모든 설정이 끝났습니다";
        DirScreenText.text = "설정을 마치는 중입니다...";

        yield return new WaitForSeconds(4f);
        DirPanel.SetActive(false);
        SettingScreenText.text = "센서를 다시 설정하시겠습니까?";

        DirPanel.SetActive(false);
        SettingPanel.SetActive(true);
        DirText.text = "";
        DirShadowText.text = "";
    }

    IEnumerator FistSetting()
    {
        float nowTime = 0;
        float[] Ldegree = new float[5] { 200, 200, 200, 200, 200 };
        float[] Rdegree = new float[5] { 200, 200, 200, 200, 200 };

        while (nowTime < 3)
        {
            nowTime += Time.deltaTime;
            TimerImg.fillAmount = nowTime / 3;

            for (int i = 0; i < 5; i++)
            {
                if (serialManager.leftConnected)
                {
                    float data = serialManager.leftHandData[i];
                    if (Ldegree[i] > data)
                        Ldegree[i] = data;
                }
                if (serialManager.rightConnected)
                {
                    float data = serialManager.rightHandData[i];
                    if (Rdegree[i] > data)
                        Rdegree[i] = data;
                }
            }
            yield return new WaitForSeconds(0.01f);
        }
        DirText.text = "";
        DirShadowText.text = "";
        TimerImg.fillAmount = 0;

        for (int i = 0; i < 5; i++)
        {
            if (serialManager.leftConnected)
            {
                LFistdegree[i] = Ldegree[i];
                PlayerPrefs.SetFloat("LhandFist" + i, Ldegree[i]);
            }
            if (serialManager.rightConnected)
            {
                RFistdegree[i] = Rdegree[i];
                PlayerPrefs.SetFloat("RhandFist" + i, Rdegree[i]);
            }
        }
        fingerMove.FingerReset();
    }

    IEnumerator StretchSetting()
    {
        float nowTime = 0;
        float[] Ldegree = new float[5] { 0, 0, 0, 0, 0 };
        float[] Rdegree = new float[5] { 0, 0, 0, 0, 0 };

        while (nowTime < 3)
        {
            nowTime += Time.deltaTime;
            TimerImg.fillAmount = nowTime / 3;

            for (int i = 0; i < 5; i++)
            {
                if (serialManager.leftConnected)
                {
                    float data = serialManager.leftHandData[i];
                    if (Ldegree[i] < data)
                        Ldegree[i] = data;
                }
                if (serialManager.rightConnected)
                {
                    float data = serialManager.rightHandData[i];
                    if (Rdegree[i] < data)
                        Rdegree[i] = data;
                }
            }
            yield return new WaitForSeconds(0.01f);
        }
        DirText.text = "";
        DirShadowText.text = "";
        TimerImg.fillAmount = 0;

        for (int i = 0; i < 5; i++)
        {
            if (serialManager.leftConnected)
            {
                LStretchdegree[i] = Ldegree[i];
                PlayerPrefs.SetFloat("LhandStretch" + i, Ldegree[i]);
                PlayerPrefs.SetFloat("LhandRange" + i, LFistdegree[i] - LStretchdegree[i]);
            }
            if (serialManager.rightConnected)
            {
                RStretchdegree[i] = Rdegree[i];
                PlayerPrefs.SetFloat("RhandStretch" + i, Rdegree[i]);
                PlayerPrefs.SetFloat("RhandRange" + i, RStretchdegree[i] - RFistdegree[i]);
            }
        }
        fingerMove.FingerReset();
    }

    public void SkipButton()
    {
        gameObject.SetActive(false);
        serialManager.CloseSerial();

        SceneManager.LoadScene("MainScene");
    }
}
