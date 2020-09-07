using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace RPG.Core
{
    public class Fader : MonoBehaviour
    {
        CanvasGroup cg;
        [SerializeField] float fadeTime = 0.3f;
        // Start is called before the first frame update
        void Start()
        {
            cg = GetComponent<CanvasGroup>();
            //StartCoroutine(FadeInOut());
        }

        public IEnumerator FadeOut(float time)
        {
            while(cg.alpha < 1)
            {
                cg.alpha +=  Time.deltaTime / time;
                yield return null; 
            }
        }

        public IEnumerator FadeIn(float time)
        {
            while (cg.alpha > 0)
            {
                cg.alpha -= Time.deltaTime / time;
                yield return null;
            }
        }

        public IEnumerator FadeInOut()
        {
            yield return FadeOut(fadeTime);
            yield return FadeIn(fadeTime);
        }
    }

}
