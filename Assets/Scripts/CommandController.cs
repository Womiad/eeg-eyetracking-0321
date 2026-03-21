
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CommandController : MonoBehaviour
{
    public static CommandController instance;
    public GameObject command;
    public TMP_Text uiText;
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
    public void UpdateText(string newText)
    {
        if (uiText != null)
        {
            uiText.text = newText;
        }
    }  

    public void SetTextActive(bool active){
        if(command != null){
            command.SetActive(active);
        }
    }
}
