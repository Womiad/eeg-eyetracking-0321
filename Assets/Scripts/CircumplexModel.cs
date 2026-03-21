using System;

namespace CircumplexModel
{
    public enum AffectiveType
    {
        Netural,
        HVHA,
        HVLA,
        LVHA,
        LVLA
    }

    [Serializable]
    public class AffectiveLevel
    {
        public float valence;
        public float arousal;

        public AffectiveLevel() => valence = arousal = 0f;

        public AffectiveLevel(float _valence, float _arousal)
        {
            valence = _valence;
            arousal = _arousal;
        }
    }
}

