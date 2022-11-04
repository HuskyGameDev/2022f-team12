using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestInteraction : MonoBehaviour, IInteractable
{
    public DialogueBox DB;

    public string DialogueKnot;

    public void OnInteract(PlayerOverworldControl pc)
    {
        DB.StartDialogue(DialogueKnot);

        // Diable player control. //
        pc.enabled = false;

        System.Action obd = null;
        obd = () =>
        {
            // Re-enable player control.
            pc.enabled = true;

            // Remove self from OnBoxDisappear.
            DB.OnBoxDisappear -= obd;
        };

        DB.OnBoxDisappear += obd;
    }
}
