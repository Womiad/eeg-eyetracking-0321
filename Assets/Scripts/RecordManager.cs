using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecordManager : MonoBehaviour
{
    public static RecordManager instance;
    public GameObject record_cam;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(this.gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(this.gameObject);
    }

    public void HandleCSV_VideoStart(EmotionalVideoData emotionalVideo)
    {
        VideoCSVHandler.instance.WriteRecord("number", emotionalVideo.number.ToString());
        VideoCSVHandler.instance.WriteRecord("title", emotionalVideo.title);
        VideoCSVHandler.instance.WriteRecord("type", emotionalVideo.type.ToString());
        VideoCSVHandler.instance.WriteRecord("ground_truth_valence", emotionalVideo.groundTruth.valence.ToString());
        VideoCSVHandler.instance.WriteRecord("ground_truth_arousal", emotionalVideo.groundTruth.arousal.ToString());
        VideoCSVHandler.instance.WriteRecord("start_time", VideoCSVHandler.instance.GetDateTime());
        //開始錄影
        record_cam.GetComponent<uNvEncoder.Examples.TextureEncoder>().enabled = true; // UTJ.FrameCapturer.MovieRecorder uNvEncoder.Examples.TextureEncoder
        record_cam.GetComponent<uNvEncoder.Examples.OutputEncodedDataToFile>().enabled = true; // uNvEncoder.Examples.OutputEncodedDataToFile
        record_cam.GetComponent<OutputAudioRecorder>().StartRecording();
    }

    public void HandleCSV_VideoEnd()
    {
        VideoCSVHandler.instance.WriteRecord("end_time", VideoCSVHandler.instance.GetDateTime());
        //結束錄影
        record_cam.GetComponent<uNvEncoder.Examples.TextureEncoder>().enabled = false; // UTJ.FrameCapturer.MovieRecorder uNvEncoder.Examples.TextureEncoder
        record_cam.GetComponent<uNvEncoder.Examples.OutputEncodedDataToFile>().enabled = false; // uNvEncoder.Examples.OutputEncodedDataToFile
        record_cam.GetComponent<OutputAudioRecorder>().StopRecording();
    }


}
