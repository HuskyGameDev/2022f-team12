using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ink.Runtime;

public class TestScriptRead : MonoBehaviour
{
    public TextAsset Script;

    private Story story;

    private void Start()
    {
        this.story = new Story(Script.text);
    }

    private void Update()
    {
        bool advance = false;

        if (Input.GetKeyDown("0"))
        {
            story.ChooseChoiceIndex(0);
            advance = true;
        }
        else if (Input.GetKeyDown("1"))
        {
            story.ChooseChoiceIndex(1);
            advance = true;
        }
        else if (Input.GetKeyDown("2"))
        {
            story.ChooseChoiceIndex(2);
            advance = true;
        }

        if ( Input.GetKeyDown("space") || advance ) 
        {
            if (story.canContinue)
            {
                Debug.Log(story.Continue());
            }
            else if (story.currentChoices.Count > 0)
            {
                Debug.Log("Choices:");

                var choices = story.currentChoices;

                foreach (var c in choices)
                {
                    Debug.Log($"{c.index}: {c.text}");
                }
            }
            else
            {
                Debug.Log("The story is now over. Restarting...");
                story.ResetState();
            }
        }
    }
}
