using System.Collections;
using TMPro;
using UnityEngine;

namespace RPG.Core
{
    public class Fader : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI faderText;
        CanvasGroup loadingScreen;

        private void Awake()
        {
            loadingScreen = GetComponent<CanvasGroup>();
        }

        public IEnumerator FadeOut(float seconds, string text)
        {
            faderText.text = text;
            while (loadingScreen.alpha < 1)
            {
                loadingScreen.alpha += Time.deltaTime / seconds;
                yield return null;
            }
        }

        public IEnumerator FadeIn(float seconds)
        {
            while (loadingScreen.alpha > 0)
            {
                loadingScreen.alpha -= Time.deltaTime / seconds;
                yield return null;
            }
        }
    }
}
