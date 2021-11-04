using UnityEngine;

public class UGUITweenPosition : UGUITweener
{
    public Vector3 from;
    public Vector3 to;

    RectTransform rectTr;

    private void Awake()
    {
        rectTr = GetComponent<RectTransform>();
    }

    public Vector3 value
    {
        get
        {
            if (rectTr == null)
            {
                return transform.position;
            }
            return rectTr.anchoredPosition;
        }
        set
        {
            if (rectTr == null)
            {
                transform.position = value;
            }
            else
            {
                rectTr.anchoredPosition = value;
            }
        }
    }

    public override void PlayForward()
    {
        base.PlayForward();
    }

    public override void PlayReverse()
    {
        base.PlayReverse();
    }

    public override void PlayToggle()
    {
        base.PlayToggle();
    }
    [ContextMenu("Set 'From' to current value")]
    public override void SetStartToCurrentValue() { from = value; }

    [ContextMenu("Set 'To' to current value")]
    public override void SetEndToCurrentValue() { to = value; }

    protected override void OnUpdate(float t) { value = Vector3.Lerp(from, to, t); }
}
