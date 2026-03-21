using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SAMSceneManager : MonoBehaviour
{
    public static SAMSceneManager instance = null;

    public string nextSceneName = "360 Video";
    public string endSceneName = "_Ending";
    public bool test_mode = false;

    public float changeSceneDelay = 80f;
    public float changeSceneMinDelay = 20f;
    float start_timer = 0f;
    float delayTime;
    public GameObject FadeUI;
    public float DelayTime
    {
        get
        {
            return delayTime;
        }
    }

    public bool shouldHandleVideoCSV = true;
    public bool shouldHandleBaselineCSV = true;

    Coroutine COR_CHANGESCENE = null;
    IEnumerator ChangeScene()
    {
        int counter = Mathf.CeilToInt(delayTime);
        if (shouldHandleBaselineCSV){
            HandleCSV_BaselineStart("video");
            counter = Mathf.CeilToInt(changeSceneMinDelay);
        }
        for (int i = 0; i <= delayTime; i++)
        {
            SAMUIManager.instance.UpdateEndingInfomation(counter--);
            yield return new WaitForSeconds(1f);
        }
        if (shouldHandleBaselineCSV)
            HandleCSV_BaselineEnd();
    }

    public void ConfirmtoNextScene(){
        SceneManager.LoadScene(nextSceneName);
    }

    int valence;
    int arousal;
    public void UpdateSAMValue(int value)
    {
        if (state == State.FillInValence){
            valence = value;
        } else if (state == State.FillInArousal){
            arousal = value;
        }
            
    }

    public enum State
    {
        FillInValence,
        FillInArousal,
        Finished,
        BaselineIntro
    }
    State state = State.FillInValence;

    public State GetCurrentState
    {
        get
        {
            return state;
        }
    }

    public bool HasNextState
    {
        get
        {
            return state != State.Finished;
        }
    }

    public bool HasPreviousState
    {
        get
        {
            return state != State.FillInValence;
        }
    }

    public void GoPreviousState()
    {
        if (!HasPreviousState) return;

        switch (state)
        {
            case State.FillInArousal:
                ChangeState(State.FillInValence);
                break;
        }
    }

    public void GoNextState()
    {
        if (!HasNextState) return;

        switch (state)
        {
            case State.FillInValence:
                if (SAMUIManager.instance.UI_Confirm.activeSelf || SAMUIManager.instance.CheckDefaultValue()){
                    ChangeState(State.FillInArousal);
                    SAMUIManager.instance.UI_Confirm.SetActive(false);
                    SAMUIManager.instance.TXT_Hint1.SetActive(true);
                    SAMUIManager.instance.UI_Confirm = SAMUIManager.instance.Confirm_A;
                    SAMUIManager.instance.TXT_Hint1 = SAMUIManager.instance.TXT_HintA1;
                }
                break;
            case State.FillInArousal:
                if (SAMUIManager.instance.UI_Confirm.activeSelf || SAMUIManager.instance.CheckDefaultValue())
                {
                    SAMUIManager.instance.UI_Confirm.SetActive(false);
                    SAMUIManager.instance.TXT_Hint1.SetActive(true);
                    if(test_mode) SceneManager.LoadScene(nextSceneName);
                    delayTime = changeSceneDelay - (Time.time - start_timer);
                    delayTime = (delayTime < changeSceneMinDelay) ? changeSceneMinDelay : delayTime;
                    Debug.Log(delayTime);
                    SAMUIManager.instance.UpdateEndingInfomation(delayTime);
                    if(shouldHandleBaselineCSV) ChangeState(State.BaselineIntro);//如果是baseline的話要進入等待畫面
                    if( SkyboxVideoPlayerController.instance != null) {
                        if( !SkyboxVideoPlayerController.instance.VideoEnd) ChangeState(State.Finished);
                    }
                    if( SkyboxVideoPlayerController.instance != null) {
                        if( SkyboxVideoPlayerController.instance.VideoEnd) SceneManager.LoadScene(endSceneName);
                    }
                    if (shouldHandleVideoCSV)
                        HandleCSV_SAMFinished();
                    if (COR_CHANGESCENE == null && !shouldHandleBaselineCSV) COR_CHANGESCENE = StartCoroutine(ChangeScene());
                    
                } 
                break;
            case State.BaselineIntro:
                delayTime = changeSceneDelay - (Time.time - start_timer);
                delayTime = (delayTime < changeSceneMinDelay) ? changeSceneMinDelay : delayTime;
                Debug.Log(delayTime);
                SAMUIManager.instance.UpdateEndingInfomation(delayTime);
                ChangeState(State.Finished);
                if (COR_CHANGESCENE == null) COR_CHANGESCENE = StartCoroutine(ChangeScene());
                break;
        }
    }

    void ChangeState(State nextState)
    {
        state = nextState;
        SAMUIManager.instance.ChangeUI(state);
    }

    private void Awake()
    {
        instance = this;
        if(PlayerPrefs.GetInt("English") == 1){
            endSceneName = "_Ending -eng";
        }
    }

    void HandleCSV_SAMFinished()
    {
        VideoCSVHandler.instance.WriteRecord("SAM_valence", valence.ToString());
        VideoCSVHandler.instance.WriteRecord("SAM_arousal", arousal.ToString());
        Debug.Log("HandleCSV_SAMFinished:" + valence + "," + arousal);
        VideoCSVHandler.instance.WriteFile();
    }

    void HandleCSV_BaselineStart(string _type)
    {
        BaselineCSVHandler.instance.WriteRecord("type", _type);
        BaselineCSVHandler.instance.WriteRecord("start_time", BaselineCSVHandler.instance.GetDateTime());
    }

    void HandleCSV_BaselineEnd()
    {
        BaselineCSVHandler.instance.WriteRecord("end_time", BaselineCSVHandler.instance.GetDateTime());
        BaselineCSVHandler.instance.WriteFile();
    }

    private void Start()
    {
        start_timer = Time.time;
        ChangeState(State.FillInValence);
    }
}
