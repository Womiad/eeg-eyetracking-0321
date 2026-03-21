using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using TMPro;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(VideoPlayer))]
public class SkyboxVideoPlayerController : MonoBehaviour
{
    public static SkyboxVideoPlayerController instance;

    VideoPlayer videoPlayer;
    RenderTexture renderTexture;
    public Material material;
    public bool shouldDontDestroy = true;
    // public GameObject hint_object;
    // public GameObject command_text;
    bool last_20sec = false;
    public int record_number = 0;
    // public int video_number;
    string[] command = new string[] {"請舉起你的雙手" , "請左右搖頭" , "請舉起右手" , "請不要眨眼直到此提示消失" , "請在此提示發亮時眨眼"};
    string[] command_code = new string[] {"HandsUp", "Wagging", "RightHand", "NoBlink", "Blink"}; // Handsup, Wagging, Object, NoBlink, Blink
    public ArrayList video_number = new ArrayList();
    public ArrayList command_time = new ArrayList();
    public ArrayList command_end_time = new ArrayList();
    public ArrayList executed_command = new ArrayList();
    public ArrayList last20_command = new ArrayList();

    [SerializeField]
    List<EmotionalVideoData> videoSources;
    List<EmotionalVideoData> videoClips = new List<EmotionalVideoData>();
    float timer = 0;
    
    int clipIndex = 0;
    public bool VideoEnd = false;

    public float Index
    {
        get
        {
            return clipIndex;
        }
    }

    public EmotionalVideoData Clip
    {
        get
        {
            return videoClips[clipIndex];
        }
    }

    public bool IsFinished
    {
        get
        {
            return !(clipIndex < videoClips.Count);
        }
    }

    void Awake()
    {
        if (instance != null)
        {
            Destroy(this.gameObject);
            return;
        }
        instance = this;
        if (shouldDontDestroy){
            DontDestroyOnLoad(this.gameObject);
        }
            
    }

    // Start is called before the first frame update
    void Start()
    {
        videoPlayer = this.GetComponent<VideoPlayer>();
        renderTexture = videoPlayer.targetTexture;
        renderTexture.Release();
        Shuffle();
        timer += Time.deltaTime;
        // StartCoroutine(Display_Command(timer));
    }

