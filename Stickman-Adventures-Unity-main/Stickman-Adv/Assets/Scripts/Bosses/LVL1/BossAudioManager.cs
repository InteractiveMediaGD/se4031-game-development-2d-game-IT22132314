using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAudioManager : MonoBehaviour
{
    public AudioSource Earthquake;
    public AudioSource Rolling;
    public AudioSource PlayerWin;

    public void PlayEarthquakeSound()
    {
        Earthquake.Play();
    }

    public void PlayRollingSound()
    {
        Rolling.PlayDelayed(0.5f);
    }

    public void PlayPlayerWinSound()
    {
        PlayerWin.Play();
    }
}
