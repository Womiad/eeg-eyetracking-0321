using System.Collections.Generic;
using CircumplexModel;

class EmotionalVideoDataSource
{
    public static List<EmotionalVideoData> videoSources = new List<EmotionalVideoData>()
    {
        // { new EmotionalVideoData(AffectiveType.LVLA, 1, "Abandoned building", null, 4.39f, 2.77f, 45) },
        // { new EmotionalVideoData(AffectiveType.LVLA, 7, "Fukushima", null, 2.69f, 4.63f, 0) },
        { new EmotionalVideoData(AffectiveType.LVLA, 14, "War Zone", null, 2.53f, 3.82f, 210) },
        // { new EmotionalVideoData(AffectiveType.LVLA, 16, "Solitary Confinement", null, 2.38f, 4.25f, 210) },
        { new EmotionalVideoData(AffectiveType.LVLA, 18, "The Displaced", null, 2.18f, 4.73f, 219) },
        { new EmotionalVideoData(AffectiveType.LVLA, 19, "The Nepal Earthquake Aftermath", null, 2.73f, 3.8f, 270) },
        { new EmotionalVideoData(AffectiveType.LVHA, 21, "Zombie Apocalypse Horror", null, 3.2f, 5.6f, 90) },
        { new EmotionalVideoData(AffectiveType.HVLA, 22, "Great Ocean Road", null, 7.77f, 3.92f, 270) },
        // { new EmotionalVideoData(AffectiveType.HVLA, 23, "Instant Caribbean Vacation", null, 7.2f, 3.2f, 270) },
        { new EmotionalVideoData(AffectiveType.HVLA, 33, "Pacific Sunset Half Moon Bay", null, 6.19f, 1.81f, 90) },
        { new EmotionalVideoData(AffectiveType.HVLA, 37, "Sunset of Oia-Santorini", null, 6.55f, 3.09f, 94) },
        // { new EmotionalVideoData(AffectiveType.Netural, 39, "Zip-lining in Chattanooga", null, 4.79f, 4.57f, 265) },
        { new EmotionalVideoData(AffectiveType.HVHA, 50, "Puppies host SourceFed for a day", null, 7.47f, 5.35f, 92) },
        { new EmotionalVideoData(AffectiveType.HVHA, 52, "Speed Flying", null, 6.75f, 7.42f, 133) },
        { new EmotionalVideoData(AffectiveType.HVHA, 53, "Tommorow Land", null, 5.8f, 5.4f, 90) },
        // { new EmotionalVideoData(AffectiveType.HVHA, 63, "NASA Encapsulation & Launch of OSIRIS Rex", null, 6.36f, 5.93f, 270) },
        // { new EmotionalVideoData(AffectiveType.HVHA, 66, "Great Hammerhead Shark Encounter", null, 6.17f, 6.67f, 75) },
        { new EmotionalVideoData(AffectiveType.LVHA, 68, "Jailbreak 360", null, 4.4f, 6.7f, 0) }
    };
}

