using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class CubeManager : MonoBehaviour {

    public GameObject[] CubeObj = new GameObject[10];
    public Transform CubesParent;

    public SerialManager serialManager;
    private void Start()
    {
        StartCoroutine("ISummonCube");
    }

    public void SummonCube()
    {
        StartCoroutine("ISummonCube");
    }

    IEnumerator ISummonCube()
    {
        for (int i = 0; i < 30; i++)
        {
            GameObject newCube = Instantiate(CubeObj[Random.Range(0, CubeObj.Length)],
                CubesParent.position + new Vector3(Random.Range(-0.3f, 0.3f), 0, Random.Range(-0.3f, 0.3f)),
                Quaternion.Euler(0, Random.Range(0, 180), 0));
            newCube.transform.SetParent(CubesParent);
            yield return new WaitForSeconds(0.02f);
        }
    }

    public void DeleteCube()
    {
        for (int i = 0; i < CubesParent.childCount; i++)
        {
            Destroy(CubesParent.GetChild(i).gameObject);
        }
    }

    public void MenuButton()
    {
        serialManager.CloseSerial();
        SceneManager.LoadScene("MainScene");
    }
}
