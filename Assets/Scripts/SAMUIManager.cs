using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Palmmedia.ReportGenerator.Core.Parser.Analysis;
using System.Collections;

public class SAMUIManager : MonoBehaviour
{

    public bool English = false;

    public static SAMUIManager instance = null;

    public SAMSliderController valenceSlider;
    public SAMSliderController arousalSlider;

    public GameObject UI_Valence;
    public GameObject UI_Arousal;
    public GameObject UI_Ending;
    public GameObject Confirm_Next;
    public TMP_Text TXT_EndingCounter;
    public TMP_Text TXT_Hint;
    public GameObject TXT_Hint1;
    public TMP_Text TXT_HintV;
    public GameObject TXT_HintV1;
    public TMP_Text TXT_HintA;
    public GameObject TXT_HintA1;
    public GameObject UI_Confirm;
    public GameObject Confirm_V;
    public GameObject Confirm_A;
    public GameObject righthand;
    public AudioSource Audio;

    public GameObject UI_baseline_intro;
    public TMP_Text UI_baseline_text;
    public bool practice = false;
    public string state_string = "情緒效價";

    private void Start()
    {
        Audio = GetComponent<AudioSource>();
    }

    private void Awake()
    {
        instance = this;
    }

    public bool CheckDefaultValue()
    {
        SAMSceneManager.State currState = SAMSceneManager.instance.GetCurrentState;
        switch (currState)
        {
            case SAMSceneManager.State.FillInValence:
                if (valenceSlider.IsChanged)
                    return true;
                else
                    state_string = "情緒效價";
                    UpdateHint(state_string);
                    if(!practice){
                        UI_Confirm = Confirm_V;
                        TXT_Hint1 = TXT_HintV1;
                    }
                    UI_Confirm.SetActive(true);
                    TXT_Hint1.SetActive(false);
                break;
            case SAMSceneManager.State.FillInArousal:
                if (arousalSlider.IsChanged)
                    return true;
                else
                    state_string = "情緒覺醒";
                    UpdateHint(state_string);
                    if(!practice){
                        UI_Confirm = Confirm_A;
                        TXT_Hint1 = TXT_HintA1;
                    }
                    UI_Confirm.SetActive(true);
                    TXT_Hint1.SetActive(false);
                break;
        }
        return false;
    }

    public void ConfirmDefaultValue(bool isConfirm)
    {
        UI_Confirm.SetActive(false);
        TXT_Hint1.SetActive(true);
        SAMSceneManager.State currState = SAMSceneManager.instance.GetCurrentState;
        switch (currState)
        {
            case SAMSceneManager.State.FillInValence:
                state_string = "情緒效價";
                UpdateHint(state_string);
                if (isConfirm)
                {
                    valenceSlider.IsChanged = true;
                    SAMSceneManager.instance.GoNextState();
                }
                break;
            case SAMSceneManager.State.FillInArousal:
                state_string = "情緒覺醒";
                UpdateHint(state_string);
                if (isConfirm)
                {
                    arousalSlider.IsChanged = true;
                    SAMSceneManager.instance.GoNextState();
                }
                break;
        }
    }

    public void UpdateHint(string state){
        if(!practice){
            if(state == "情緒效價"){
                TXT_Hint = TXT_HintV;
                TXT_Hint1 = TXT_HintV1;
            }else {
                TXT_Hint = TXT_HintA;
                TXT_Hint1 = TXT_HintA1;
            }
        }
        
        TXT_Hint.text = "<color=yellow>目前選取的<color=green>" + state_string.ToString() + "</color>分數為<color=green> 1 </color>，若確定選取此分數請再次按下<color=green>確認</color>鍵";
        if(English){
            string state_eng_string;
            if(state == "情緒效價"){
                state_eng_string = "Valence";
            }else{
                state_eng_string = "Arousal";
            }
            TXT_Hint.text = "<color=yellow>The currently selected " + state_eng_string + " score is the default value of <color=green>1</color>. Please press <color=green>Confirm</color> to finalize your selection.</color>";
        }
        TXT_Hint1.SetActive(false);
    }

