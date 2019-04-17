using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioCtrl : MonoBehaviour {

    public AudioSource audio_;
    public AudioChorusFilter audioChorus;

    [Header("[Information]")]
    public float volume = 0.3f;
    public float pitch = 1f;
    public float stereoPan = 0f;

    [Header("[Chorus Panel]")]
    bool chorusOn = false;
    public GameObject AudioSourceUI;
    public Transform AudioChorusPanel;
    public Transform AudioChorusSlot;
    public Transform SlotArrow;

    public CDPlayer cdPlayer;


    public void ChorusPanel_Slot()
    {
        chorusOn = !chorusOn;
    }

    public IEnumerator ChorusUp()
    {
        yield return new WaitForSeconds(0.001f);
    }
    public IEnumerator ChorusDown()
    {
        yield return new WaitForSeconds(0.001f);
    }
}
