using System.Collections.Generic;
using UnityEngine;

public class BaselineCSVHandler : _CSVHandler
{
    public new static BaselineCSVHandler instance;

    protected override List<string> header
    {
        get
        {
            List<string> header = new List<string>();
            header.Add("type");
            header.Add("start_time");
            header.Add("end_time");
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
        fileName = GetDateTime() + "_bl.csv";
        CreateFile();
        CreateRecord();
        DontDestroyOnLoad(this.gameObject);
    }
}
