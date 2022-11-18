using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ink.Runtime;
using UnityEngine.Assertions;

public class Dialoguer : MonoBehaviour
{
    [SerializeField]
    private SuperTextMesh stm;

    [SerializeField]
    private TextAsset inkScript;

    [SerializeField]
    private string defaultDrawAnimation;

    private Story inkStory;

    private Coroutine dialogueReadCR;
    private bool dialogueReadCRRunning = false;

    public bool IsReading
    {
        get => dialogueReadCRRunning;
    }

    public event System.Action OnDialogueFinished = () => { };

    /// <summary>
    /// Forwarded to the SuperTextMesh's OnCustomEvent.
    /// </summary>
    public event SuperTextMesh.OnCustomAction OnCustomEvent
    {
        add { stm.OnCustomEvent += value; }
        remove { stm.OnCustomEvent -= value; }
    }

    private void Awake()
    {
        this.inkStory = new Story(inkScript.text);
    }

    public void ReadDialogue(string knot)
    {
        // Make a fuss if the ReadDialogue was called while still reading dialogue.
        Assert.IsFalse( dialogueReadCRRunning );
        
        this.inkStory.ChoosePathString(knot);

        this.dialogueReadCR = StartCoroutine( dialogueRead() );
    }

    public void Clear()
    {
        this.stm.text = "";
    }

    private IEnumerator dialogueRead()
    {
        dialogueReadCRRunning = true;

        bool remainingStory = true;

        while (remainingStory)
        {
            // If there is another line of dialogue to read. //
            if (inkStory.canContinue)
            {
                // Set the STM's text to the next line of dialogue, prependded by a call
                // to our default draw animation.
                stm.text = $"<drawAnim={this.defaultDrawAnimation}>{inkStory.Continue()}";
                stm.Read();
            }
            // If there is a choice to be made. //
            else if (inkStory.currentChoices.Count > 0)
            {
                Debug.LogError("Dialoguer choices not yet implemented.");
                yield break;
            }
            // If dialogue is over. //
            else
            {
                remainingStory = false;
                break;
            }

            // Wait for the spacebar to be pressed. //
            yield return AssCoroutine.WaitFor( () => {
                return Input.GetKeyDown("space");
            }, false);
        }

        dialogueReadCRRunning = false;

        OnDialogueFinished.Invoke();
    }
}
