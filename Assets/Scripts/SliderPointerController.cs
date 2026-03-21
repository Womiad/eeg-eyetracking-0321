using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class SliderPointerController : MonoBehaviour
{
    Image image;

    public float fadeInTime = 0.5f;
    public float fadeOutTime = 0.5f;
    public float displayTime = 2f;

    private void Awake()
    {
        image = this.GetComponent<Image>();
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Blink());
    }

    IEnumerator Blink()
    {
        float elapsedTime = 0;
        Color c = image.color;
        while (true)
        {
            elapsedTime = 0;
            while (elapsedTime < fadeInTime)
            {
                float alpha = Mathf.Lerp(0, 1, elapsedTime / fadeInTime);
                c.a = alpha;
                image.color = c;
                elapsedTime += Time.deltaTime;
                yield return new WaitForSeconds(0.02f);
            }
            c.a = 1;
            image.color = c;
            yield return new WaitForSeconds(displayTime);
            elapsedTime = 0;
            while (elapsedTime < fadeOutTime)
            {
                float alpha = Mathf.Lerp(1, 0, elapsedTime / fadeOutTime);
                c.a = alpha;
                image.color = c;
                elapsedTime += Time.deltaTime;
                yield return new WaitForSeconds(0.02f);
            }
            c.a = 1;
            image.color = c;
        }
    }
}
