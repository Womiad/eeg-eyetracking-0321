using System.Collections.Generic;
using UnityEngine;
public class CommandCSVHandler : _CSVHandler
{
    public new static CommandCSVHandler instance;

    protected override List<string> header
    {
        get
        {
            List<string> header = new List<string>();
            header.Add("video_number");
            header.Add("excuted_time");
            header.Add("end_time");
            header.Add("command");
            // header.Add("in_last20?");
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
        fileName = GetDateTime() + "_command.csv";
        CreateFile();
        CreateRecord();
        DontDestroyOnLoad(this.gameObject);
    }
}
