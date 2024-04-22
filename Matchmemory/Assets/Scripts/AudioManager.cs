using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioClip[] audioClips_Bgms = null;

    [SerializeField] private AudioClip positiveFeedback = null;
    [SerializeField] private AudioClip negativeFeedback = null;

    //private Slider feedbackaudioScaler = null;
    [SerializeField] private float feedbackaudioScaler;

    private AudioSource audioSource;
    private bool isPlaying = false;

    private void Awake()
    {
        InitializeAudioManager();
    }

    void InitializeAudioManager()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!audioSource.isPlaying)
        {
            audioSource.clip = GetRandomClip();
            audioSource.volume = 0.7f;
            audioSource.Play();
        }
    }

    private AudioClip GetRandomClip()
    {
        return audioClips_Bgms[Random.Range(0, audioClips_Bgms.Length)];
    }

    public void PlayFeedbackAudio(bool status)
    {
        if(status)
        {
            audioSource.PlayOneShot(positiveFeedback, feedbackaudioScaler);
        }
        else
        {
            audioSource.PlayOneShot(negativeFeedback, feedbackaudioScaler);
        }
    }
}
