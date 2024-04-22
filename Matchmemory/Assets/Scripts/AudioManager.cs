using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioClip[] audioClips_Bgms = null;

    [SerializeField] private AudioClip positiveFeedback = null;
    [SerializeField] private AudioClip negativeFeedback = null;
    [SerializeField] private AudioClip LevelCompleted = null;

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

    /// <summary>
    /// 0 - tile matched feedback
    /// 1 - tile unmatched feedback
    /// 2 - Level completed feedback
    /// </summary>
    /// <param name="status"></param>
    public void PlayFeedbackAudio(int status) 
    {
        switch(status)
        {
            case 0:
                audioSource.PlayOneShot(positiveFeedback, feedbackaudioScaler);
                break; 
            case 1:
                audioSource.PlayOneShot(negativeFeedback, feedbackaudioScaler);
                break;
            case 2:
                audioSource.PlayOneShot(LevelCompleted, feedbackaudioScaler);
                break;
            default: break;
        }

        
    }
}