    public IEnumerator Display_Command(float time){
        Debug.Log("command_StartCoroutine");
        CommandController.instance.SetTextActive(false);
        
        yield return new WaitForSeconds(60f);//等待60秒後，video剩30秒

        int L , random;
        L = command.Length;

        random = (int)UnityEngine.Random.Range( 0 , 2145677);//取得一個亂數
        int randomDisplayMoment = random % 2;//亂數選擇前15秒顯示或後15秒顯示

        random = (int)UnityEngine.Random.Range( 0 , 2145677);//取得一個亂數
        int randomDisplayTimes = random % 2;//亂數選擇顯示1次或2次

        if(randomDisplayMoment == 0){
            //前15秒顯示
            if(randomDisplayTimes == 0){
                //顯示一次
                CommandController.instance.SetTextActive(true);
                random = (int)UnityEngine.Random.Range( 0 , 2145677);//取得一個亂數
                int randomCommand = random % L;//取得亂數command
                CommandController.instance.UpdateText(command[randomCommand]);//設定顯示command
                yield return new WaitForSeconds(5f);//顯示5秒
                CommandController.instance.SetTextActive(false);//關閉

            }else{
                //顯示兩次
                CommandController.instance.SetTextActive(true);
                random = (int)UnityEngine.Random.Range( 0 , 2145677);//取得一個亂數
                int randomCommand = random % L;//取得亂數command
                CommandController.instance.UpdateText(command[randomCommand]);//設定顯示command
                yield return new WaitForSeconds(5f);//顯示5秒
                CommandController.instance.SetTextActive(false);//關閉
                yield return new WaitForSeconds(5f);//等待5秒
                CommandController.instance.SetTextActive(true);
                random = (int)UnityEngine.Random.Range( 0 , 2145677);//取得一個亂數
                randomCommand = random % L;//取得亂數command
                CommandController.instance.UpdateText(command[randomCommand]);//設定顯示command
                yield return new WaitForSeconds(5f);//顯示5秒
                CommandController.instance.SetTextActive(false);//關閉
            }
        }else{
            //後15秒顯示
            yield return new WaitForSeconds(15f);//15秒後再回來

            if(randomDisplayTimes == 0){
                //顯示一次
                CommandController.instance.SetTextActive(true);
                random = (int)UnityEngine.Random.Range( 0 , 2145677);//取得一個亂數
                int randomCommand = random % L;//取得亂數command
                CommandController.instance.UpdateText(command[randomCommand]);//設定顯示command
                yield return new WaitForSeconds(5f);//顯示5秒
                CommandController.instance.SetTextActive(false);//關閉
            }else{
                //顯示兩次
                CommandController.instance.SetTextActive(true);
                random = (int)UnityEngine.Random.Range( 0 , 2145677);//取得一個亂數
                int randomCommand = random % L;//取得亂數command
                CommandController.instance.UpdateText(command[randomCommand]);//設定顯示command
                yield return new WaitForSeconds(5f);//顯示5秒
                CommandController.instance.SetTextActive(false);//關閉
                yield return new WaitForSeconds(5f);//等待5秒
                CommandController.instance.SetTextActive(true);
                random = (int)UnityEngine.Random.Range( 0 , 2145677);//取得一個亂數
                randomCommand = random % L;//取得亂數command
                CommandController.instance.UpdateText(command[randomCommand]);//設定顯示command
                yield return new WaitForSeconds(5f);//顯示5秒
                CommandController.instance.SetTextActive(false);//關閉
            }
        }

        // int L , random;
        // TMP_Text T;
        // while (true) {
        //     // hint_object.SetActive(true);
        //     CommandController.instance.SetTextActive(true);
        //     L = command.Length;
        //     random = (int)UnityEngine.Random.Range( 0 , 2145677);
        //     random = random % L;
        //     // T = command_text.GetComponent<TMP_Text>();
        //     // T.text = command[random];
        //     
        //     int counter = 0;
        //     if (random == 5){ // Blink when hint light
        //         while (counter != 5){
        //             yield return new WaitForSeconds(5f);
        //             Debug.Log("Blink" + " " + counter +  " time");
        //             counter += 1;
        //         }
        //     }
        //     Debug.Log(random + " command = " + command[random]);

        //     video_number.Add(videoClips[clipIndex].number);
        //     if(SceneManager.GetActiveScene().name == "360 Video"){
        //         CommandCSVHandler.instance.WriteRecord("video_number",videoClips[clipIndex].number.ToString());
        //     }

        //     executed_command.Add(command_code[random]);
        //     if(SceneManager.GetActiveScene().name == "360 Video"){
        //         CommandCSVHandler.instance.WriteRecord("command",command_code[random]);
        //     }

        //     command_time.Add(VideoCSVHandler.instance.GetDateTime());
        //     if(SceneManager.GetActiveScene().name == "360 Video"){
        //         CommandCSVHandler.instance.WriteRecord("excuted_time",VideoCSVHandler.instance.GetDateTime());
        //     }

        //     last20_command.Add(last_20sec);
        //     foreach (string s in executed_command)
        //     {
        //         print("Executed command: " + s);
        //     }
            
        //     yield return new WaitForSeconds(5f);
        //     // hint_object.SetActive(false);
        //     CommandController.instance.SetTextActive(false);
        //     command_end_time.Add(VideoCSVHandler.instance.GetDateTime());

            
        //     if(SceneManager.GetActiveScene().name == "360 Video"){
        //         CommandCSVHandler.instance.WriteRecord("end_time",VideoCSVHandler.instance.GetDateTime());
        //         CommandCSVHandler.instance.WriteFile();
        //     }

        //     record_number++;
        //     yield return new WaitForSeconds(5f);

        // }
        
    }

