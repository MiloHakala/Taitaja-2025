using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameDialogue : MonoBehaviour
{
    public TextMeshProUGUI dialogueText;

    [Header("Strings")]
    public string CoreMovementText = "Move with WASD and plant trees with E";
    public string winText = "You Won!";
   

    [Header("Fade Settings")]
    public float fadeDuration = 1f;  // Duration for fade in and out

    private Coroutine currentFadeCoroutine;

    void Start()
    {
        StartCoroutine(OnPlayerAction());
    }

    // Function to display text with fade-in and fade-out effects
    public void DisplayTextWithFade(string message)
    {
        // If a fade is already in progress, stop it before starting a new one
        if (currentFadeCoroutine != null)
        {
            StopCoroutine(currentFadeCoroutine);
        }

        // Start a new fade coroutine
        currentFadeCoroutine = StartCoroutine(FadeText(message));
    }

    private IEnumerator FadeText(string message)
    {
        dialogueText.text = message;

        // Fade in
        float elapsedTime = 0f;
        while (elapsedTime < fadeDuration)
        {
            dialogueText.alpha = Mathf.Lerp(0f, 1f, elapsedTime / fadeDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        dialogueText.alpha = 1f;

        // Wait for 5 seconds
        yield return new WaitForSeconds(5f);

        // Fade out
        elapsedTime = 0f;
        while (elapsedTime < fadeDuration)
        {
            dialogueText.alpha = Mathf.Lerp(1f, 0f, elapsedTime / fadeDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        dialogueText.alpha = 0f;

        dialogueText.text = "";
    }

    public IEnumerator OnPlayerAction()
    {
        yield return new WaitForSeconds(3f);
        yield return StartCoroutine(FadeText(CoreMovementText));
    }

    public void youWon()
    {
        DisplayTextWithFade(winText);  // Display shoot text
    }
}
