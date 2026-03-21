using System;
using System.IO;
using System.Collections.Generic;
using UnityEngine;

public abstract class _CSVHandler : MonoBehaviour
{
    public static _CSVHandler instance = null;

    public string filePath;
    protected abstract List<string> header { get; }
    Dictionary<string, string> record = new Dictionary<string, string>();
    protected string fileName;

    string fullPath()
    {
        return Path.Combine(filePath, fileName);
    }

    string header2str()
    {
        string headerStr = "";
        foreach (string hdr in header)
            headerStr += hdr + ",";
        return headerStr.Substring(0, headerStr.Length - 1);
    }

    string record2str()
    {
        string recordStr = "";
        foreach (string hdr in header)
            recordStr += record[hdr] + ",";
        return recordStr.Substring(0, recordStr.Length - 1);
    }

    public string GetDateTime()
    {
        DateTime dateTime = DateTime.Now;
        return string.Format("{0:yyyy-MM-dd_HH-mm-ss_fff}", dateTime.ToLocalTime());
    }

    protected void CreateFile()
    {
        StreamWriter writer = new StreamWriter(fullPath());
        writer.WriteLine(header2str());
        writer.Close();
        writer.Dispose();
    }

    public void WriteFile()
    {
        if (File.Exists(fullPath()))
        {
            if (IsRecordComplete())
            {
                StreamWriter writer = File.AppendText(fullPath());
                writer.WriteLine(record2str());
                writer.Close();
                writer.Dispose();
                ClearRecord();
                Debug.Log("WriteRecird:ClearRecord");
            }
            else
            {
                Debug.LogErrorFormat("Record is not complete: {0}", record2str());
            }
        }
        else
        {
            Debug.LogErrorFormat("File ({0}) doesn't exisit", fullPath());
        }
    }

    protected void CreateRecord()
    {
        record.Clear();
        ClearRecord();
    }

    public void WriteRecord(string _key, string _value)
    {
        if (record.ContainsKey(_key))
        {
            Debug.Log("WriteRecord:WriteRecord " + _key + ":" + _value);
            record[_key] = _value;
        }
    }

    void ClearRecord()
    {
        foreach (string _key in header)
        {
            record[_key] = "";
        }
    }

    bool IsRecordComplete()
    {
        foreach (string _key in header)
        {
            Debug.Log("WriteRecord:IsRecordComplete " + _key + ":" + record[_key]);
            if (record[_key] == "")
                return false;
        }
        return true;
    }
}
