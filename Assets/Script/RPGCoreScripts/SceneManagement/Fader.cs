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

        private void Start()
        {
            cg = GetComponent<CanvasGroup>();
            //StartCoroutine(FadeInOut());
        }

        public IEnumerator FadeOut(float time)
        {
            if(cg == null)
                cg = GetComponent<CanvasGroup>();

            while (cg.alpha < 1)
            {
                print("cgalphaincrease");
                cg.alpha +=  Time.deltaTime / time;
                yield return null;
            }
        }

        public IEnumerator FadeIn(float time)
        {
            if (cg == null)
                cg = GetComponent<CanvasGroup>();
            
            cg.alpha = 1;
            while (cg.alpha > 0)
            {
                print("FadeIn");
                print("cgalphadiscount");
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
