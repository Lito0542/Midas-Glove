using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PianoCtrl : MonoBehaviour {

    public AudioSource audio_;
    public AudioClip[] clips = new AudioClip[8];

    public void LowKeyPlay(int index)
    {
        audio_.pitch = 0.5f;
        audio_.PlayOneShot(clips[index]);
    }
    public void HighKeyPlay(int index)
    {
        audio_.pitch = 1f;
        audio_.PlayOneShot(clips[index]);
    }
    public void BlackLowKeyPlay(int index)
    {
        audio_.pitch = 1.5f;
        audio_.PlayOneShot(clips[index]);
    }
    public void BlackHightKeyPlay(int index)
    {
        audio_.pitch = 2f;
        audio_.PlayOneShot(clips[index]);
    }
}
