using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UGUITweenAlpha : UGUITweener
{
    public float from;
    public float to;

    MaskableGraphic image;
    CanvasGroup canvasGroup;

    private WaitForSeconds waitDuration;

    private void Awake()
    {
        waitDuration = new WaitForSeconds(duration);

        image = GetComponent<MaskableGraphic>();
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public float value
    {
        get
        {
            if(canvasGroup != null)
            {
                return canvasGroup.alpha;
            }
            return image.color.a;
        }
        set
        {
            if (canvasGroup != null)
            {
                canvasGroup.alpha = value;
            }
            else
            {
                Color c = image.color;
                c.a = value;
                image.color = c;
            }
        }
    }

    public override void PlayForward()
    {
        base.PlayForward();
        if (canvasGroup != null)
        {
            canvasGroup.blocksRaycasts = from == 0;
            canvasGroup.interactable = from == 0;
        }
    }

    public override void PlayReverse()
    {
        base.PlayReverse();
        if (canvasGroup != null)
        {
            canvasGroup.blocksRaycasts = from != 0;
            canvasGroup.interactable = from != 0;
        }
    }

    public override IEnumerator PlayPingPong()
    {
        while (true)
        {
            PlayForward();
            yield return waitDuration;
            PlayReverse();
            yield return waitDuration;
        }

    }

    public override void PlayToggle()
    {
        base.PlayToggle();
    }

    public override void SetStartToCurrentValue() { from = value; }

    public override void SetEndToCurrentValue() { to = value; }

    protected override void OnUpdate(float t) { value = Mathf.Lerp(from, to, t); }

    [ContextMenu("Assume value of 'From'")]
    public void SetCurrentValueToStart() { value = from; }

    [ContextMenu("Assume value of 'To'")]
    public void SetCurrentValueToEnd() { value = to; }
}
