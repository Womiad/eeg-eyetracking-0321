using System.Collections.Generic;
using UnityEngine;

public class VideoCSVHandler : _CSVHandler
{
    public new static VideoCSVHandler instance;

    protected override List<string> header
    {
        get
        {
            List<string> header = new List<string>();
            header.Add("number");
            header.Add("title");
            header.Add("type");
            header.Add("ground_truth_valence");
            header.Add("ground_truth_arousal");
            header.Add("start_time");
            header.Add("end_time");
            header.Add("SAM_valence");
            header.Add("SAM_arousal");
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
        fileName = GetDateTime() + "_msg.csv";
        CreateFile();
        CreateRecord();
        DontDestroyOnLoad(this.gameObject);
    }
}
