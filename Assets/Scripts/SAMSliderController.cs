using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class SAMSliderController : MonoBehaviour
{
    Slider slider;

    public int test_value = 0;
    public GameObject Confirm;

    public int Value
    {
        get
        {
            return (int)slider.value;
        }
    }

    bool isChanged = false;
    public bool IsChanged
    {
        set
        {
            isChanged = value;
        }
        get
        {
            return isChanged;
        }
    }

    public SliderAdjustableImage[] SAM_images;
    float[] imageTargetValues;
    float offsetValue;

    private void Awake()
    {
        slider = this.GetComponent<Slider>();
    }

    // Start is called before the first frame update
    void Start()
    {
        if(test_value != -1){
            Confirm.GetComponent<Button>().interactable = false;
            Debug.Log("test != 0");
        }
        imageTargetValues = new float[SAM_images.Length];
        offsetValue = (float)(slider.maxValue - slider.minValue) / (SAM_images.Length - 1f);
        for (int i = 0; i < imageTargetValues.Length; i++)
            imageTargetValues[i] = slider.minValue + offsetValue * i;
        OnValueChanged(false);
    }

    public void OnValueChanged(bool shouldUpdateChanged)
    {
        SAMUIManager.instance.UI_Confirm.SetActive(false);
        SAMUIManager.instance.TXT_Hint1.SetActive(true);
        if (shouldUpdateChanged) isChanged = true;
        for (int i = 0; i < SAM_images.Length; i++)
        {
            SAM_images[i].Scale = 1f - Mathf.Abs((slider.value - imageTargetValues[i]) / offsetValue);
        }
        SAMSceneManager.instance.UpdateSAMValue((int)slider.value);
        if(test_value != -1){
            if(test_value == ((int)slider.value)){
                Confirm.GetComponent<Button>().interactable = true;
            }else{
                Confirm.GetComponent<Button>().interactable = false;
            }
        }
        
    }
}
