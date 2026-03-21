using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementCSVHandler :  _CSVHandler
{
public new static MovementCSVHandler instance;

    protected override List<string> header
    {
        get
        {
            List<string> header = new List<string>();
            header.Add("TimeStamp");
            header.Add("Head");
            header.Add("RightHand");
            header.Add("LeftHand");
            return header;
        }
    }

    void Awake()
    {
        filePath = PlayerPrefs.GetString("filePath");
        if (instance != null)
        {
            Destroy(this.gameObject);
            return;
        }
        instance = this;
        fileName = GetDateTime() + "_movement.csv";
        CreateFile();
        CreateRecord();
        DontDestroyOnLoad(this.gameObject);
    }
}
