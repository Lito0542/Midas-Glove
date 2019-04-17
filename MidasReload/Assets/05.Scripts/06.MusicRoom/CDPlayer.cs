using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CDPlayer : MonoBehaviour
{
    public AudioClip[] clips = new AudioClip[3];
    public string[] clipName = new string[3];

    public GameObject nowPlayCD;
    public bool nowPlay = false;
    bool cdEnd = true;

    public Music_Visualizer musicVisualizer;
    public AudioCtrl audioCtrl;
    public AudioPlayCtrl audioPlayCtrl;

    Transform transform_;

    private void Awake()
    {
        transform_ = transform;
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("CD") && !nowPlay && cdEnd)
        {
            for (int i = 0; i < clipName.Length; i++)
            {
                if (collision.transform.name == "CD_" + clipName[i])
                {
                    nowPlayCD = collision.gameObject;

                    musicVisualizer.audio_.clip = clips[i];
                    musicVisualizer.audio_.Play();
                    audioPlayCtrl.MusicStart();

                    musicVisualizer.VisualizerActive(true);
                    nowPlay = true;
                    cdEnd = false;
                    break;
                }
            }
        }
    }

    public void OnCollisionExit(Collision collision)
    {
        if (collision.transform.CompareTag("CD") && collision.gameObject == nowPlayCD)
        {
            nowPlayCD = null;
            musicVisualizer.audio_.Stop();
            musicVisualizer.audio_.clip = null;

            musicVisualizer.VisualizerActive(false);
            nowPlay = false;
            cdEnd = true;
        }
    }

    private void Update()
    {
        if (nowPlay)
        {
            transform_.Rotate(0, 0.05f, 0);
            nowPlayCD.transform.Rotate(0, 0.05f, 0);
        }
    }

    public void CdEnd()
    {
        musicVisualizer.audio_.Stop();
        musicVisualizer.audio_.clip = null;

        musicVisualizer.VisualizerActive(false);
        nowPlay = false;
    }
}
