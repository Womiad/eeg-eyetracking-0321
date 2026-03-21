using UnityEngine;
using System.IO;
using UnityEngine.Video;

public class SceneAudioRecorder : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public AudioSource audioSource;

    void Start(){
        if (videoPlayer == null || audioSource == null)
        {
            Debug.LogError("VideoPlayer or AudioSource is not assigned!");
            return;
        }

        videoPlayer.audioOutputMode = VideoAudioOutputMode.AudioSource;
        videoPlayer.SetTargetAudioSource(0, audioSource);

        audioSource.mute = false;
        audioSource.volume = 1.0f;

        audioSource.Play();
    }
}
