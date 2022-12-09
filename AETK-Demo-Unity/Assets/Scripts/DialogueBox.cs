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
    public Dialoguer dialoguer;
    [SerializeField]
    private RectTransform boxRoot;

    private RectTransform rt;

    public event System.Action OnBoxDisappear = () => { };

    public bool IsReading => dialoguer.IsReading;

    public void Start()
    {
        var scale = boxRoot.localScale;

        scale.y = 0;

        boxRoot.localScale = scale;
    }

    public void OpenBox( System.Action onOpenDone )
    {
        // Setup box animation. //
        boxRoot
            .DOScaleY(1, this.BoxAppearTime)
            .SetEase(Ease.OutBounce)
            .OnComplete(() => { onOpenDone.Invoke(); });
    }

    public void CloseBox(System.Action onCloseDone)
    {
        // Setup box animation. //
        boxRoot
            .DOScaleY(0, this.BoxAppearTime)
            .SetEase(Ease.OutBounce)
            .OnComplete(() => { onCloseDone.Invoke(); });
    }

    public void StartDialogue(string knot)
    {
        // Make a fuss if the StartDialogue was called while the Dialoguer was still reading.
        Assert.IsFalse(dialoguer.IsReading);

        // Clear Dialoguer text.
        dialoguer.Clear();

        // Setup open box animation.
        OpenBox( () => { dialoguer.ReadDialogue(knot); } );

        // Setup post-read animation. //
        System.Action diaFin = null;
        diaFin = () =>
        {
            CloseBox( () => { OnBoxDisappear.Invoke(); } );

            // Remove self.
            dialoguer.OnDialogueFinished -= diaFin;
        };

        dialoguer.OnDialogueFinished += diaFin;
    }
}
