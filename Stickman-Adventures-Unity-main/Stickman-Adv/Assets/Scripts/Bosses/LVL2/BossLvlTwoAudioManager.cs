using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossLvlTwoAudioManager : MonoBehaviour
{
    public AudioSource SwordSlash;
    public AudioSource ShurikenThrow;
    
    
    public void PlaySwordSlashSound()
    {
        SwordSlash.Play();
    }

    public void PlayShurikenThrowSound()
    {
        ShurikenThrow.Play();
    }

}
