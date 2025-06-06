using System.Collections;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class ObscuringItemFader : MonoBehaviour   //Put on sprites we want to fade as player moves behind.
{
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void FadeOut()
    {
        StartCoroutine(FadeOutRoutine());
    }

    public void FadeIn()
    {
        StartCoroutine(FadeInRoutine());
    }

    private IEnumerator FadeOutRoutine()    //I prefer to do mine without the weird distance thing, but is exactly the same.
    {
        float currentAlpha = spriteRenderer.color.a;

        float fadeDistance = currentAlpha - Settings.TARGET_ALPHA;

        while (currentAlpha - Settings.TARGET_ALPHA > 0.01f)
        {
            currentAlpha = currentAlpha - (fadeDistance / Settings.FADE_OUT_SECONDS * Time.deltaTime);

            spriteRenderer.color = new Color(1f, 1f, 1f, currentAlpha);

            yield return null;       //Next frame.
        }

        spriteRenderer.color = new Color(1f, 1f, 1f, Settings.TARGET_ALPHA);      //Ensure ends at target alpha.

    }

    private IEnumerator FadeInRoutine()
    {
        float currentAlpha = spriteRenderer.color.a;

        float fadeDistance = 1f - currentAlpha;      //get back to 100% opacity.

        while (1f - currentAlpha > 0.01f)
        {
            currentAlpha = currentAlpha + (fadeDistance / Settings.FADE_IN_SECONDS * Time.deltaTime);

            spriteRenderer.color = new Color(1f, 1f, 1f, currentAlpha);

            yield return null;       //Next frame.
        }

        spriteRenderer.color = new Color(1f, 1f, 1f, 1f);      //Ensure ends at 100% opacity.
    }
}
