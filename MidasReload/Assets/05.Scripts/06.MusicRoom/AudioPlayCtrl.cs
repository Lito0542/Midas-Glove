using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayCtrl : MonoBehaviour
{
    public AudioSource audio_;
    public CDPlayer cdPlayer;
    public Music_Visualizer musicVisualizer;

    [Header("[Mute]")]
    bool mute = false;
    public Renderer MuteRenderer;
    public Material muteOn;
    public Material muteOff;

    [Header("[Audio Time]")]
    public float nowTime = 0;
    public int maxTime = 0;
    public TextMesh nowTimeText;
    public TextMesh maxTimeText;

    [Header("[Audio UI]")]
    public Transform Handle;
    public Transform FillBar;
    public float percent = 0;

    [Header("[Pause]")]
    public Renderer PlayRenderer;
    public Material play;
    public Material pause;

    [Header("[Lerp]")]
    public Renderer LerpRenderer;
    [Header("[Stop]")]
    public Renderer StopRenderer;
    public Color OriginColor;
    public Color PressedColor;

    public UISliderCtrl volumeSlider;
    public UISliderCtrl pitchSlider;
    public UISliderCtrl stereoSlider;

    public void MusicStart()
    {
        maxTime = (int)audio_.clip.length;
        int min = (maxTime / 60);
        string sec =
            (maxTime % 60 > 10) ? (maxTime % 60).ToString() : "0" + (maxTime % 60).ToString();
        nowTime = 0;

        maxTimeText.text = "/" + min + ":" + sec;
    }
    public void MusicEnd()
    {
        maxTime = 0;
        nowTime = 0;
        nowTimeText.text = "0:00";
        maxTimeText.text = "/0:00";

        percent = 0;
        Handle.transform.localPosition = new Vector3(0.01f, 0.1f, 0.009f);
        FillBar.transform.localPosition = new Vector3(0.01f, 0.071f, 0);
        FillBar.transform.localScale = new Vector3(0, 0.035f, 0.05f);
    }

    public void MuteButton()
    {
        mute = !mute;
        audio_.mute = mute;
        MuteRenderer.material.color = PressedColor;
        Invoke("MuteOrigin", 0.1f);

        if (mute)
            MuteRenderer.material = muteOn;
        else
            MuteRenderer.material = muteOff;
    }

    public void PlayButton()
    {
        if (cdPlayer.nowPlay) //Pause!
        {
            audio_.Stop();
            musicVisualizer.VisualizerActive(false);
            cdPlayer.nowPlay = false;
            PlayRenderer.material = play;
        }
        else
        {
            audio_.time = nowTime;
            audio_.Play();
            musicVisualizer.VisualizerActive(true);
            cdPlayer.nowPlay = true;
            PlayRenderer.material = pause;
        }
    }

    public void LerpButton()
    {
        LerpRenderer.material.color = PressedColor;
        Invoke("LerpOrigin", 0.1f);
        nowTime += 10;
        if (nowTime > maxTime)
        {
            nowTime = maxTime;
            MusicEnd();
            cdPlayer.CdEnd();
        }
        audio_.Stop();
        audio_.time = nowTime;
        audio_.Play();
    }

    public void StopButton()
    {
        StopRenderer.material.color = PressedColor;
        Invoke("StopOrigin", 0.1f);
        MusicEnd();
        cdPlayer.CdEnd();
    }

    public void VolumeSet()
    {
        audio_.volume = volumeSlider.value;
    }
    public void PitchSet()
    {
        audio_.pitch = pitchSlider.value;
    }
    public void StereoSet()
    {
        audio_.panStereo = stereoSlider.value;
    }

    private void Update()
    {
        if (cdPlayer.nowPlay)
        {
            nowTime += Time.deltaTime;
            int min = (int)(nowTime / 60);
            string sec =
                (nowTime % 60 > 10) ? ((int)nowTime % 60).ToString() : "0" + ((int)nowTime % 60).ToString();
            nowTimeText.text = min + ":" + sec;

            percent = nowTime / maxTime;
            Handle.transform.localPosition = new Vector3(0.01f, 0.1f, -1.28f * percent + 0.009f);
            FillBar.transform.localPosition = new Vector3(0.01f, 0.071f, -0.65f * percent);
            FillBar.transform.localScale = new Vector3(1.3f * percent, 0.035f, 0.05f);

            if (nowTime > maxTime)
            {
                MusicEnd();
                cdPlayer.CdEnd();
            }
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            PlayButton();
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            StopButton();
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            LerpButton();
        }
        if (Input.GetKeyDown(KeyCode.M))
        {
            MuteButton();
        }
    }

    public void StopOrigin()
    {
        StopRenderer.material.color = OriginColor;
    }

    public void LerpOrigin()
    {
        LerpRenderer.material.color = OriginColor;
    }
    public void MuteOrigin()
    {
        MuteRenderer.material.color = OriginColor;
    }
}
