using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LanguagePref : MonoBehaviour
{

    
    public bool English = false;

    public bool Command = true;

    public string filePath = "C:\\Users\\daimo\\OneDrive\\桌面\\Eye-Tracking_Recording";

    // Start is called before the first frame update
    void Start()
    {
        if(English){
            PlayerPrefs.SetInt("English", 1);
        }else{
            PlayerPrefs.SetInt("English", 0);
        }

        if(Command){
            PlayerPrefs.SetInt("Command", 1);
        }else{
            PlayerPrefs.SetInt("Command", 0);
        }

        PlayerPrefs.SetString("filePath", filePath);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
