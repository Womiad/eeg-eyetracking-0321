using System.Collections.Generic;

public class FaceCSVHandler : _CSVHandler
{
    public new static FaceCSVHandler instance;

    protected override List<string> header
    {
        get
        {
            List<string> header = new List<string>();
            header.Add("Time");
            header.Add("BrowLowererL");
            header.Add("BrowLowererR");
            header.Add("CheekPuffL");
            header.Add("CheekPuffR");
            header.Add("CheekRaiserL");
            header.Add("CheekRaiserR");
            header.Add("CheekSuckL");
            header.Add("CheekSuckR");
            header.Add("ChinRaiserB");
            header.Add("ChinRaiserT");
            header.Add("DimplerL");
            header.Add("DimplerR");
            header.Add("EyesClosedL");
            header.Add("EyesClosedR");
            header.Add("EyesLookDownL");
            header.Add("EyesLookDownR");
            header.Add("EyesLookLeftL");
            header.Add("EyesLookLeftR");
            header.Add("EyesLookRightL");
            header.Add("EyesLookRightR");
            header.Add("EyesLookUpL");
            header.Add("EyesLookUpR");
            header.Add("InnerBrowRaiserL");
            header.Add("InnerBrowRaiserR");
            header.Add("JawDrop");
            header.Add("JawSidewaysLeft");
            header.Add("JawSidewaysRight");
            header.Add("JawThrust");
            header.Add("LidTightenerL");
            header.Add("LidTightenerR");
            header.Add("LipCornerDepressorL");
            header.Add("LipCornerDepressorR");
            header.Add("LipCornerPullerL");
            header.Add("LipCornerPullerR");
            header.Add("LipFunnelerLB");
            header.Add("LipFunnelerLT");
            header.Add("LipFunnelerRB");
            header.Add("LipFunnelerRT");
            header.Add("LipPressorL");
            header.Add("LipPressorR");
            header.Add("LipPuckerL");
            header.Add("LipPuckerR");
            header.Add("LipStretcherL");
            header.Add("LipStretcherR");
            header.Add("LipSuckLB");
            header.Add("LipSuckLT");
            header.Add("LipSuckRB");
            header.Add("LipSuckRT");
            header.Add("LipTightenerL");
            header.Add("LipTightenerR");
            header.Add("LipsToward");
            header.Add("LowerLipDepressorL");
            header.Add("LowerLipDepressorR");
            header.Add("MouthLeft");
            header.Add("MouthRight");
            header.Add("NoseWrinklerL");
            header.Add("NoseWrinklerR");
            header.Add("OuterBrowRaiserL");
            header.Add("OuterBrowRaiserR");
            header.Add("UpperLidRaiserL");
            header.Add("UpperLidRaiserR");
            header.Add("UpperLipRaiserL");
            header.Add("UpperLipRaiserR");
            header.Add("TongueTipInterdental");
            header.Add("TongueTipAlveolar");
            header.Add("TongueFrontDorsalPalate");
            header.Add("TongueMidDorsalPalate");
            header.Add("TongueBackDorsalVelar");
            header.Add("TongueOut");
            header.Add("TongueRetreat");
            return header;
        }
    }

    void Awake()
    {
        if (instance != null)
        {
            Destroy(this.gameObject);
            return;
        }
        instance = this;
        fileName = GetDateTime() + "_face.csv";
        CreateFile();
        CreateRecord();
        DontDestroyOnLoad(this.gameObject);
    }
}
