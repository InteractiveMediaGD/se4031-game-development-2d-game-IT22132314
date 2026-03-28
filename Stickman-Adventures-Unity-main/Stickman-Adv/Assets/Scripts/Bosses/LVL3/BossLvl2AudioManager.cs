using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossLvl2AudioManager : MonoBehaviour
{
    public AudioSource SwordSlash;
    public AudioSource FireballThrow;
    
    
    public void PlaySwordSlashSound()
    {
        SwordSlash.Play();
    }

    public void PlayFireballThrowSound()
    {
        FireballThrow.Play();
    }
}
