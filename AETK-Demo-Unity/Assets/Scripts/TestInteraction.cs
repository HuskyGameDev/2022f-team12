using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestInteraction : MonoBehaviour, IInteractable
{
    public DialogueBox DB;

    public string DialogueKnot;

    [SerializeField]
    private Animator anim;
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
            anim.SetBool( animBoolParam, DB.dialoguer.IsReadingChars );

            yield return new WaitForEndOfFrame();
        }
    }
}
