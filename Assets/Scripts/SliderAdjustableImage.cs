using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderAdjustableImage : MonoBehaviour
{
    float targetScale;
    float targetWhiteFactor;

    [SerializeField]
    Vector2 scaleRange = new Vector2(0.65f, 1f);
    [SerializeField]
    float lerpFactor = 0.05f;

    Image image;

    private void Start()
    {
        image = this.GetComponent<Image>();
    }

    public float Scale
    {
        set
        {
            targetWhiteFactor = targetScale = Mathf.Clamp(value, 0f, 1f) * (scaleRange.y - scaleRange.x) + scaleRange.x;
        }
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.localScale = Vector3.one * Mathf.Lerp(this.transform.localScale.x, targetScale, lerpFactor);
        float colorFactor = Mathf.Lerp(image.color.r, targetWhiteFactor, lerpFactor);
        image.color = new Color(colorFactor, colorFactor, colorFactor, 1);
    }
}
