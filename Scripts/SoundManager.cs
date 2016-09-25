using UnityEngine;
using System.Collections;

public class SoundManager : MonoBehaviour
{
    private AudioSource[] audios;

    private void Awake()
    {
        audios = GetComponents<AudioSource>();
    }

    private void Start()
    {
        audios[0].Play();
    }
	
    private void Update()
    {
        if (!audios[0].isPlaying)
        {
            if (!audios[1].isPlaying)
            {
                audios[1].Play();
            }
                  
        }
    }
}
