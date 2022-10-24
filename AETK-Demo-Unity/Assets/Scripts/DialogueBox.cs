using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using MyBox;
using UnityEngine.Assertions;

public class DialogueBox : MonoBehaviour
{
    public float BoxAppearTime = 1f;
    [Separator]
    [SerializeField]
    private Dialoguer dbDialoguer;
    [SerializeField]
    private RectTransform boxRoot;

    private RectTransform rt;

    public event System.Action OnBoxDisappear = () => { };

    public void Start()
    {
        var scale = boxRoot.localScale;

        scale.y = 0;

        boxRoot.localScale = scale;
    }

    public void StartDialogue(string knot)
    {
        // Make a fuss if the StartDialogue was called while the Dialoguer was still reading.
        Assert.IsFalse(dbDialoguer.IsReading);

        // Clear Dialoguer text.
        dbDialoguer.Clear();
        
        // Setup box animation. //
        boxRoot
            .DOScaleY(1, this.BoxAppearTime)
            .SetEase(Ease.OutBounce)
            .OnComplete( () => { dbDialoguer.ReadDialogue(knot); } );

        // Setup post-read animation. //
        System.Action diaFin = null;
        diaFin = () =>
        {
            boxRoot
                .DOScaleY(0, this.BoxAppearTime)
                .SetEase(Ease.OutBounce)
                .OnComplete( () => { OnBoxDisappear.Invoke(); } );

            // Remove self.
            dbDialoguer.OnDialogueFinished -= diaFin;
        };

        dbDialoguer.OnDialogueFinished += diaFin;
    }
}
