using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fader : MonoBehaviour
{
    CanvasGroup cg;
    [SerializeField]bool fadeIn;
    [SerializeField]bool fadeOut;
    // Start is called before the first frame update
    void Start()
    {
        cg = GetComponent<CanvasGroup>();
    }

    void FadeIn()
    {
        cg.alpha += 0.3f * Time.deltaTime;
    }

    void FadeOut()
    {
        cg.alpha -= 0.3f * Time.deltaTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (fadeIn)
            FadeIn();
        if (fadeOut)
            FadeOut();
    }
}
