using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class UISceneManager : MonoBehaviour
{
    public string nextSceneName = "SAM (Prac.)";
    public float changeSceneDelay = 5f;

    public TMP_Text TXT_Ending;
    public string Title_Ending = "影片";

    public float UIFadeInTime = 1f;
    public float UIFadeOutTime = 1f;
    public float UIDisplayTime = 3f;
    public List<CanvasGroup> UIs;

    public GameObject FadeUI;

    public bool shouldHandleCSV = true;

    IEnumerator FadeInOutUI(CanvasGroup UI, float time, float startValue, float endValue)
    {
        // Setting
        UI.alpha = startValue;
        // Fade
        float timeElapsed = 0;
        while (timeElapsed < time)
        {
            float value = Mathf.Lerp(startValue, endValue, timeElapsed / time);
            UI.alpha = value;
            timeElapsed += Time.deltaTime;
            yield return null;
        }
        UI.alpha = endValue;
    }

    IEnumerator ChangeScene()
    {
        float delayTime = changeSceneDelay;
        int counter = Mathf.CeilToInt(delayTime);
        for (int i = 0; i <= delayTime; i++)
        {
            if(Title_Ending != "SAM"){
                UpdateEndingInfomation(counter--);
            }else counter--;
            yield return new WaitForSeconds(1f);
        }
        // if (shouldHandleCSV) HandleCSV_BaselineEnd();
        // Debug.Log("WriteRecord:HandleCSV_BaselineEnd");
        if(Title_Ending != "SAM"){
            SceneManager.LoadScene(nextSceneName);
        }
    }

    IEnumerator SceneRoutine()
    {
        for (int i = 0; i < UIs.Count; i++)
        {
            yield return StartCoroutine(FadeInOutUI(UIs[i], UIFadeInTime, 0, 1));
            if (i == UIs.Count - 1)
                break;
            yield return new WaitForSeconds(UIDisplayTime);
            yield return StartCoroutine(FadeInOutUI(UIs[i], UIFadeOutTime, 1, 0));
        }
        StartCoroutine(ChangeScene());
    }

    void UpdateEndingInfomation(float time)
    {
        TXT_Ending.text = Title_Ending +
        "將在<color=orange>" +
        time.ToString() +
        "秒</color>後開始";
    }

    private void Start()
    {
        foreach (CanvasGroup UI in UIs)
        {
            UI.alpha = 0f;
        }
        if(Title_Ending != "SAM") UpdateEndingInfomation(changeSceneDelay);
        if (shouldHandleCSV) HandleCSV_BaselineStart("start");
        StartCoroutine(SceneRoutine());
    }

    void HandleCSV_BaselineStart(string _type)
    {
        Debug.Log("WriteRecord:HandleCSV_BaselineStart " + _type);
        BaselineCSVHandler.instance.WriteRecord("type", _type);
        BaselineCSVHandler.instance.WriteRecord("start_time", BaselineCSVHandler.instance.GetDateTime());
    }

    void HandleCSV_BaselineEnd()
    {
        BaselineCSVHandler.instance.WriteRecord("end_time", BaselineCSVHandler.instance.GetDateTime());
        BaselineCSVHandler.instance.WriteFile();
    }
}
