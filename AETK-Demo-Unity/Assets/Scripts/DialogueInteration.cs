using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyBox;

public class DialogueInteration : MonoBehaviour, IInteractable
{
    public DialogueBox DB;

    public string DialogueKnot;

    [Separator]

    /// <summary>
    /// An animator to control based on wether the Dialoguer's text is blitting.
    /// 
    /// If null, animatior control will be ignored.
    /// </summary>
    [SerializeField]
    private Animator anim;

    [ConditionalField( nameof(anim) )]
    [SerializeField]
    private string animBoolParam;

    public void OnInteract(PlayerOverworldControl pc)
    {
        // Play dialoguebox open anim, and have the dialoguer read the dialogue afterward.
        DB.StartDialogue( DialogueKnot );

        // Start controlAnimation coroutine.
        StartCoroutine( controlAnimation() );

        // Diable player control.
        pc.enabled = false;

        System.Action obd = null;
        obd = () =>
        {
            // Stop all coroutines.
            StopAllCoroutines();

            // Set the animBoolParam to false, just in case the user skipped through all the text.
            anim.SetBool(animBoolParam, false);

            // Re-enable player control.
            pc.enabled = true;

            // Remove self from OnBoxDisappear.
            DB.OnBoxDisappear -= obd;
        };

        DB.OnBoxDisappear += obd;
    }

    private IEnumerator controlAnimation()
    {
        while (true)
        {
            if (anim != null)
                anim.SetBool( animBoolParam, DB.dialoguer.IsReadingChars );

            yield return new WaitForEndOfFrame();
        }
    }
}
