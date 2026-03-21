using UnityEngine;
using System.IO;
using System.Runtime.InteropServices;

namespace uNvEncoder.Examples
{

public class OutputEncodedDataToFile : MonoBehaviour
{
    public string filePath = "test.h264";

    FileStream fileStream_;
    BinaryWriter binaryWriter_;

    void Start()
    {   
        int now_video = (int)(SkyboxVideoPlayerController.instance.Index);
        filePath = "C:\\Users\\USER\\Desktop\\Eye-tracking\\video\\" + $"Recording_{now_video}.h264";
        fileStream_ = new FileStream(filePath, FileMode.Create, FileAccess.Write);
        binaryWriter_ = new BinaryWriter(fileStream_);
    }

    void OnApplicationQuit()
    {
        if (fileStream_ != null) 
        {
            fileStream_.Close();
        }

        if (binaryWriter_ != null) 
        {
            binaryWriter_.Close();
        }
    }

    public void OnData(System.IntPtr ptr, int size)
    {
        if (!enabled) return;

        if (ptr == System.IntPtr.Zero) return;

        var bytes = new byte[size];
        Marshal.Copy(ptr, bytes, 0, size);
        binaryWriter_.Write(bytes);
    }
}

}