    // Shuffle 將讀取的影片檔案按照規則重新排列並放置於videoSources: 1. 第一部影片是Netural type； 2.剩下的影片隨機排列但重複類型的不會連續出現
    void Shuffle()
    {
        // First one is always a netural video clip
        List<EmotionalVideoData> clips = new List<EmotionalVideoData>(videoSources);
        

        // Shuffle the rest
        bool isNoRepeatShuffled = false;
        List<EmotionalVideoData> shuffledClips = new List<EmotionalVideoData>(); ;
        while (!isNoRepeatShuffled)
        {
            isNoRepeatShuffled = true;
            shuffledClips = clips.OrderBy(i => Guid.NewGuid()).ToList();
            for (int i = 0; i < shuffledClips.Count - 1; i++)
            {
                if (shuffledClips[i].type == shuffledClips[i + 1].type)
                {
                    isNoRepeatShuffled = false;
                    break;
                }
            }
        }
        videoClips.AddRange(shuffledClips);
    }

    // videoPlayingTime 表示影片需要播放的時間
    // videoPlayer.frame 將隨機選擇並保證不會使影片播放超過其長度 (videoTotalTime > videoRandomStartTime + videoPlayingTime)
    public IEnumerator PlayVideoForSecondsAndMoveToNext(float fadeInTime, float fadeOutTime, float displayTime)
    {
        if (!IsFinished)
        {
            VideoClip targetClip = videoClips[clipIndex++].clip;
            renderTexture.Release();
            renderTexture.width = (int)targetClip.width;
            renderTexture.height = (int)targetClip.height;
            renderTexture.Create();
            while (!videoPlayer.isPrepared) { }
            videoPlayer.Play();
            yield return StartCoroutine(FadeInOutVideo(fadeInTime, 0, 1));

            yield return new WaitForSeconds(displayTime);

            // yield return new WaitForSeconds(Mathf.Abs(20 - displayTime));
            // last_20sec = true;
            // yield return new WaitForSeconds(20f);
            // last_20sec = false;
            yield return StartCoroutine(FadeInOutVideo(fadeOutTime, 1, 0));
            videoPlayer.Stop();
            renderTexture.Release();
            
        }
    }

    public IEnumerator FadeInOutVideo(float time, float startValue, float endValue)
    {
        // Setting
        material.SetFloat("_Exposure", startValue);
        videoPlayer.SetDirectAudioVolume(0, startValue);
        // Fade
        float timeElapsed = 0;
        while (timeElapsed < time)
        {
            float value = Mathf.Lerp(startValue, endValue, timeElapsed / time);
            material.SetFloat("_Exposure", value);
            videoPlayer.SetDirectAudioVolume(0, value);
            timeElapsed += Time.deltaTime;
            yield return null;
        }
        material.SetFloat("_Exposure", endValue);
        videoPlayer.SetDirectAudioVolume(0, endValue);
        Debug.Log(executed_command);
    }

    public void PrepareVideo(float videoPlayingTime)
    {
        videoPlayer.clip = videoClips[clipIndex].clip;
        float videoTotalTime = videoPlayer.clip.frameCount / (float)videoPlayer.clip.frameRate;

        // 計算 mask_start 和 mask_end 秒對應的幀數
        long maskStartFrame = (long)(videoClips[clipIndex].mask_start * videoPlayer.clip.frameRate);
        long maskEndFrame = (long)((videoTotalTime - (float)videoClips[clipIndex].mask_end) * videoPlayer.clip.frameRate);

        // 確保 videoPlayingTime 的長度不超過 mask_end，並顯式轉換為 long
        maskEndFrame = (long)Mathf.Max(maskStartFrame, maskEndFrame - (long)(videoPlayingTime * videoPlayer.clip.frameRate));

        // 設定影片起始幀在 mask_start 和 maskEndFrame 之間
        videoPlayer.frame = (long)UnityEngine.Random.Range(maskStartFrame, maskEndFrame);
        videoPlayer.Prepare();

        material.SetInt("_Rotation", videoClips[clipIndex].materialRotation);
    }
}