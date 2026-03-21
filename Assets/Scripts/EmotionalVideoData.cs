using System;
using UnityEngine.Video;
using CircumplexModel;

[Serializable]
public class EmotionalVideoData
{
    public int number;
    public string title;
    public VideoClip clip;
    public AffectiveType type;
    public AffectiveLevel groundTruth;
    public int materialRotation;

    public int mask_start = 0;
    public int mask_end = 0;

    public EmotionalVideoData()
    {
        number = -1;
        title = "";
        clip = null;
        type = AffectiveType.Netural;
        groundTruth = new AffectiveLevel();
        materialRotation = 0;
    }

    public EmotionalVideoData(AffectiveType _type)
    {
        number = -1;
        title = "";
        clip = null;
        type = _type;
        groundTruth = new AffectiveLevel();
    }

    public EmotionalVideoData(AffectiveType _type, int _number, string _title, VideoClip _clip, float _valence, float _arousal, int _materialRotation = 0, int _mask_start = 0, int _mask_end = 0)
    {
        number = _number;
        title = _title;
        clip = _clip;
        type = _type;
        groundTruth = new AffectiveLevel(_valence, _arousal);
        materialRotation = _materialRotation;
        mask_start = _mask_start;
        mask_end = _mask_end;
    }
}

