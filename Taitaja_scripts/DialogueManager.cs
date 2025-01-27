using System.Collections;
using UnityEngine;
using TMPro;
using static UnityEngine.Rendering.DebugUI;
using System;


//this script works one text display at a time /for it to work on multiple; some modification is needed 
public class DialogueManager : MonoBehaviour
{
    public TMP_Text mainDisplayText; // Assign a TMP_Text component in the Inspector
    public TMP_Text subDisplayText; // Assign a TMP_Text component in the Inspector

    public float characterDelay = 0.1f; // Time in seconds between each character
    private string currentText = ""; // The current displayed text

    private bool ongoingDialogue = false;
    private bool wantToSkip = false;

    void Start()
    {
        
    }

   
    void Update()
    {
        //if player wants to fastforward dialogue
        if (Input.GetKeyDown(KeyCode.Space)) 
        {
            wantToSkip = true;
        }
        if(Input.GetKeyUp(KeyCode.Space))
        {
            wantToSkip = false;
        }
    }


    //give a nice animation when text is apearing
    private IEnumerator UpdateText(string textToDisplay, string textBubble)
    {
        //after how long it takes fore each chr to appear
        float timerForText = characterDelay;
        TMP_Text displayText = giveTextBubbel(textBubble);
        ongoingDialogue = true;
        currentText = ""; // Clear current text
        displayText.text = currentText; // Reset the display
        foreach (char c in textToDisplay)
        {
            if (wantToSkip)
            {
                timerForText = 0f;
                
            }
            else
            {
                timerForText = characterDelay;
            }
            //for if you want a multi paragrph dialogue text
            if (c == '_')
            {
                yield return new WaitForSeconds(2); // Wait for 2 seconds
                currentText = ""; // Clear the display text
                displayText.text = currentText; // Update the UI with an empty string
                continue; // Skip the underscore and continue with the next character
            }
            currentText += c; // Add one character at a time
            displayText.text = currentText; // Update the UI
            yield return new WaitForSeconds(timerForText); // Wait before adding the next character
        }
        print("done");
        ongoingDialogue = false;
        yield return new WaitForSeconds(2);
        //clean bubble
        if(!ongoingDialogue)
        {
            displayText.text = ""; // Clear current text
        }
        
    }

    //give option to chose what bubble text appears on //if not recognised defoults to main
    private TMP_Text giveTextBubbel(string textBubble)
    {
        return textBubble switch
        {
            "mainDisplayText" => mainDisplayText,
            "subDisplayText" => subDisplayText,
            _ => mainDisplayText
        };
    }

    //check if a dialougue is currently on 
    public bool NewDialogue(string textToDisplay, string textBubble = "mainDisplayText" /* define witch text bubble to show text on*/)
    {
        if (ongoingDialogue)
        {
            return false;
        }
        else
        {
            StartCoroutine(UpdateText(textToDisplay, textBubble));
            
            
        }
        return true;
    }
}
