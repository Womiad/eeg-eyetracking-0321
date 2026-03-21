using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System.Diagnostics;
using System.Threading;
using UnityEngine.UI;




public class VideoSceneManager : MonoBehaviour
{
    private bool English = false;
    public string nextSceneName = "SAM";
    public string endSceneName = "_Ending";

    public TMP_Text TMP_Trial;
    public float delayTime = 2f;
    

    public float trialFadeInTime = 1f;
    public float trialFadeOutTime = 1f;
    public float trialDisplayTime = 3f;
    public float videoFadeInTime = 5f;
    public float videoFadeOutTime = 5f;
    public float videoDisplayTime = 70f;
    int now_video;

    public int maxVideoNum = 8;

    public GameObject record_cam;

    public GameObject FadeUI;

    public bool isExample = true;

    Color TransparentWhite = new Color(1, 1, 1, 0);
    Coroutine COR_PLAYVIDEOROUTINE = null;

    private Thread convert_video;

    public GameObject progress_bar;

    private RawImage progress_img;

    // Start is called before the first frame update
    void Start()
    {
        if(PlayerPrefs.GetInt("English") == 1){
            English = true;
            nextSceneName = "SAM -eng";
            endSceneName = "_Ending -eng";
        }
        TMP_Trial.text = "";
        StartCoroutine(DelayStart(delayTime));
        
        record_cam.GetComponent<uNvEncoder.Examples.TextureEncoder>().enabled = false; // UTJ.FrameCapturer.MovieRecorder uNvEncoder.Examples.TextureEncoder
        record_cam.GetComponent<uNvEncoder.Examples.OutputEncodedDataToFile>().enabled = false; // uNvEncoder.Examples.OutputEncodedDataToFile
        progress_img = progress_bar.GetComponent<RawImage>();
        progress_img.rectTransform.sizeDelta = new Vector2(0,3f);
    }

    IEnumerator DelayStart(float time)
    {
        yield return new WaitForSeconds(time);
        COR_PLAYVIDEOROUTINE = StartCoroutine(PlayVideoRoutine());
    }

    IEnumerator TrialDisplayRoutine()
    {
        StartCoroutine(ProgressBar(trialDisplayTime+trialFadeOutTime));

        // Setting
        TMP_Trial.color = TransparentWhite;
        if (isExample){
            TMP_Trial.text = "範例影片";
        }else{
            now_video = (int)(SkyboxVideoPlayerController.instance.Index + 1);
            TMP_Trial.text = "第 " + (now_video).ToString() + "部影片";
            if(English) TMP_Trial.text = "Video " + (now_video).ToString();
            if(now_video == maxVideoNum) SkyboxVideoPlayerController.instance.VideoEnd = true;
        }
        

        // Fade in
        float timeElapsed = 0;
        while (timeElapsed < trialFadeInTime)
        {
            TMP_Trial.color = Color.Lerp(TransparentWhite, Color.white, timeElapsed / trialFadeInTime);
            timeElapsed += Time.deltaTime;
            yield return null;
        }
        TMP_Trial.color = Color.white;



        // Display
        yield return new WaitForSeconds(trialDisplayTime);




        // Fade out
        timeElapsed = 0;
        while (timeElapsed < trialFadeOutTime)
        {
            TMP_Trial.color = Color.Lerp(Color.white, TransparentWhite, timeElapsed / trialFadeOutTime);
            timeElapsed += Time.deltaTime;
            yield return null;
        }
        // FadeUI.SetActive(true);
        TMP_Trial.color = TransparentWhite;
    }

    IEnumerator PlayVideoRoutine()
    {
        //Start
        if (!SkyboxVideoPlayerController.instance.IsFinished && !SkyboxVideoPlayerController.instance.VideoEnd)
        {
            // Prepare video
            SkyboxVideoPlayerController.instance.PrepareVideo(videoFadeInTime + videoDisplayTime + videoFadeOutTime);
            // Display trial number
            yield return StartCoroutine(TrialDisplayRoutine());


            // Play video
            // yield return StartCoroutine(SkyboxVideoPlayerController.instance.PlayVideoForSecondsAndMoveToNext(videoFadeInTime, videoFadeOutTime, videoDisplayTime));
            //叫他開始播
            StartCoroutine(SkyboxVideoPlayerController.instance.PlayVideoForSecondsAndMoveToNext(videoFadeInTime, videoFadeOutTime, videoDisplayTime));
           
            //這邊是紀錄用
            yield return new WaitForSeconds(videoFadeInTime);
            // Handle video start csv record
            if (!isExample){
                HandleCSV_VideoStart(SkyboxVideoPlayerController.instance.Clip);
            }
            yield return new WaitForSeconds(videoDisplayTime);
            // // Handle video End csv record
            if (!isExample){
                HandleCSV_VideoEnd();
            }
            yield return new WaitForSeconds(videoFadeOutTime);
                
            // Change SAM Scene
            SceneManager.LoadScene(nextSceneName);
        }
        else
        {
            SceneManager.LoadScene(endSceneName);
        }

        COR_PLAYVIDEOROUTINE = null;
    }

    private void HandleCSV_VideoStart(EmotionalVideoData emotionalVideo)
    {
        if (!isExample){
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
        }else{
            return;
        }
    }

    private void HandleCSV_VideoEnd()
    {
        if (!isExample){
            VideoCSVHandler.instance.WriteRecord("end_time", VideoCSVHandler.instance.GetDateTime());
            //結束錄影
            record_cam.GetComponent<uNvEncoder.Examples.TextureEncoder>().enabled = false; // UTJ.FrameCapturer.MovieRecorder uNvEncoder.Examples.TextureEncoder
            record_cam.GetComponent<uNvEncoder.Examples.OutputEncodedDataToFile>().enabled = false; // uNvEncoder.Examples.OutputEncodedDataToFile
            record_cam.GetComponent<OutputAudioRecorder>().StopRecording();
        }else{
            return;
        }
    }

    IEnumerator ProgressBar(float progress_time){
        progress_bar.SetActive(true);
        for (float i = progress_time; i > 0 ; i -= progress_time/150){
            float bar_width = 180 * i/progress_time;
            progress_img.rectTransform.sizeDelta = new Vector2(bar_width, 3f);
            yield return new WaitForSecondsRealtime(progress_time/150);
        }
        progress_img.rectTransform.sizeDelta = new Vector2(0, 3);
        progress_bar.SetActive(false);
    }

}
