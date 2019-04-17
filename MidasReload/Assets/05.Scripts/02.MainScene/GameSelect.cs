using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSelect : MonoBehaviour
{
    public int nowIndex = 0;
    public Transform GameSelectScreens;

    public float rotSpeed = 5;
    public float nowRot = 0;

    public TextMesh SceneText;
    public string[] SceneNames = new string[5];

    public SerialManager serialManager;

    void Start()
    {
        GameSelectScreens.transform.eulerAngles = new Vector3(0, 0, 0);
        SceneText.text = SceneNames[nowIndex + 2];
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
            IndexUp();
        if (Input.GetKeyDown(KeyCode.D))
            IndexDown();

        if (nowRot < nowIndex * 60)
        {
            if(Mathf.Abs(nowRot - nowIndex * 60) < 5)
                GameSelectScreens.transform.eulerAngles = new Vector3(0, nowIndex * 60, 0);
            else
            {
                nowRot += rotSpeed * Time.deltaTime;
                GameSelectScreens.transform.eulerAngles = new Vector3(0, nowRot, 0);
            }
        }
        else if (nowRot > nowIndex * 60)
        {
            if (Mathf.Abs(nowRot - nowIndex * 60) < 5)
                GameSelectScreens.transform.eulerAngles = new Vector3(0, nowIndex * 60, 0);
            else
            {
                nowRot -= rotSpeed * Time.deltaTime;
                GameSelectScreens.transform.eulerAngles = new Vector3(0, nowRot, 0);
            }
        }
    }

    public void IndexDown()
    {
        if (nowIndex < 2)
            nowIndex++;
        SceneText.text = SceneNames[nowIndex + 2];
    }

    public void IndexUp()
    {
        if (nowIndex > -2)
            nowIndex--;
        SceneText.text = SceneNames[nowIndex + 2];
    }

    public void SelectGame()
    {
        serialManager.CloseSerial();
        SceneManager.LoadScene("InGame" + (nowIndex + 2));
    }
}