    public void ChangeUI(SAMSceneManager.State nextState)
    {
        switch (nextState)
        {
            case SAMSceneManager.State.FillInValence:
                UI_Valence.SetActive(true);
                UI_Arousal.SetActive(false);
                TXT_Hint1.SetActive(true);
                UI_Confirm.SetActive(false);
                UI_Ending.SetActive(false);
                break;
            case SAMSceneManager.State.FillInArousal:
                UI_Valence.SetActive(false);
                UI_Arousal.SetActive(true);
                TXT_Hint1.SetActive(true);
                UI_Confirm.SetActive(false);
                UI_Ending.SetActive(false);
                break;
            case SAMSceneManager.State.Finished:
                UI_Valence.SetActive(false);
                UI_Arousal.SetActive(false);
                UI_Ending.SetActive(true);
                Confirm_Next.SetActive(false);
                break;
            case SAMSceneManager.State.BaselineIntro:
                UI_Arousal.SetActive(false);
                TXT_Hint1.SetActive(false);
                UI_baseline_intro.SetActive(true);
                StartCoroutine(BaselineIntroRoutine());
                break;
        }
    }

    public void UpdateEndingInfomation(float time)
    {
        bool end_hint = false;
        int t = (int) time;
        if (t == 1){
            Audio.Play();
            TXT_EndingCounter.text = "請您按下確認鍵。\n 下部影片即將在 <color=yellow>按下確認</color> 後撥放。";
            if(English) TXT_EndingCounter.text = "Please press the confirm button. The video will play below <color=yellow>after you press confirm</color>.";
            end_hint = true;
            righthand.SetActive(true);
            Confirm_Next.SetActive(true);
        }
        if (t <= 0){
            end_hint = true;
            righthand.SetActive(true);
        }
        
        if(!practice){
            if (end_hint == false){
                TXT_EndingCounter.text = "請您放鬆心情，閉眼休息。\n 下部影片即將在 <color=yellow>" + time.ToString() +  "</color> 秒後撥放。";
                if(English) TXT_EndingCounter.text = "Please relax and close your eyes. The video will start in <color=yellow>" + time.ToString() + "</color> seconds.";
                righthand.SetActive(false);
            }
        }else{
            if (end_hint == false){
                righthand.SetActive(false);
                if (t <= 90 ){  
                    TXT_EndingCounter.text = "請您放鬆心情，閉眼休息。\n 實驗將在 <color=yellow>" + time.ToString() + "</color> 秒後開始。";
                    if(English) TXT_EndingCounter.text = "\nPractice exercises have concluded. Before proceeding to the formal experimental procedure, please relax and close your eyes for <color=yellow>" + time.ToString() + "</color> seconds.";
                }else{
                    TXT_EndingCounter.text = "範例練習已結束，在進入正式實驗程序前，\n 將請您放鬆心情，閉眼休息<color=yellow> 90 </color>秒。";
                    if(English) TXT_EndingCounter.text = "Practice exercises have concluded. Before proceeding to the formal experimental procedure, please relax and close your eyes for <color=yellow>90</color> seconds.";
                }
            }
            
        }
    }

    IEnumerator BaselineIntroRoutine(){
        UI_baseline_text.text = "很好，你成功了";
        yield return new WaitForSeconds(3f);
        UI_baseline_text.text = "接下來，請放慢你的呼吸，並放鬆身體";
        yield return new WaitForSeconds(3f);
        UI_baseline_text.text = "在之後的90秒內保持不動，並讓身體放鬆";
        yield return new WaitForSeconds(3f);
        UI_baseline_text.text = "準備開始放鬆";
        yield return new WaitForSeconds(3f);
        UI_baseline_text.text = "3";
        yield return new WaitForSeconds(1f);
        UI_baseline_text.text = "2";
        yield return new WaitForSeconds(1f);
        UI_baseline_text.text = "1";
        yield return new WaitForSeconds(1f);
        UI_baseline_intro.SetActive(false);
        SAMSceneManager.instance.GoNextState();
    }
}
