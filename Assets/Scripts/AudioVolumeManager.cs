using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioVolumeManager : MonoBehaviour
{
    private AudioVolumeControler[] audios;
    public float maxVolumeLevel;
    public float currentVolumeLevel;

    // Start is called before the first frame update
    void Start()
    {
        audios = FindObjectsOfType<AudioVolumeControler>();
        ChangeGlobalAudioVolume();
    }

    public void ChangeGlobalAudioVolume()
    {
        if (currentVolumeLevel >= maxVolumeLevel)
        {
            currentVolumeLevel = maxVolumeLevel;
        }

        if (currentVolumeLevel <= 0)
        {
            currentVolumeLevel = 0;
        }

        foreach(AudioVolumeControler avc in audios)
        {
            avc.SetAudioLevel(currentVolumeLevel);
        }

    }

    // Update is called once per frame
    void Update()
    {
        ChangeGlobalAudioVolume();
    }
}
