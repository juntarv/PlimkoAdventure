using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    public float fadeDuration = 1.0f;
    public Color fadeColor = Color.black;

    private bool isFading = false;

    public void StartTransition(string sceneName)
    {
        if (!isFading)
        {
            StartCoroutine(Transition(sceneName));
        }
    }

    private IEnumerator Transition(string sceneName)
    {
        isFading = true;

        // Create a new blank texture
        Texture2D texture = new Texture2D(1, 1);
        texture.SetPixel(0, 0, fadeColor);
        texture.Apply();

        // Create a new sprite with the blank texture
        GameObject fadeCanvas = new GameObject("FadeCanvas");
        Canvas canvas = fadeCanvas.AddComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        fadeCanvas.AddComponent<UnityEngine.UI.Image>().color = fadeColor;
        fadeCanvas.GetComponent<UnityEngine.UI.Image>().raycastTarget = false;

        // Fade in
        float timer = 0f;
        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            float alpha = Mathf.Clamp01(timer / fadeDuration);
            fadeCanvas.GetComponent<UnityEngine.UI.Image>().color = new Color(fadeColor.r, fadeColor.g, fadeColor.b, alpha);
            yield return null;
        }

        // Load the new scene
        SceneManager.LoadScene(sceneName);

        // Fade out
        timer = 0f;
        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            float alpha = Mathf.Clamp01(1 - timer / fadeDuration);
            fadeCanvas.GetComponent<UnityEngine.UI.Image>().color = new Color(fadeColor.r, fadeColor.g, fadeColor.b, alpha);
            yield return null;
        }

        Destroy(fadeCanvas);

        isFading = false;
    }
}

